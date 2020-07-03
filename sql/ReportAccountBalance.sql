
with d (CurrencyPairID, Rate) AS ( 
	select CurrencyPairID, 
		   Rate
	from (select CurrencyPairID, 
				 Rate, 
				 row_number() over(partition by CurrencyPairID order by TradeDate desc) as rn
		  from Dfr) as d
	where rn = 1    
),
bal (AccountBankAccountID,Balance) AS ( 
select AccountBankAccountID, sum(isnull(AmountCredit,0))-Sum(isnull(AmountDebit,0)) as Balance
from AccountTxn 
group by AccountBankAccountID  
)
select ab.id, ab.AccountName, ab.CurrencyName, bal.Balance, bal.Balance*Rate as BalanceUSD,Rate 
from bal   
left outer join AccountBankAccountView ab on bal.AccountBankAccountID=ab.id
left outer join CurrencyPairView cp on ab.CurrencyID=cp.id1 and cp.CurrencyName2='USD'
left outer join d on cp.id = d.CurrencyPairID