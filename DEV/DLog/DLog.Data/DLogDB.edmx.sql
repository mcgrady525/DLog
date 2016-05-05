
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/30/2015 17:15:56
-- Generated from EDMX file: D:\sources.github\DLog\DEV\DLog\DLog.Data\DLogDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DLogDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ErrorLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ErrorLog];
GO
IF OBJECT_ID(N'[dbo].[PerfLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PerfLog];
GO
IF OBJECT_ID(N'[dbo].[SearchInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SearchInfo];
GO
IF OBJECT_ID(N'[dbo].[DebugLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DebugLog];
GO
IF OBJECT_ID(N'[dbo].[CommonConfig]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommonConfig];
GO
IF OBJECT_ID(N'[dbo].[PerfLogSearchInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PerfLogSearchInfo];
GO
IF OBJECT_ID(N'[dbo].[Resource]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Resource];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ErrorLog'
CREATE TABLE [dbo].[ErrorLog] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [ThreadID] int  NOT NULL,
    [SystemCode] nvarchar(64)  NOT NULL,
    [Source] nvarchar(64)  NOT NULL,
    [Message] nvarchar(512)  NOT NULL,
    [MachineName] nvarchar(64)  NULL,
    [AppDomainName] nvarchar(1024)  NOT NULL,
    [ProcessID] int  NOT NULL,
    [ProcessName] nvarchar(1024)  NOT NULL,
    [ThreadName] nvarchar(1024)  NULL,
    [Detail] varbinary(max)  NULL,
    [CreateTime] datetime  NOT NULL,
    [IpAddress] nvarchar(256)  NULL
);
GO

-- Creating table 'PerfLog'
CREATE TABLE [dbo].[PerfLog] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [ThreadID] int  NOT NULL,
    [SystemCode] nvarchar(64)  NOT NULL,
    [Source] nvarchar(64)  NOT NULL,
    [ClassName] nvarchar(64)  NOT NULL,
    [MethodName] nvarchar(64)  NOT NULL,
    [Duration] bigint  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [MachineName] nvarchar(64)  NULL,
    [CreateTime] datetime  NOT NULL
);
GO

-- Creating table 'SearchInfo'
CREATE TABLE [dbo].[SearchInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SystemCode] nvarchar(64)  NOT NULL,
    [Source] nvarchar(64)  NOT NULL
);
GO

-- Creating table 'DebugLog'
CREATE TABLE [dbo].[DebugLog] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [ThreadID] int  NOT NULL,
    [SystemCode] nvarchar(64)  NOT NULL,
    [Source] nvarchar(64)  NOT NULL,
    [Message] nvarchar(512)  NOT NULL,
    [MachineName] nvarchar(64)  NULL,
    [AppDomainName] nvarchar(1024)  NOT NULL,
    [ProcessID] int  NOT NULL,
    [ProcessName] nvarchar(1024)  NOT NULL,
    [ThreadName] nvarchar(1024)  NULL,
    [Detail] varbinary(max)  NULL,
    [CreateTime] datetime  NOT NULL,
    [IpAddress] nvarchar(256)  NULL
);
GO

-- Creating table 'CommonConfig'
CREATE TABLE [dbo].[CommonConfig] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NULL,
    [LastUpdateTime] datetime  NOT NULL,
    [LastUpdateUser] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'PerfLogSearchInfo'
CREATE TABLE [dbo].[PerfLogSearchInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SystemCode] nvarchar(64)  NOT NULL,
    [Source] nvarchar(256)  NOT NULL,
    [ClassName] nvarchar(128)  NOT NULL,
    [MethodName] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Resource'
CREATE TABLE [dbo].[Resource] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Type] int  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'ErrorLog'
ALTER TABLE [dbo].[ErrorLog]
ADD CONSTRAINT [PK_ErrorLog]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PerfLog'
ALTER TABLE [dbo].[PerfLog]
ADD CONSTRAINT [PK_PerfLog]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SearchInfo'
ALTER TABLE [dbo].[SearchInfo]
ADD CONSTRAINT [PK_SearchInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'DebugLog'
ALTER TABLE [dbo].[DebugLog]
ADD CONSTRAINT [PK_DebugLog]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CommonConfig'
ALTER TABLE [dbo].[CommonConfig]
ADD CONSTRAINT [PK_CommonConfig]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PerfLogSearchInfo'
ALTER TABLE [dbo].[PerfLogSearchInfo]
ADD CONSTRAINT [PK_PerfLogSearchInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Resource'
ALTER TABLE [dbo].[Resource]
ADD CONSTRAINT [PK_Resource]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------