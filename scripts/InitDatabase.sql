-- =====================================================
-- Finandina API - Script de Inicialización de Base de Datos
-- SQL Server | Tablas + Stored Procedures + Datos Semilla
-- =====================================================

-- 1. Crear base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BankBranchesDb')
BEGIN
    CREATE DATABASE BankBranchesDb;
END
GO

USE BankBranchesDb;
GO

-- =====================================================
-- 2. TABLAS
-- =====================================================

-- Tabla: Users (Autenticación)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        Id          INT IDENTITY(1,1) PRIMARY KEY,
        FullName    NVARCHAR(150) NOT NULL,
        Email       NVARCHAR(256) NOT NULL,
        PasswordHash NVARCHAR(500) NOT NULL,
        Role        NVARCHAR(50) NOT NULL DEFAULT 'Usuario',
        CreatedAt   DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        IsActive    BIT NOT NULL DEFAULT 1,
        CONSTRAINT UQ_Users_Email UNIQUE (Email)
    );
END
GO

-- Tabla: Cities (Ciudades - referencia a regiones de API Colombia)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Cities')
BEGIN
    CREATE TABLE Cities (
        Id          INT IDENTITY(1,1) PRIMARY KEY,
        Name        NVARCHAR(100) NOT NULL,
        RegionId    INT NOT NULL,
        IsActive    BIT NOT NULL DEFAULT 1
    );
END
GO

-- Tabla: Branches (Sucursales - CRUD principal)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Branches')
BEGIN
    CREATE TABLE Branches (
        Id          INT IDENTITY(1,1) PRIMARY KEY,
        Name        NVARCHAR(200) NOT NULL,
        Address     NVARCHAR(300) NOT NULL,
        Phone       NVARCHAR(20) NULL,
        CityId      INT NOT NULL,
        RegionId    INT NOT NULL,
        IsActive    BIT NOT NULL DEFAULT 1,
        CreatedAt   DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_Branches_Cities FOREIGN KEY (CityId) REFERENCES Cities(Id)
    );
END
GO

-- Índices
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Branches_RegionId')
    CREATE INDEX IX_Branches_RegionId ON Branches(RegionId);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Branches_CityId')
    CREATE INDEX IX_Branches_CityId ON Branches(CityId);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Cities_RegionId')
    CREATE INDEX IX_Cities_RegionId ON Cities(RegionId);
GO

-- =====================================================
-- 3. STORED PROCEDURES - Users
-- =====================================================

-- SP: Obtener usuario por Email
IF OBJECT_ID('sp_GetUserByEmail', 'P') IS NOT NULL DROP PROCEDURE sp_GetUserByEmail;
GO
CREATE PROCEDURE sp_GetUserByEmail
    @Email NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, FullName, Email, PasswordHash, Role, CreatedAt, IsActive
    FROM Users
    WHERE Email = @Email AND IsActive = 1;
END
GO

-- SP: Crear usuario
IF OBJECT_ID('sp_CreateUser', 'P') IS NOT NULL DROP PROCEDURE sp_CreateUser;
GO
CREATE PROCEDURE sp_CreateUser
    @FullName     NVARCHAR(150),
    @Email        NVARCHAR(256),
    @PasswordHash NVARCHAR(500),
    @Role         NVARCHAR(50),
    @CreatedAt    DATETIME2,
    @IsActive     BIT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Users (FullName, Email, PasswordHash, Role, CreatedAt, IsActive)
    VALUES (@FullName, @Email, @PasswordHash, @Role, @CreatedAt, @IsActive);

    SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO

-- =====================================================
-- 4. STORED PROCEDURES - Cities
-- =====================================================

-- SP: Obtener todas las ciudades activas
IF OBJECT_ID('sp_GetAllCities', 'P') IS NOT NULL DROP PROCEDURE sp_GetAllCities;
GO
CREATE PROCEDURE sp_GetAllCities
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, RegionId, IsActive
    FROM Cities
    WHERE IsActive = 1
    ORDER BY Name;
END
GO

-- SP: Obtener ciudad por Id
IF OBJECT_ID('sp_GetCityById', 'P') IS NOT NULL DROP PROCEDURE sp_GetCityById;
GO
CREATE PROCEDURE sp_GetCityById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, RegionId, IsActive
    FROM Cities
    WHERE Id = @Id AND IsActive = 1;
END
GO

-- SP: Obtener ciudades por región
IF OBJECT_ID('sp_GetCitiesByRegionId', 'P') IS NOT NULL DROP PROCEDURE sp_GetCitiesByRegionId;
GO
CREATE PROCEDURE sp_GetCitiesByRegionId
    @RegionId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, RegionId, IsActive
    FROM Cities
    WHERE RegionId = @RegionId AND IsActive = 1
    ORDER BY Name;
END
GO

-- =====================================================
-- 5. STORED PROCEDURES - Branches
-- =====================================================

-- SP: Obtener todas las sucursales activas (con JOIN a Cities)
IF OBJECT_ID('sp_GetAllBranches', 'P') IS NOT NULL DROP PROCEDURE sp_GetAllBranches;
GO
CREATE PROCEDURE sp_GetAllBranches
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        b.Id,
        b.Name,
        b.Address,
        b.Phone,
        b.CityId,
        c.Name AS CityName,
        b.RegionId,
        b.IsActive,
        b.CreatedAt
    FROM Branches b
    INNER JOIN Cities c ON b.CityId = c.Id
    WHERE b.IsActive = 1
    ORDER BY b.Name;
END
GO

-- SP: Obtener sucursal por Id
IF OBJECT_ID('sp_GetBranchById', 'P') IS NOT NULL DROP PROCEDURE sp_GetBranchById;
GO
CREATE PROCEDURE sp_GetBranchById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        b.Id,
        b.Name,
        b.Address,
        b.Phone,
        b.CityId,
        c.Name AS CityName,
        b.RegionId,
        b.IsActive,
        b.CreatedAt
    FROM Branches b
    INNER JOIN Cities c ON b.CityId = c.Id
    WHERE b.Id = @Id AND b.IsActive = 1;
END
GO

-- SP: Obtener sucursales por región
IF OBJECT_ID('sp_GetBranchesByRegionId', 'P') IS NOT NULL DROP PROCEDURE sp_GetBranchesByRegionId;
GO
CREATE PROCEDURE sp_GetBranchesByRegionId
    @RegionId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        b.Id,
        b.Name,
        b.Address,
        b.Phone,
        b.CityId,
        c.Name AS CityName,
        b.RegionId,
        b.IsActive,
        b.CreatedAt
    FROM Branches b
    INNER JOIN Cities c ON b.CityId = c.Id
    WHERE b.RegionId = @RegionId AND b.IsActive = 1
    ORDER BY b.Name;
END
GO

-- SP: Obtener sucursales por ciudad
IF OBJECT_ID('sp_GetBranchesByCityId', 'P') IS NOT NULL DROP PROCEDURE sp_GetBranchesByCityId;
GO
CREATE PROCEDURE sp_GetBranchesByCityId
    @CityId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        b.Id,
        b.Name,
        b.Address,
        b.Phone,
        b.CityId,
        c.Name AS CityName,
        b.RegionId,
        b.IsActive,
        b.CreatedAt
    FROM Branches b
    INNER JOIN Cities c ON b.CityId = c.Id
    WHERE b.CityId = @CityId AND b.IsActive = 1
    ORDER BY b.Name;
END
GO

-- SP: Crear sucursal
IF OBJECT_ID('sp_CreateBranch', 'P') IS NOT NULL DROP PROCEDURE sp_CreateBranch;
GO
CREATE PROCEDURE sp_CreateBranch
    @Name       NVARCHAR(200),
    @Address    NVARCHAR(300),
    @Phone      NVARCHAR(20),
    @CityId     INT,
    @RegionId   INT,
    @IsActive   BIT,
    @CreatedAt  DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Branches (Name, Address, Phone, CityId, RegionId, IsActive, CreatedAt)
    VALUES (@Name, @Address, @Phone, @CityId, @RegionId, @IsActive, @CreatedAt);

    SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO

-- SP: Actualizar sucursal
IF OBJECT_ID('sp_UpdateBranch', 'P') IS NOT NULL DROP PROCEDURE sp_UpdateBranch;
GO
CREATE PROCEDURE sp_UpdateBranch
    @Id         INT,
    @Name       NVARCHAR(200),
    @Address    NVARCHAR(300),
    @Phone      NVARCHAR(20),
    @CityId     INT,
    @RegionId   INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Branches
    SET Name = @Name,
        Address = @Address,
        Phone = @Phone,
        CityId = @CityId,
        RegionId = @RegionId
    WHERE Id = @Id AND IsActive = 1;
END
GO

-- SP: Eliminar sucursal (Soft Delete)
IF OBJECT_ID('sp_DeleteBranch', 'P') IS NOT NULL DROP PROCEDURE sp_DeleteBranch;
GO
CREATE PROCEDURE sp_DeleteBranch
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Branches
    SET IsActive = 0
    WHERE Id = @Id AND IsActive = 1;
END
GO

-- =====================================================
-- 6. DATOS SEMILLA - Ciudades principales de Colombia
-- =====================================================
-- RegionId referencia la API Colombia:
-- 1 = Caribe, 2 = Pacífico, 3 = Orinoquía, 4 = Amazonía, 5 = Andina, 6 = Insular

IF NOT EXISTS (SELECT 1 FROM Cities)
BEGIN
    INSERT INTO Cities (Name, RegionId, IsActive) VALUES
    -- Región Caribe (1)
    ('Barranquilla', 1, 1),
    ('Cartagena', 1, 1),
    ('Santa Marta', 1, 1),
    ('Montería', 1, 1),
    ('Valledupar', 1, 1),
    ('Sincelejo', 1, 1),
    ('Riohacha', 1, 1),
    -- Región Pacífico (2)
    ('Cali', 2, 1),
    ('Buenaventura', 2, 1),
    ('Quibdó', 2, 1),
    ('Tumaco', 2, 1),
    ('Popayán', 2, 1),
    ('Pasto', 2, 1),
    -- Región Orinoquía (3)
    ('Villavicencio', 3, 1),
    ('Yopal', 3, 1),
    ('Arauca', 3, 1),
    ('Inírida', 3, 1),
    -- Región Amazonía (4)
    ('Leticia', 4, 1),
    ('Florencia', 4, 1),
    ('Mocoa', 4, 1),
    ('San José del Guaviare', 4, 1),
    ('Mitú', 4, 1),
    -- Región Andina (5)
    ('Bogotá', 5, 1),
    ('Medellín', 5, 1),
    ('Bucaramanga', 5, 1),
    ('Pereira', 5, 1),
    ('Manizales', 5, 1),
    ('Armenia', 5, 1),
    ('Ibagué', 5, 1),
    ('Neiva', 5, 1),
    ('Tunja', 5, 1),
    ('Cúcuta', 5, 1),
    -- Región Insular (6)
    ('San Andrés', 6, 1),
    ('Providencia', 6, 1);
END
GO
-- =====================================================
-- 7. DATOS SEMILLA - Usuario Admin
-- =====================================================
-- Password: Admin123! (hash BCrypt pre-generado)
IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'admin@bankbranches.com')
BEGIN
    INSERT INTO Users (FullName, Email, PasswordHash, Role, CreatedAt, IsActive)
    VALUES ('Administrador', 'admin@bankbranches.com',
            '$2a$11$K5Io9kkXE7Hx3q7eKYkHFuMz1Gp6f5QXHxqK8V.jN5V4J7m5a0dHe',
            'Admin', GETUTCDATE(), 1);
END
GO

PRINT '✅ Base de datos BankBranchesDb inicializada correctamente.';
PRINT '   - Tablas: Users, Cities, Branches';
PRINT '   - Stored Procedures: 11 creados';
PRINT '   - Datos semilla: 34 ciudades + 1 usuario Admin';
PRINT '   - Admin: admin@bankbranches.com / Admin123!';
GO
