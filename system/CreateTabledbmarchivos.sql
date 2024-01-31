CREATE TABLE [keyser].[dbo].[dbmarchivos](
	[num_archivo] [int] NOT NULL,
	[nom_archivo] [varchar](50) NULL,
	[cve_tipo_archivo] [varchar](6) NULL,
	[backres] [bit] NULL,
	[lockeo_registros] [bit] NULL,
 CONSTRAINT [PK_dbmarchivos] PRIMARY KEY CLUSTERED 
(
	[num_archivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


