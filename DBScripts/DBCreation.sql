/****** Object:  Database [bd2-msft-project]    Script Date: 11/24/2020 11:09:31 AM ******/
CREATE DATABASE [bd2-msft-project]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [bd2-msft-project] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [bd2-msft-project] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [bd2-msft-project] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [bd2-msft-project] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [bd2-msft-project] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [bd2-msft-project] SET ARITHABORT OFF 
GO
ALTER DATABASE [bd2-msft-project] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [bd2-msft-project] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [bd2-msft-project] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [bd2-msft-project] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [bd2-msft-project] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [bd2-msft-project] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [bd2-msft-project] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [bd2-msft-project] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [bd2-msft-project] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [bd2-msft-project] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [bd2-msft-project] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [bd2-msft-project] SET  MULTI_USER 
GO
ALTER DATABASE [bd2-msft-project] SET ENCRYPTION ON
GO
ALTER DATABASE [bd2-msft-project] SET QUERY_STORE = ON
GO
ALTER DATABASE [bd2-msft-project] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  UserDefinedFunction [dbo].[uf_CanEdit]    Script Date: 11/24/2020 11:09:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
CREATE FUNCTION [dbo].[uf_CanEdit]
(
    @UserID int
)
RETURNS int
AS
BEGIN
    -- Declare the return variable here
    IF EXISTS (SELECT 1 FROM [dbo].[UserRole] UR INNER JOIN [dbo].[StoreRole] SR ON SR.[ID] = UR.[RoleID] WHERE SR.[RoleName] IN ('Admin', 'Editor') AND UR.[UserID] = @UserID)
		RETURN 1
	
	RETURN 0
END
GO
/****** Object:  Table [dbo].[LoginData]    Script Date: 11/24/2020 11:09:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginData](
	[LoginName] [nvarchar](255) NULL,
	[LoginPassword] [nvarchar](255) NULL,
	[UserID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 11/24/2020 11:09:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[QuantityOrdered] [int] NULL,
	[UnitPrice] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderHeader]    Script Date: 11/24/2020 11:09:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderHeader](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[TransactionDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/24/2020 11:09:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](255) NULL,
	[ProductDescription] [nvarchar](255) NULL,
	[DateAdded] [date] NULL,
	[RemainingStock] [int] NULL,
	[UnitPrice] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreRole]    Script Date: 11/24/2020 11:09:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](255) NULL,
	[RoleDescription] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserData]    Script Date: 11/24/2020 11:09:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[BirthDay] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 11/24/2020 11:09:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserID] [int] NULL,
	[RoleID] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderHeader] ADD  DEFAULT (getdate()) FOR [TransactionDate]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[LoginData]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[UserData] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[OrderHeader] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[OrderHeader]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[UserData] ([ID])
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD FOREIGN KEY([RoleID])
REFERENCES [dbo].[StoreRole] ([ID])
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[UserData] ([ID])
GO
/****** Object:  StoredProcedure [dbo].[usp_LoginOrRegister]    Script Date: 11/24/2020 11:09:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_LoginOrRegister] @UserName NVARCHAR(50)
	,@PassWord VARCHAR(255)
	,@StatusCode INT OUT
AS
BEGIN
	DECLARE @ActualPassword NVarchar(255) = NULL;

	SELECT @ActualPassword = LoginPassword 
	FROM [dbo].[LoginData] 
	WHERE [LoginName] = @UserName;

	IF @ActualPassword IS NOT NULL
		IF @ActualPassword = @PassWord
		BEGIN
			SET @StatusCode = 0
			RETURN
		END
		ELSE
		BEGIN
			SET @StatusCode = 1
			RETURN
		END

	DECLARE @OutputTbl TABLE (ID INT);

	INSERT INTO UserData
	OUTPUT inserted.ID INTO @OutputTbl
	VALUES
	(
		'',
		'',
		'',
		GETDATE()
	);

	DECLARE @NewUserID int;
	SELECT TOP 1 @NewUserID = [ID]
	FROM @OutputTbl;

	INSERT INTO LoginData
	VALUES
	(
		@UserName,
		@PassWord,
		@NewUserID
	);
END
GO
/****** Object:  Trigger [dbo].[NewUserRoleAssignment]    Script Date: 11/24/2020 11:09:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[NewUserRoleAssignment] ON [dbo].[UserData]
AFTER INSERT
AS
BEGIN
	DECLARE @UserRoleID int;

	SELECT @UserRoleID = [ID]
	FROM [dbo].[StoreRole]
	WHERE [RoleName] = 'User';

	INSERT INTO [dbo].[UserRole]
	SELECT i.[ID], @UserRoleID
	FROM inserted i;
END
GO
ALTER TABLE [dbo].[UserData] ENABLE TRIGGER [NewUserRoleAssignment]
GO
ALTER DATABASE [bd2-msft-project] SET  READ_WRITE 
GO
