CREATE TABLE [keyser].[dbo].[dbmdiccionario](
	[cve_base_datos] [varchar](50) NOT NULL,
	[nom_tabla] [varchar](50) NOT NULL,
	[num_ordinal] [tinyint] NOT NULL,
	[nom_campo] [varchar](30) NOT NULL,
	[cve_tipo_datos] [varchar](50) NOT NULL,
	[tam] [int] NOT NULL,
	[marca] [bit] NULL,
 CONSTRAINT [PK_dbmdiccionario] PRIMARY KEY CLUSTERED 
(
	[cve_base_datos] ASC,
	[nom_tabla] ASC,
	[num_ordinal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

