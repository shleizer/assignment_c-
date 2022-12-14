
Create PROCEDURE [dbo].[GetMaxAdvanceAmountAgent] @Year int
AS

	select top 1 agent_code
	from ORDERS
	Where ord_date >= DATEFROMPARTS(@Year, 1, 1) and ord_date < DATEFROMPARTS(@Year, 4, 1)
	group by agent_code
	order by sum(ADVANCE_AMOUNT) desc;
Go