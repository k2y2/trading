

WITH 
comm1 (ClientTradingProfileID, IntroducerCommissionRate) AS ( 
	select ClientTradingProfileID,sum(isnull(IntroducerCommissionRate,0))/100
	from ClientTradingProfileIntroducerMap
	where IntroducerCommissionTypeID=1
	GROUP BY ClientTradingProfileID
), 
comm2 (ClientTradingProfileID, IntroducerCommissionRate) AS ( 
	select ClientTradingProfileID,sum(isnull(IntroducerCommissionRate,0))/100
	from ClientTradingProfileIntroducerMap
	where IntroducerCommissionTypeID=2
	GROUP BY ClientTradingProfileID
), 
pout (TxnID, ClientTradingProfileID, ClientPayoutUSD,ClientPayoutOrig, IntroducerCommissionUSD2) AS ( 
	select p.TxnID,
	p.ClientTradingProfileID,
	sum(p.ClientPayoutAmount * p.UsedClientPayoutFXRate / p.UsedUSDRate) AS ClientPayoutUSD,
	sum(p.ClientPayoutAmount) AS ClientPayoutOrig,
	--sum((p.ClientPayoutAmount / p.ClientPayoutUSDRate) * isnull(comm2.IntroducerCommissionRate,0)) as IntroducerCommissionUSD2
	sum((p.ClientPayoutAmount * p.UsedClientPayoutFXRate / p.UsedUSDRate) * isnull(comm2.IntroducerCommissionRate,0)) as IntroducerCommissionUSD2
	from PayoutView p
	left outer join comm2 on p.ClientTradingProfileID = comm2.ClientTradingProfileID
	group by TxnID,p.ClientTradingProfileID
) 
,pin (TxnID, ProviderPayinUSD) AS ( 
	select TxnID, 
		   ProviderPayinAmount / p.ProviderPayinUSDRate
	from (select TxnID, 
				 ProviderPayinAmount,
				 ProviderPayinUSDRate, 
				 row_number() over(partition by TxnID order by DateTimeModified desc) as rn
		  from PayoutView) as p
	where rn = 1    
)

select i.TxnID, i.ProviderPayinUSD, o.ClientPayoutUSD, o.IntroducerCommissionUSD2, o.ClientPayoutOrig, t.ClientAmountOut-o.ClientPayoutOrig as ClientPayoutMissing, 
t.ReferenceNo, t.TradeDate,t.ClientCurrencyNameOut,t.ClientAmountOut,
t.ClientTradingProfileID,t.ClientTradingProfileName,t.ProviderTradingProfileID,t.ProviderTradingProfileName,

(i.ProviderPayinUSD - o.ClientPayoutUSD - o.IntroducerCommissionUSD2) * isnull(comm1.IntroducerCommissionRate,0) as IntroducerCommissionUSD1,
(i.ProviderPayinUSD - o.ClientPayoutUSD - o.IntroducerCommissionUSD2) * (1-isnull(comm1.IntroducerCommissionRate,0)) as GrossProfitUSD,
(i.ProviderPayinUSD - o.ClientPayoutUSD - o.IntroducerCommissionUSD2) * (1-isnull(comm1.IntroducerCommissionRate,0))*100/o.ClientPayoutUSD as GrossProfitUSDPct
from pin i
inner join pout o on i.TxnID=o.TxnID
left outer join comm1 on o.ClientTradingProfileID=comm1.ClientTradingProfileID
inner join TxnCompleteView t on i.TxnID=t.id
