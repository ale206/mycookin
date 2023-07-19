-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <20/11/20152>
-- Description:	<Get Not Standard Quantity by its Id and according to Language Id>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetQuantityNotStdByIdAndLanguage]
	@IDQuantityNotStd int,
	@IDLanguage int

AS
BEGIN
    SELECT     QuantityNotStdLanguage.IDQuantityNotStdLanguage, QuantityNotStdLanguage.IDQuantityNotStd, QuantityNotStdLanguage.IDLanguage,
                      QuantityNotStdLanguage.QuantityNotStdSingular, QuantityNotStdLanguage.QuantityNotStdPlural
                      FROM         QuantityNotStdLanguage INNER JOIN
                      QuantityNotStd ON QuantityNotStdLanguage.IDQuantityNotStd = QuantityNotStd.IDQuantityNotStd
                      WHERE     (QuantityNotStd.EnabledToUser = 1) AND (QuantityNotStdLanguage.IDLanguage = @IDLanguage) AND (QuantityNotStdLanguage.IDQuantityNotStd = @IDQuantityNotStd)
END