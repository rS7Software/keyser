CREATE PROCEDURE [dbo].[InsertaEndbmbases_datos]
	@param0 varchar(50),
	@param1 varchar(50),
	@param2 varchar(50),
	@param3 bit

AS BEGIN

INSERT INTO [dbo].[dbmbases_datos] (
	[cve_base_datos], 
	[nombre_base_datos], 
	[dbms], 
	[enlinea])
VALUES ( 
	@param0,
	@param1,
	@param2,
	@param3)
END
