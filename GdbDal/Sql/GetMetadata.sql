-- DECLARE @name as nvarchar(226) = 'LRS.DBO.GPSLRSStatwide'
SELECT [Documentation]
FROM [dbo].[GDB_ITEMS]
WHERE [Name] LIKE @name