CREATE PROCEDURE [dbo].[InsertaEndbmarchivos]
	@param0 int,
	@param1 varchar(50),
	@param2 varchar(6),
	@param3 bit,
	@param4 bit

AS BEGIN

INSERT INTO [keyser].[dbo].[dbmarchivos] (
	[num_archivo], 
	[nom_archivo], 
	[cve_tipo_archivo], 
	[backres], 
	[lockeo_registros]) 
VALUES ( 
	@param0,
	@param1,
	@param2,
	@param3,
	@param4)

END
