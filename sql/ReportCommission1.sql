
with t (TxnID,ReferenceNo,TradeDate,ClientTradingProfileID,ClientTradingProfileName,ClientCurrencyNameIn,ClientAmountIn,ClientCurrencyNameOut,ClientAmountOut,              IntroducerID,IntroducerCommissionRate) as (
select t.id, t.ReferenceNo, t.TradeDate, t.ClientTradingProfileID,t.ClientTradingProfileName,t.ClientCurrencyNameIn,t.ClientAmountIn,t.ClientCurrencyNameOut,t.ClientAmountOut,   m.IntroducerID, m.IntroducerCommissionRate 
from  TxnView t
inner join ClientTradingProfileIntroducerMap m on t.ClientTradingProfileID=m.ClientTradingProfileID
where m.IntroducerCommissionTypeID=1
)
select 
t.* , (isnull(t.IntroducerCommissionRate,0)/100)*r.GrossProfitUSD as IntroducerCommissionUSD
from t
left outer join ReportTxn r on t.TxnID=r.TxnID