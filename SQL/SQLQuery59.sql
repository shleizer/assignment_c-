Create PROCEDURE [dbo].[GetOrdersInYear] @Year int, @Orders int
AS

	select AGENT_CODE, AGENT_NAME, PHONE_NO
	from AGENTS
	where AGENT_CODE in (
		(select AGENT_CODE
		from ORDERS
		where ord_date >= DATEFROMPARTS(@Year, 1, 1) and ord_date < DATEFROMPARTS(@Year + 1, 1, 1)
		group by  AGENT_CODE
		having sum(ORD_AMOUNT) >=@Orders))
Go
