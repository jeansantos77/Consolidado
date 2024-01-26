CREATE DATABASE dbConsolidados
GO

USE dbConsolidados
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [LactoConsolidados] (
    [Id] int NOT NULL IDENTITY,
    [Data] datetime2 NOT NULL,
    [Creditos] decimal(18,2) NOT NULL,
    [Debitos] decimal(18,2) NOT NULL,
    [Saldo] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_LactoConsolidados] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240126214652_InitialCreate', N'5.0.17');
GO

COMMIT;
GO
