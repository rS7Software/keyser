CREATE TABLE [keyser].[dbo].[dbmbases_datos](
	[cve_base_datos] [varchar](50) NOT NULL,
	[nombre_base_datos] [varchar](50) NULL,
	[dbms] [varchar](50) NULL,
	[enlinea] [bit] NULL,
 CONSTRAINT [PK_dbmbases_datos] PRIMARY KEY CLUSTERED 
(
	[cve_base_datos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


