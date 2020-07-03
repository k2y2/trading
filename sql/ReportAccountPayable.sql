
with p (TxnID,ClientPayoutAmountTotal) as (
select TxnID, sum(isnull(ClientPayoutAmount,0)) as ClientPayoutAmountTotal
from payout 
group by TxnID 
),

t (ClientTradingProfileID,ClientTradingProfileName,ClientCurrencyIDOut,ClientCurrencyNameOut,TradeDate,ClientAmountUncompleted) as (
select  ClientTradingProfileID,ClientTradingProfileName,
        ClientCurrencyIDOut,ClientCurrencyNameOut,
		TradeDate, 
        sum(isnull(ClientAmountOut,0)-isnull(ClientPayoutAmountTotal,0)) as ClientAmountUncompleted
from TxnView t
left outer join p on t.id=p.TxnID
group by ClientTradingProfileID,ClientTradingProfileName,ClientCurrencyIDOut,ClientCurrencyNameOut,TradeDate
)

select ClientTradingProfileID, ClientTradingProfileName,ClientCurrencyNameOut,TradeDate,ClientAmountUncompleted,
ClientAmountUncompleted*d.Rate as ClientAmountUncompletedUSD,
Rate
from t 
left outer join CurrencyPairView cp on t.ClientCurrencyIDOut=cp.id1 and cp.CurrencyName2='USD'
left outer join DfrLatestView d on cp.id=d.CurrencyPairID
