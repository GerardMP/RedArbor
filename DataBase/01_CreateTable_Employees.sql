/****** Object:  Table [dbo].[Employees] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Employees'))
	CREATE TABLE [dbo].[Employees](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[CompanyId] [int] NOT NULL,
		[CreatedOn] [datetime] NULL,
		[DeletedOn] [datetime] NULL,
		[Email] [nvarchar](100) NULL,
		[Fax] [nvarchar](15) NULL,
		[Name] [nvarchar](100) NULL,
		[LastLogin] [datetime] NULL,
		[Password] [nvarchar](50) NOT NULL,
		[PortalId] [int] NOT NULL,
		[RoleId] [int] NOT NULL,
		[StatusId] [int] NOT NULL,
		[Telephone] [nvarchar](15) NULL,
		[UpdatedOn] [datetime] NULL,
		[Username] [nvarchar](50) NOT NULL,
	 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC)
		WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	 CONSTRAINT [UK_Employees_Username] UNIQUE NONCLUSTERED ([Username] ASC)
		WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
GO


