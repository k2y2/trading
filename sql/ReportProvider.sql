
WITH 
t (ProviderTradingProfileID, TotalProviderExpectedPayInAmount) AS ( 
SELECT ProviderTradingProfileID, sum(isnull(ProviderExpectedPayInAmount,0)) AS TotalProviderExpectedPayInAmount
FROM      Txn
WHERE   ProviderTradingProfileID IS NOT NULL
GROUP BY ProviderTradingProfileID
),
s (ProviderTradingProfileID, TotalSlipAmount, TotalActualAmount) AS ( 
SELECT ProviderTradingProfileID, sum(isnull(SlipAmount,0)) AS TotalSlipAmount, sum(isnull(ActualAmount,0)) AS TotalActualAmount 
FROM      Slip
WHERE   ProviderTradingProfileID IS NOT NULL
GROUP BY ProviderTradingProfileID
)
select t.ProviderTradingProfileID, p.ProviderTradingProfileName, TotalProviderExpectedPayInAmount, isnull(TotalSlipAmount,0) as TotalSlipAmount, isnull(TotalActualAmount,0) as TotalActualAmount,
TotalProviderExpectedPayInAmount-isnull(TotalSlipAmount,0) as MissingSlipAmount
from t 
left outer join s on t.ProviderTradingProfileID=s.ProviderTradingProfileID
left outer join ProviderTradingProfile p on t.ProviderTradingProfileID=p.id
