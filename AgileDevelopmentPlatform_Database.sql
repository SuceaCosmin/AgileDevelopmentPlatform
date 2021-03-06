USE [master]
GO
/****** Object:  Database [AgileDevelopmentDatabase]    Script Date: 10/28/2018 7:33:45 PM ******/
CREATE DATABASE [AgileDevelopmentDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AgileDevelopmentDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\AgileDevelopmentDatabase.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AgileDevelopmentDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\AgileDevelopmentDatabase_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AgileDevelopmentDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
USE [AgileDevelopmentDatabase]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 10/28/2018 7:33:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](50) NOT NULL,
	[OwnerId] [nvarchar](128) NOT NULL,
	[IsDeleted] [int] NOT NULL,
 CONSTRAINT [PK_ProjectTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 10/28/2018 7:33:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sprint]    Script Date: 10/28/2018 7:33:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sprint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[TargetDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sprint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Task]    Script Date: 10/28/2018 7:33:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ProjectId] [int] NOT NULL,
	[TaskInitiatorId] [nvarchar](128) NOT NULL,
	[ResponsibleUserId] [nvarchar](128) NULL,
	[SprintId] [int] NULL,
	[Priority] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[WorkEffort] [int] NULL,
	[EffortEstimation] [int] NULL,
	[TaskDificultyId] [int] NULL,
	[TaskCreationDate] [date] NULL,
	[TaskCompletionDate] [date] NULL,
 CONSTRAINT [PK_TaskTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskDificultyLevel]    Script Date: 10/28/2018 7:33:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskDificultyLevel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ContributionPoints] [int] NOT NULL,
 CONSTRAINT [PK_TaskDificultyLevel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 10/28/2018 7:33:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserAccess]    Script Date: 10/28/2018 7:33:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccess](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_UserAccess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 10/28/2018 7:33:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 10/28/2018 7:33:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 10/28/2018 7:33:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Project] ON 

INSERT [dbo].[Project] ([Id], [ProjectName], [OwnerId], [IsDeleted]) VALUES (3, N'Desktop application', N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', 0)
INSERT [dbo].[Project] ([Id], [ProjectName], [OwnerId], [IsDeleted]) VALUES (4, N'Some Project', N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', 1)
INSERT [dbo].[Project] ([Id], [ProjectName], [OwnerId], [IsDeleted]) VALUES (8, N'John''s project', N'3b987c30-9e40-4156-9b8f-7936f5f8556b', 0)
INSERT [dbo].[Project] ([Id], [ProjectName], [OwnerId], [IsDeleted]) VALUES (9, N'Susan''s project', N'82fd269b-5b02-473b-87a5-a2f0188d3ba7', 0)
INSERT [dbo].[Project] ([Id], [ProjectName], [OwnerId], [IsDeleted]) VALUES (10, N'Some project', N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', 1)
SET IDENTITY_INSERT [dbo].[Project] OFF
SET IDENTITY_INSERT [dbo].[Sprint] ON 

INSERT [dbo].[Sprint] ([Id], [Name], [ProjectId], [TargetDate]) VALUES (17, N'Sprint', 4, N'asd')
INSERT [dbo].[Sprint] ([Id], [Name], [ProjectId], [TargetDate]) VALUES (20, N'Initial sprint', 3, N'20/08/2018')
INSERT [dbo].[Sprint] ([Id], [Name], [ProjectId], [TargetDate]) VALUES (22, N'2nd Sprint', 3, N'20/08/2018')
SET IDENTITY_INSERT [dbo].[Sprint] OFF
SET IDENTITY_INSERT [dbo].[Task] ON 

INSERT [dbo].[Task] ([Id], [Name], [Description], [ProjectId], [TaskInitiatorId], [ResponsibleUserId], [SprintId], [Priority], [Status], [WorkEffort], [EffortEstimation], [TaskDificultyId], [TaskCreationDate], [TaskCompletionDate]) VALUES (36, N'Create database design', N'desing', 3, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', N'3b987c30-9e40-4156-9b8f-7936f5f8556b', 20, N'Medium', N'Finished', 35, 50, 2, CAST(N'2018-06-18' AS Date), CAST(N'2018-06-30' AS Date))
INSERT [dbo].[Task] ([Id], [Name], [Description], [ProjectId], [TaskInitiatorId], [ResponsibleUserId], [SprintId], [Priority], [Status], [WorkEffort], [EffortEstimation], [TaskDificultyId], [TaskCreationDate], [TaskCompletionDate]) VALUES (37, N'Create UI mockups', N'UI mockups have to be created in order to validate the layout', 3, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', N'3b987c30-9e40-4156-9b8f-7936f5f8556b', 20, N'High', N'Working', 10, 16, 2, CAST(N'2018-06-19' AS Date), CAST(N'2018-06-30' AS Date))
INSERT [dbo].[Task] ([Id], [Name], [Description], [ProjectId], [TaskInitiatorId], [ResponsibleUserId], [SprintId], [Priority], [Status], [WorkEffort], [EffortEstimation], [TaskDificultyId], [TaskCreationDate], [TaskCompletionDate]) VALUES (38, N'Enviorment setup', N'Install required tools for development', 3, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', N'6a8ef7f1-e6f9-4374-9381-8d32c5275833', 20, N'Medium', N'Finished', 10, 15, 1, CAST(N'2018-06-19' AS Date), CAST(N'2018-06-30' AS Date))
INSERT [dbo].[Task] ([Id], [Name], [Description], [ProjectId], [TaskInitiatorId], [ResponsibleUserId], [SprintId], [Priority], [Status], [WorkEffort], [EffortEstimation], [TaskDificultyId], [TaskCreationDate], [TaskCompletionDate]) VALUES (40, N'Create system  UML diagrams', N'UML diagrams shall highlight the  interaction between user and  application', 3, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', N'82fd269b-5b02-473b-87a5-a2f0188d3ba7', 20, N'Medium', N'Open', 0, 24, 2, CAST(N'2018-06-24' AS Date), CAST(N'0001-01-01' AS Date))
INSERT [dbo].[Task] ([Id], [Name], [Description], [ProjectId], [TaskInitiatorId], [ResponsibleUserId], [SprintId], [Priority], [Status], [WorkEffort], [EffortEstimation], [TaskDificultyId], [TaskCreationDate], [TaskCompletionDate]) VALUES (41, N'Clarify application input files', N'A list of the application input files shall be  available  in order to develop handles for each of them', 3, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', N'3b987c30-9e40-4156-9b8f-7936f5f8556b', 20, N'Medium', N'Working', 2, 8, 3, CAST(N'2018-06-24' AS Date), CAST(N'0001-01-01' AS Date))
INSERT [dbo].[Task] ([Id], [Name], [Description], [ProjectId], [TaskInitiatorId], [ResponsibleUserId], [SprintId], [Priority], [Status], [WorkEffort], [EffortEstimation], [TaskDificultyId], [TaskCreationDate], [TaskCompletionDate]) VALUES (42, N'Develop handlers for the input files.', N'Develop import/export  functionality for the identified files. ', 3, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', N'3b987c30-9e40-4156-9b8f-7936f5f8556b', 20, N'Medium', N'Open', 0, 24, 1, CAST(N'2018-06-24' AS Date), CAST(N'0001-01-01' AS Date))
INSERT [dbo].[Task] ([Id], [Name], [Description], [ProjectId], [TaskInitiatorId], [ResponsibleUserId], [SprintId], [Priority], [Status], [WorkEffort], [EffortEstimation], [TaskDificultyId], [TaskCreationDate], [TaskCompletionDate]) VALUES (43, N'Integrate logging  framework', N'A logging framework shall be used to development/debug and configuration activity tracking.', 3, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', NULL, N'Medium', N'Open', 0, 18, 2, CAST(N'2018-06-24' AS Date), NULL)
INSERT [dbo].[Task] ([Id], [Name], [Description], [ProjectId], [TaskInitiatorId], [ResponsibleUserId], [SprintId], [Priority], [Status], [WorkEffort], [EffortEstimation], [TaskDificultyId], [TaskCreationDate], [TaskCompletionDate]) VALUES (44, N'Create user interface', N'The applications user interface shall be  implemented according to the validate mockups.', 3, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', 20, N'High', N'Open', 0, 80, 2, CAST(N'2018-06-24' AS Date), CAST(N'0001-01-01' AS Date))
INSERT [dbo].[Task] ([Id], [Name], [Description], [ProjectId], [TaskInitiatorId], [ResponsibleUserId], [SprintId], [Priority], [Status], [WorkEffort], [EffortEstimation], [TaskDificultyId], [TaskCreationDate], [TaskCompletionDate]) VALUES (45, N'Update import functionality', N'Multiple  file formats should be supported for import functionality', 3, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', N'82fd269b-5b02-473b-87a5-a2f0188d3ba7', 22, N'Medium', N'Open', 0, 40, 2, CAST(N'2018-06-25' AS Date), CAST(N'0001-01-01' AS Date))
INSERT [dbo].[Task] ([Id], [Name], [Description], [ProjectId], [TaskInitiatorId], [ResponsibleUserId], [SprintId], [Priority], [Status], [WorkEffort], [EffortEstimation], [TaskDificultyId], [TaskCreationDate], [TaskCompletionDate]) VALUES (46, N'Setup enviorment', N'Updated description', 3, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', NULL, N'High', N'Open', 0, 50, 2, CAST(N'2018-07-02' AS Date), CAST(N'0001-01-01' AS Date))
SET IDENTITY_INSERT [dbo].[Task] OFF
SET IDENTITY_INSERT [dbo].[TaskDificultyLevel] ON 

INSERT [dbo].[TaskDificultyLevel] ([Id], [Name], [ContributionPoints]) VALUES (1, N'Easy', 1)
INSERT [dbo].[TaskDificultyLevel] ([Id], [Name], [ContributionPoints]) VALUES (2, N'Medium', 2)
INSERT [dbo].[TaskDificultyLevel] ([Id], [Name], [ContributionPoints]) VALUES (3, N'Hard', 3)
SET IDENTITY_INSERT [dbo].[TaskDificultyLevel] OFF
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe', N'suceacosmin@yahoo.com', 0, N'AAJdaOUTGrZcKtX4o17kFrTbwR6ZcIziAfgWeG6Sb3FmPNgZ96JQVkg54lP9FScncA==', N'ba050c75-1285-4b14-aaac-de2ed733771f', NULL, 0, 0, NULL, 1, 0, N'Cosmin')
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3b728856-f6c7-462d-b1be-a7df94db3c32', N'andrewadams@yahoo.com', 0, N'ADdR9lPIOMl7rcWHRW5cytJ1U9WTL1w5yBUgUENm4m0sPkIdeesklO0Z79tumrMA+Q==', N'15beb1d0-9b53-47a6-9228-e80c30e60205', NULL, 0, 0, NULL, 1, 0, N'Andrew')
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3b987c30-9e40-4156-9b8f-7936f5f8556b', N'JohnMayer@yahoo.com', 0, N'AFq9Fcer5XZ7DCiBuieED1CRN1+/yVJ07bdbgR1b8zVUlLbWCuHWF9vTwma0NHdV0w==', N'fe7ab14e-765b-4607-aa5f-d8db2692140f', NULL, 0, 0, NULL, 1, 0, N'John')
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6a8ef7f1-e6f9-4374-9381-8d32c5275833', N'Alice_Jonson@yahoo.com', 0, N'AKRHh5+idTYFHA6H14KJ7mZW8VCAjUOQuhOMXupfRkTjRGD8S6qO7aTxAK/lGS8rcw==', N'3f9b1fac-49ad-4d02-9142-93ac5e73c04c', NULL, 0, 0, NULL, 1, 0, N'Alice')
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'824fb7fb-c64e-45f1-a86e-47b2a50d2bdc', N'MykeAdle@yahoo.com', 0, N'AFif4xUVWg8lxbpCkuxr+8HzM+39doN14um82pSGn9ENka4oPdNjws2fIDaaPFZk6A==', N'b055ee59-dacc-4885-8ef8-85c9d644dfa3', NULL, 0, 0, NULL, 1, 0, N'Myke')
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'82fd269b-5b02-473b-87a5-a2f0188d3ba7', N'SusanCroft@yahoo.com', 0, N'ALCPWGdCgG6VhbYa5eaOP6L7w2KzgVA3Fx2HjSAWFhNlb6JqPOpJE+0U61VRIYRwUg==', N'7a158611-662a-4bc9-a5a2-beaaa9367f74', NULL, 0, 0, NULL, 1, 0, N'Susan')
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e4d5ca56-58de-47ae-938b-b766e0f3e653', N'theodorethomas@yahoo.com', 0, N'AHk7S6hGBKPy9VPg6RobYDLzwvyzQLqeL/RKzCHrcqazEKveUBQRUd73P5Qrm/bbyw==', N'6aad770e-a3c1-4305-8733-8efe737925b5', NULL, 0, 0, NULL, 1, 0, N'Theodore')
SET IDENTITY_INSERT [dbo].[UserAccess] ON 

INSERT [dbo].[UserAccess] ([Id], [ProjectId], [UserId]) VALUES (6, 3, N'3b987c30-9e40-4156-9b8f-7936f5f8556b')
INSERT [dbo].[UserAccess] ([Id], [ProjectId], [UserId]) VALUES (9, 4, N'82fd269b-5b02-473b-87a5-a2f0188d3ba7')
INSERT [dbo].[UserAccess] ([Id], [ProjectId], [UserId]) VALUES (15, 8, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe')
INSERT [dbo].[UserAccess] ([Id], [ProjectId], [UserId]) VALUES (21, 9, N'82fd269b-5b02-473b-87a5-a2f0188d3ba7')
INSERT [dbo].[UserAccess] ([Id], [ProjectId], [UserId]) VALUES (22, 3, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe')
INSERT [dbo].[UserAccess] ([Id], [ProjectId], [UserId]) VALUES (23, 10, N'0f237ed7-1e3e-4bcc-ae81-43301bc3f8fe')
INSERT [dbo].[UserAccess] ([Id], [ProjectId], [UserId]) VALUES (24, 3, N'82fd269b-5b02-473b-87a5-a2f0188d3ba7')
INSERT [dbo].[UserAccess] ([Id], [ProjectId], [UserId]) VALUES (1018, 3, N'6a8ef7f1-e6f9-4374-9381-8d32c5275833')
SET IDENTITY_INSERT [dbo].[UserAccess] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 10/28/2018 7:33:45 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Roles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 10/28/2018 7:33:45 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[User]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 10/28/2018 7:33:45 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[UserClaim]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 10/28/2018 7:33:45 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[UserLogin]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 10/28/2018 7:33:45 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[UserRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 10/28/2018 7:33:45 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[UserRole]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_User] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_User]
GO
ALTER TABLE [dbo].[Sprint]  WITH CHECK ADD  CONSTRAINT [FK_Sprint_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[Sprint] CHECK CONSTRAINT [FK_Sprint_Project]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Sprint] FOREIGN KEY([SprintId])
REFERENCES [dbo].[Sprint] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Sprint]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_TaskDificultyLevel] FOREIGN KEY([TaskDificultyId])
REFERENCES [dbo].[TaskDificultyLevel] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_TaskDificultyLevel]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_User] FOREIGN KEY([TaskInitiatorId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_User]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_User1] FOREIGN KEY([ResponsibleUserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_User1]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_TaskTable_ProjectTable] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_TaskTable_ProjectTable]
GO
ALTER TABLE [dbo].[UserAccess]  WITH CHECK ADD  CONSTRAINT [FK_UserAccess_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[UserAccess] CHECK CONSTRAINT [FK_UserAccess_Project]
GO
ALTER TABLE [dbo].[UserAccess]  WITH CHECK ADD  CONSTRAINT [FK_UserAccess_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserAccess] CHECK CONSTRAINT [FK_UserAccess_User]
GO
ALTER TABLE [dbo].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_UserClaim_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserClaim] CHECK CONSTRAINT [FK_UserClaim_User]
GO
ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_User]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Roles]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_UserRole] FOREIGN KEY([UserId], [RoleId])
REFERENCES [dbo].[UserRole] ([UserId], [RoleId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_UserRole]
GO
USE [master]
GO
ALTER DATABASE [AgileDevelopmentDatabase] SET  READ_WRITE 
GO
