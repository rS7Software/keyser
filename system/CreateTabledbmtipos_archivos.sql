CREATE TABLE [keyser].[dbo].[dbmtipos_archivos](
	[cve_tipo_archivo] [varchar](6) NOT NULL,
	[des_tipo_archivo] [varchar](50) NULL,
 CONSTRAINT [PK_dbmtipos_archivos] PRIMARY KEY CLUSTERED 
(
	[cve_tipo_archivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
