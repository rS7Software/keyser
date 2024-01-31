CREATE PROCEDURE [dbo].[ActualizaEndbmbases_datos]
	@param0 varchar(50),
	@param1 varchar(50),
	@param2 varchar(50),
	@param3 bit

AS BEGIN

UPDATE [dbo].[dbmbases_datos] SET
	[nombre_base_datos] = @param1,
	[dbms] = @param2,
	[enlinea] = @param3
WHERE
	[cve_base_datos] = @param0 

END
