-- ================================================ 
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 
CREATE OR ALTER PROCEDURE sp_ReportAccountPayable
	@ClientTradingProfileID int,
	@ClientCurrencyIDOut tinyint,
	@TradeDateFrom datetime,
	@TradeDateTo datetime
AS
BEGIN 
	SET NOCOUNT ON;
	
/*
get ActiveDays
*/
with a (ClientTradingProfileID,ActiveDays) as (
select ClientTradingProfileID, count(ClientTradingProfileID) as ActiveDays from 
(select  ClientTradingProfileID, TradeDate 
from TxnView 
where PayoutDone = 0 and TradeDate >= @TradeDateFrom and TradeDate <= @TradeDateTo
group by ClientTradingProfileID,TradeDate ) as s
group by ClientTradingProfileID
),

/*
get ActiveDays Weekend
*/
w (ClientTradingProfileID,ActiveDaysWeekend) as (
select ClientTradingProfileID, count(ClientTradingProfileID) as ActiveDaysWeekend from 
(select  ClientTradingProfileID, TradeDate 
from TxnView 
where PayoutDone = 0 and TradeDate >= @TradeDateFrom and TradeDate <= @TradeDateTo
      and (DATENAME(DW, TradeDate) = 'Saturday' or DATENAME(DW, TradeDate) = 'Sunday')
group by ClientTradingProfileID,TradeDate ) as s
group by ClientTradingProfileID
),

/*
get payout/txn
*/
p (TxnID,ClientPayoutAmountTotal) as (
select TxnID, sum(isnull(ClientPayoutAmount,0)) as ClientPayoutAmountTotal
from payout 
group by TxnID 
),

/*
get txn group by ClientTradingProfileID
*/
t (ClientTradingProfileID,ClientTradingProfileName,ClientCurrencyIDIn,ClientCurrencyNameIn,ClientCurrencyIDOut,ClientCurrencyNameOut,TradeDateSince,TradeDateUp,ClientAmountUncompleted,BalanceIn) as (
select  ClientTradingProfileID,ClientTradingProfileName,
        ClientCurrencyIDIn,ClientCurrencyNameIn,
		ClientCurrencyIDOut,ClientCurrencyNameOut,		
		min(TradeDate) as TradeDateSince, 
		max(TradeDate) as TradeDateUp, 
        sum(isnull(ClientAmountOut,0)-isnull(ClientPayoutAmountTotal,0)) as ClientAmountUncompleted,

		sum(iif(isnull(ClientAmountOut,0)=0,isnull(ClientAmountIn,0),0)) as BalanceIn
from TxnView t
left outer join p on t.id=p.TxnID
where PayoutDone = 0 and TradeDate >= @TradeDateFrom and TradeDate <= @TradeDateTo
group by ClientTradingProfileID,ClientTradingProfileName,ClientCurrencyIDIn,ClientCurrencyNameIn,ClientCurrencyIDOut,ClientCurrencyNameOut
)
 
select t.ClientTradingProfileID, ClientTradingProfileName,ClientCurrencyNameIn,ClientCurrencyNameOut,TradeDateSince,TradeDateUp,ClientAmountUncompleted,BalanceIn,
a.ActiveDays,
isnull(w.ActiveDaysWeekend,0) as ActiveDaysWeekend,
(DATEDIFF(wk, TradeDateSince, TradeDateUp) * 2)
   +(CASE WHEN DATENAME(dw, TradeDateSince) = 'Sunday'   THEN 1 ELSE 0 END)
   +(CASE WHEN DATENAME(dw, TradeDateUp)   = 'Saturday' THEN 1 ELSE 0 END) as DaysWeekend,
ClientAmountUncompleted*dout.Rate as ClientAmountUncompletedUSD,
dout.Rate,
BalanceIn*din.Rate as BalanceInUSD
from t 
inner join a on t.ClientTradingProfileID = a.ClientTradingProfileID  
left outer join w on t.ClientTradingProfileID = w.ClientTradingProfileID 

left outer join CurrencyPairView cpin on t.ClientCurrencyIDIn=cpin.id1 and cpin.CurrencyName2='USD'
left outer join DfrLatestView din on cpin.id=din.CurrencyPairID

left outer join CurrencyPairView cpout on t.ClientCurrencyIDOut=cpout.id1 and cpout.CurrencyName2='USD'
left outer join DfrLatestView dout on cpout.id=dout.CurrencyPairID

where (ClientAmountUncompleted>0 or BalanceIn>0)
and (@ClientTradingProfileID=0 or t.ClientTradingProfileID=@ClientTradingProfileID)
and (@ClientCurrencyIDOut=0 or t.ClientCurrencyIDOut=@ClientCurrencyIDOut)
END
GO
