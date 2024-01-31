CREATE PROCEDURE [dbo].[ActualizaMarcaEndbmdiccionario]
	@param0 varchar(50),
	@param1 varchar(50),
	@param2 tinyint,
	@param3 bit

AS BEGIN

UPDATE [dbo].[dbmdiccionario] SET
	[marca] = @param3
WHERE
	[cve_base_datos] = @param0 AND 
	[nom_tabla] = @param1 AND 
	[num_ordinal] = @param2 

END
