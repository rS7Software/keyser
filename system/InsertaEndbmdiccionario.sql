CREATE PROCEDURE [dbo].[InsertaEndbmdiccionario]
	@param0 varchar(50),
	@param1 varchar(50),
	@param2 tinyint,
	@param3 varchar(30),
	@param4 varchar(50),
	@param5 int

AS BEGIN

INSERT INTO [dbo].[dbmdiccionario] (
	[cve_base_datos], 
	[nom_tabla], 
	[num_ordinal], 
	[nom_campo], 
	[cve_tipo_datos],
	[tam],
	[marca]) 
VALUES ( 
	@param0,
	@param1,
	@param2,
	@param3,
	@param4,
	@param5,
	1)
END
