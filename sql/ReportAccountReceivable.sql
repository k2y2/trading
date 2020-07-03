
WITH  
s (ProviderTradingProfileID, TotalSlipAmount) AS ( 
SELECT ProviderTradingProfileID, sum(isnull(SlipAmount,0)) AS TotalSlipAmount  
FROM      Slip
WHERE   ProviderTradingProfileID IS NOT NULL and isnull(ActualAmount,0)=0
GROUP BY ProviderTradingProfileID
)

select r.ProviderTradingProfileID, r.ProviderTradingProfileName,
       isnull(r.MissingSlipAmount,0)+isnull(s.TotalSlipAmount,0) as AmountReceivable,
	   cp1.CurrencyName2,
	   (isnull(r.MissingSlipAmount,0)+isnull(s.TotalSlipAmount,0)) * Rate as AmountReceivableUSD
	   ,Rate
from ReportProvider r
left outer join s on r.ProviderTradingProfileID=s.ProviderTradingProfileID
left outer join ProviderTradingProfile ptp on r.ProviderTradingProfileID=ptp.id 
left outer join CurrencyPairView cp1 on ptp.CurrencyPairID=cp1.id 
left outer join CurrencyPairView cp2 on cp1.id2 = cp2.id1 and cp2.CurrencyName2='USD'
left outer join DfrLatestView d on cp2.id=d.CurrencyPairID
 
 