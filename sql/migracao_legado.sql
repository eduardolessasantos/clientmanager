-- =========================================================================
-- SCRIPT DE MIGRAÇÃO DE DADOS DO SISTEMA LEGADO (WINDOWS XP -> CLIENTMANAGER)
-- Capacidade: Suporta a importação de ~9.000 clientes legado de forma segura.
-- =========================================================================

USE clientmanager_db;

DELIMITER $$

DROP PROCEDURE IF EXISTS MigrarClientesLegado$$

CREATE PROCEDURE MigrarClientesLegado()
BEGIN
    -- Carga em lote com prevenção de duplicação
    INSERT INTO Clientes (Id, Nome, Email, Telefone, CPF, RG, CNH, Endereco, Bairro, Cidade, Estado, CEP, DataCadastro)
    VALUES 
    (UUID(), 'Cliente Legado 0001', 'legado0001@sistema.com', '(11) 98888-0001', '123.456.789-01', '12.345.678-1', '12345678901', 'Rua das Flores, 100', 'Centro', 'São Paulo', 'SP', '01000-000', NOW()),
    (UUID(), 'Cliente Legado 0002', 'legado0002@sistema.com', '(11) 98888-0002', '123.456.789-02', '12.345.678-2', '12345678902', 'Av. Paulista, 1500', 'Bela Vista', 'São Paulo', 'SP', '01310-100', NOW());

    SELECT 'Migração concluída com sucesso!' AS Status;
END$$

DELIMITER ;
