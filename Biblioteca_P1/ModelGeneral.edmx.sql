
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/23/2018 18:21:20
-- Generated from EDMX file: C:\Users\manol\source\repos\Biblioteca_P1\Biblioteca_P1\ModelGeneral.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BTEST3];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AutorId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CARTE] DROP CONSTRAINT [FK_AutorId];
GO
IF OBJECT_ID(N'[dbo].[FK_CarteId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IMPRUMUT] DROP CONSTRAINT [FK_CarteId];
GO
IF OBJECT_ID(N'[dbo].[FK_GenId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CARTE] DROP CONSTRAINT [FK_GenId];
GO
IF OBJECT_ID(N'[dbo].[FK_CititorId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IMPRUMUT] DROP CONSTRAINT [FK_CititorId];
GO
IF OBJECT_ID(N'[dbo].[FK_ImprumutId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[REVIEW] DROP CONSTRAINT [FK_ImprumutId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AUTOR]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AUTOR];
GO
IF OBJECT_ID(N'[dbo].[CARTE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CARTE];
GO
IF OBJECT_ID(N'[dbo].[CITITOR]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CITITOR];
GO
IF OBJECT_ID(N'[dbo].[GEN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GEN];
GO
IF OBJECT_ID(N'[dbo].[IMPRUMUT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IMPRUMUT];
GO
IF OBJECT_ID(N'[dbo].[REVIEW]', 'U') IS NOT NULL
    DROP TABLE [dbo].[REVIEW];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AUTOR'
CREATE TABLE [dbo].[AUTOR] (
    [AutorId] int IDENTITY(1,1) NOT NULL,
    [Nume] char(20)  NULL,
    [Prenume] char(20)  NULL
);
GO

-- Creating table 'CARTE'
CREATE TABLE [dbo].[CARTE] (
    [CarteId] int IDENTITY(1,1) NOT NULL,
    [AutorId] int  NULL,
    [Titlu] char(50)  NULL,
    [GenId] int  NULL
);
GO

-- Creating table 'CITITOR'
CREATE TABLE [dbo].[CITITOR] (
    [CititorId] int IDENTITY(1,1) NOT NULL,
    [Nume] char(15)  NULL,
    [Prenume] char(15)  NULL,
    [Adresa] char(50)  NULL,
    [Email] char(50)  NULL,
    [Stare] binary(16)  NULL
);
GO

-- Creating table 'GEN'
CREATE TABLE [dbo].[GEN] (
    [GenId] int IDENTITY(1,1) NOT NULL,
    [Descriere] char(50)  NULL
);
GO

-- Creating table 'IMPRUMUT'
CREATE TABLE [dbo].[IMPRUMUT] (
    [ImprumutId] int IDENTITY(1,1) NOT NULL,
    [CarteId] int  NULL,
    [CititorId] int  NULL,
    [DataImprumut] datetime  NULL,
    [DataScadenta] datetime  NULL,
    [DataRestituire] datetime  NULL
);
GO

-- Creating table 'REVIEW'
CREATE TABLE [dbo].[REVIEW] (
    [ReviewId] int IDENTITY(1,1) NOT NULL,
    [ImprumutId] int  NULL,
    [Text] varchar(4096)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AutorId] in table 'AUTOR'
ALTER TABLE [dbo].[AUTOR]
ADD CONSTRAINT [PK_AUTOR]
    PRIMARY KEY CLUSTERED ([AutorId] ASC);
GO

-- Creating primary key on [CarteId] in table 'CARTE'
ALTER TABLE [dbo].[CARTE]
ADD CONSTRAINT [PK_CARTE]
    PRIMARY KEY CLUSTERED ([CarteId] ASC);
GO

-- Creating primary key on [CititorId] in table 'CITITOR'
ALTER TABLE [dbo].[CITITOR]
ADD CONSTRAINT [PK_CITITOR]
    PRIMARY KEY CLUSTERED ([CititorId] ASC);
GO

-- Creating primary key on [GenId] in table 'GEN'
ALTER TABLE [dbo].[GEN]
ADD CONSTRAINT [PK_GEN]
    PRIMARY KEY CLUSTERED ([GenId] ASC);
GO

-- Creating primary key on [ImprumutId] in table 'IMPRUMUT'
ALTER TABLE [dbo].[IMPRUMUT]
ADD CONSTRAINT [PK_IMPRUMUT]
    PRIMARY KEY CLUSTERED ([ImprumutId] ASC);
GO

-- Creating primary key on [ReviewId] in table 'REVIEW'
ALTER TABLE [dbo].[REVIEW]
ADD CONSTRAINT [PK_REVIEW]
    PRIMARY KEY CLUSTERED ([ReviewId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AutorId] in table 'CARTE'
ALTER TABLE [dbo].[CARTE]
ADD CONSTRAINT [FK_AutorId]
    FOREIGN KEY ([AutorId])
    REFERENCES [dbo].[AUTOR]
        ([AutorId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AutorId'
CREATE INDEX [IX_FK_AutorId]
ON [dbo].[CARTE]
    ([AutorId]);
GO

-- Creating foreign key on [CarteId] in table 'IMPRUMUT'
ALTER TABLE [dbo].[IMPRUMUT]
ADD CONSTRAINT [FK_CarteId]
    FOREIGN KEY ([CarteId])
    REFERENCES [dbo].[CARTE]
        ([CarteId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarteId'
CREATE INDEX [IX_FK_CarteId]
ON [dbo].[IMPRUMUT]
    ([CarteId]);
GO

-- Creating foreign key on [GenId] in table 'CARTE'
ALTER TABLE [dbo].[CARTE]
ADD CONSTRAINT [FK_GenId]
    FOREIGN KEY ([GenId])
    REFERENCES [dbo].[GEN]
        ([GenId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GenId'
CREATE INDEX [IX_FK_GenId]
ON [dbo].[CARTE]
    ([GenId]);
GO

-- Creating foreign key on [CititorId] in table 'IMPRUMUT'
ALTER TABLE [dbo].[IMPRUMUT]
ADD CONSTRAINT [FK_CititorId]
    FOREIGN KEY ([CititorId])
    REFERENCES [dbo].[CITITOR]
        ([CititorId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CititorId'
CREATE INDEX [IX_FK_CititorId]
ON [dbo].[IMPRUMUT]
    ([CititorId]);
GO

-- Creating foreign key on [ImprumutId] in table 'REVIEW'
ALTER TABLE [dbo].[REVIEW]
ADD CONSTRAINT [FK_ImprumutId]
    FOREIGN KEY ([ImprumutId])
    REFERENCES [dbo].[IMPRUMUT]
        ([ImprumutId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImprumutId'
CREATE INDEX [IX_FK_ImprumutId]
ON [dbo].[REVIEW]
    ([ImprumutId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------