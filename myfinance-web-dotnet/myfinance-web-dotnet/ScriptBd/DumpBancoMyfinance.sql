/*
 Navicat Premium Data Transfer

 Source Server         : SQL_LOCAL
 Source Server Type    : SQL Server
 Source Server Version : 15002000 (15.00.2000)
 Source Host           : DESKTOP-UQJ98U7:1433
 Source Catalog        : myfinance
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000 (15.00.2000)
 File Encoding         : 65001

 Date: 11/12/2024 12:43:18
*/


-- ----------------------------
-- Table structure for HistoricoTransacoes
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[HistoricoTransacoes]') AND type IN ('U'))
	DROP TABLE [dbo].[HistoricoTransacoes]
GO

CREATE TABLE [dbo].[HistoricoTransacoes] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [PlanoContasId] int  NOT NULL,
  [descricao] varchar(2000) COLLATE Latin1_General_CI_AS  NOT NULL,
  [Data] datetime  NOT NULL,
  [Valor] decimal(10,2)  NOT NULL
)
GO

ALTER TABLE [dbo].[HistoricoTransacoes] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of HistoricoTransacoes
-- ----------------------------
SET IDENTITY_INSERT [dbo].[HistoricoTransacoes] ON
GO

INSERT INTO [dbo].[HistoricoTransacoes] ([id], [PlanoContasId], [descricao], [Data], [Valor]) VALUES (N'1015', N'11', N'Recebimento Salario', N'2024-12-05 17:05:00.000', N'1400.00')
GO

INSERT INTO [dbo].[HistoricoTransacoes] ([id], [PlanoContasId], [descricao], [Data], [Valor]) VALUES (N'1016', N'12', N'Internet', N'2024-12-05 17:05:00.000', N'150.00')
GO

INSERT INTO [dbo].[HistoricoTransacoes] ([id], [PlanoContasId], [descricao], [Data], [Valor]) VALUES (N'1017', N'13', N'Compras Mensais', N'2024-12-06 17:05:00.000', N'600.00')
GO

INSERT INTO [dbo].[HistoricoTransacoes] ([id], [PlanoContasId], [descricao], [Data], [Valor]) VALUES (N'1018', N'12', N'Luz', N'2024-12-06 17:06:00.000', N'300.00')
GO

INSERT INTO [dbo].[HistoricoTransacoes] ([id], [PlanoContasId], [descricao], [Data], [Valor]) VALUES (N'1019', N'12', N'Agua', N'2024-12-06 17:06:00.000', N'100.00')
GO

INSERT INTO [dbo].[HistoricoTransacoes] ([id], [PlanoContasId], [descricao], [Data], [Valor]) VALUES (N'1020', N'13', N'Feira', N'2024-12-05 17:06:00.000', N'50.00')
GO

SET IDENTITY_INSERT [dbo].[HistoricoTransacoes] OFF
GO


-- ----------------------------
-- Table structure for PlanoContas
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[PlanoContas]') AND type IN ('U'))
	DROP TABLE [dbo].[PlanoContas]
GO

CREATE TABLE [dbo].[PlanoContas] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [descricao] varchar(1000) COLLATE Latin1_General_CI_AS  NOT NULL,
  [tipo] varchar(1) COLLATE Latin1_General_CI_AS  NOT NULL,
  [Ativo] bit  NOT NULL
)
GO

ALTER TABLE [dbo].[PlanoContas] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of PlanoContas
-- ----------------------------
SET IDENTITY_INSERT [dbo].[PlanoContas] ON
GO

INSERT INTO [dbo].[PlanoContas] ([id], [descricao], [tipo], [Ativo]) VALUES (N'11', N'Salario', N'R', N'1')
GO

INSERT INTO [dbo].[PlanoContas] ([id], [descricao], [tipo], [Ativo]) VALUES (N'12', N'Casa', N'D', N'1')
GO

INSERT INTO [dbo].[PlanoContas] ([id], [descricao], [tipo], [Ativo]) VALUES (N'13', N'Mercado', N'D', N'1')
GO

SET IDENTITY_INSERT [dbo].[PlanoContas] OFF
GO


-- ----------------------------
-- Auto increment value for HistoricoTransacoes
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[HistoricoTransacoes]', RESEED, 1020)
GO


-- ----------------------------
-- Indexes structure for table HistoricoTransacoes
-- ----------------------------
CREATE NONCLUSTERED INDEX [idx_HistoricoTransacoes]
ON [dbo].[HistoricoTransacoes] (
  [id] ASC
)
GO


-- ----------------------------
-- Auto increment value for PlanoContas
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[PlanoContas]', RESEED, 13)
GO


-- ----------------------------
-- Primary Key structure for table PlanoContas
-- ----------------------------
ALTER TABLE [dbo].[PlanoContas] ADD CONSTRAINT [PK__PlanoCon__3213E83F1CC4325B] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table HistoricoTransacoes
-- ----------------------------
ALTER TABLE [dbo].[HistoricoTransacoes] ADD CONSTRAINT [fk_planoContasId] FOREIGN KEY ([PlanoContasId]) REFERENCES [dbo].[PlanoContas] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

