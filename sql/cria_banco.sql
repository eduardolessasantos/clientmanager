CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Categorias` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Nome` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Descricao` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Categorias` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Clientes` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Nome` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Email` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `Telefone` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `CPFCNPJ` varchar(20) CHARACTER SET utf8mb4 NULL,
    `DataCadastro` datetime(6) NOT NULL,
    CONSTRAINT `PK_Clientes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Usuarios` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Nome` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Email` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `SenhaHash` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
    `Perfil` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Ativo` tinyint(1) NOT NULL,
    `DataCriacao` datetime(6) NOT NULL,
    CONSTRAINT `PK_Usuarios` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Projetos` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Titulo` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `Descricao` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Orcamento` decimal(18,2) NOT NULL,
    `DataInicio` datetime(6) NOT NULL,
    `DataFim` datetime(6) NULL,
    `ClienteId` char(36) COLLATE ascii_general_ci NULL,
    `CategoriaId` char(36) COLLATE ascii_general_ci NULL,
    CONSTRAINT `PK_Projetos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Projetos_Categorias_CategoriaId` FOREIGN KEY (`CategoriaId`) REFERENCES `Categorias` (`Id`) ON DELETE SET NULL,
    CONSTRAINT `FK_Projetos_Clientes_ClienteId` FOREIGN KEY (`ClienteId`) REFERENCES `Clientes` (`Id`) ON DELETE SET NULL
) CHARACTER SET=utf8mb4;

CREATE TABLE `Propostas` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `ProjetoId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Valor` decimal(18,2) NOT NULL,
    `Descricao` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `DataEnvio` datetime(6) NOT NULL,
    `DataValidade` datetime(6) NOT NULL,
    CONSTRAINT `PK_Propostas` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Propostas_Projetos_ProjetoId` FOREIGN KEY (`ProjetoId`) REFERENCES `Projetos` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_Projetos_CategoriaId` ON `Projetos` (`CategoriaId`);

CREATE INDEX `IX_Projetos_ClienteId` ON `Projetos` (`ClienteId`);

CREATE INDEX `IX_Propostas_ProjetoId` ON `Propostas` (`ProjetoId`);

CREATE UNIQUE INDEX `IX_Usuarios_Email` ON `Usuarios` (`Email`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260826174923_InitialCreate', '8.0.13');

COMMIT;

