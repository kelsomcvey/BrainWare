-- =============================================
-- Author:		Kieran McVey
-- Create date: 2nd May 2024
-- Description:	Get compaany name and id where they have an existing order
-- =============================================
create PROCEDURE [dbo].[SPBW_GET_ALL_COMPANY_DETAILS] 
	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT c.name AS CompanyName,
      c.company_id as CompanyId
	FROM company c
	-- only return results if the company has an order
	where exists (select '' from [dbo].[Order] as o where o.company_id = c.company_id)
END
