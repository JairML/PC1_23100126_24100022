-- =============================================================
-- PREGUNTA 1: Diseño de base de datos - Taller Mecánico
-- Motor: SQL Server
-- Enfoque: Database First (este script crea la BD; luego se hace
--          el Scaffold-DbContext sobre ella en la Pregunta 3)
-- =============================================================

-- Crear la base de datos si no existe
IF DB_ID('TallerMecanicoDB') IS NULL
BEGIN
    CREATE DATABASE TallerMecanicoDB;
END
GO

USE TallerMecanicoDB;
GO

-- Para poder reejecutar el script sin errores, eliminamos
-- las tablas en orden inverso a sus dependencias (FKs).
IF OBJECT_ID('dbo.OrdenServicio', 'U') IS NOT NULL DROP TABLE dbo.OrdenServicio;
IF OBJECT_ID('dbo.Vehiculo', 'U')      IS NOT NULL DROP TABLE dbo.Vehiculo;
IF OBJECT_ID('dbo.Cliente', 'U')       IS NOT NULL DROP TABLE dbo.Cliente;
IF OBJECT_ID('dbo.TipoServicio', 'U')  IS NOT NULL DROP TABLE dbo.TipoServicio;
GO

-- -------------------------------------------------------------
-- TABLA: TipoServicio
-- Id autogenerado (IDENTITY) como clave primaria.
-- -------------------------------------------------------------
CREATE TABLE dbo.TipoServicio
(
    Id          INT IDENTITY(1,1) NOT NULL,
    Nombre      NVARCHAR(100)     NOT NULL,
    PrecioBase  DECIMAL(18,2)     NOT NULL,
    CONSTRAINT PK_TipoServicio PRIMARY KEY (Id)
);
GO

-- -------------------------------------------------------------
-- TABLA: Cliente
-- Un cliente puede tener uno o varios vehículos (1:N con Vehiculo).
-- -------------------------------------------------------------
CREATE TABLE dbo.Cliente
(
    Id        INT IDENTITY(1,1) NOT NULL,
    Paterno   NVARCHAR(50)      NOT NULL,
    Materno   NVARCHAR(50)      NOT NULL,
    Nombres   NVARCHAR(100)     NOT NULL,
    Correo    NVARCHAR(100)     NULL,
    Telefono  NVARCHAR(20)      NULL,
    CONSTRAINT PK_Cliente PRIMARY KEY (Id)
);
GO

-- -------------------------------------------------------------
-- TABLA: Vehiculo
-- FK ClienteId -> Cliente(Id). Cada vehículo pertenece a un cliente.
-- -------------------------------------------------------------
CREATE TABLE dbo.Vehiculo
(
    Id         INT IDENTITY(1,1) NOT NULL,
    Placa      NVARCHAR(10)      NOT NULL,
    Marca      NVARCHAR(50)      NOT NULL,
    Modelo     NVARCHAR(50)      NOT NULL,
    Anio       INT               NOT NULL,
    ClienteId  INT               NOT NULL,
    CONSTRAINT PK_Vehiculo PRIMARY KEY (Id),
    CONSTRAINT FK_Vehiculo_Cliente FOREIGN KEY (ClienteId)
        REFERENCES dbo.Cliente (Id)
);
GO

-- -------------------------------------------------------------
-- TABLA: OrdenServicio
-- FK VehiculoId     -> Vehiculo(Id)
-- FK TipoServicioId -> TipoServicio(Id)
-- -------------------------------------------------------------
CREATE TABLE dbo.OrdenServicio
(
    Id                   INT IDENTITY(1,1) NOT NULL,
    FechaIngreso         DATETIME          NOT NULL,
    DescripcionProblema  NVARCHAR(500)     NOT NULL,
    CostoEstimado        DECIMAL(18,2)     NOT NULL,
    Estado               NVARCHAR(20)      NOT NULL,
    VehiculoId           INT               NOT NULL,
    TipoServicioId       INT               NOT NULL,
    CONSTRAINT PK_OrdenServicio PRIMARY KEY (Id),
    CONSTRAINT FK_OrdenServicio_Vehiculo FOREIGN KEY (VehiculoId)
        REFERENCES dbo.Vehiculo (Id),
    CONSTRAINT FK_OrdenServicio_TipoServicio FOREIGN KEY (TipoServicioId)
        REFERENCES dbo.TipoServicio (Id)
);
GO

-- =============================================================
-- DATOS DE PRUEBA (para que los endpoints respondan de inmediato)
-- =============================================================
INSERT INTO dbo.TipoServicio (Nombre, PrecioBase) VALUES
('Cambio de aceite',        80.00),
('Alineamiento y balanceo', 120.00),
('Cambio de pastillas de freno', 150.00),
('Mantenimiento general',   250.00);
GO

INSERT INTO dbo.Cliente (Paterno, Materno, Nombres, Correo, Telefono) VALUES
('Lopez',  'Garcia', 'Carlos Andres', 'carlos.lopez@mail.com', '987654321'),
('Quispe', 'Mamani', 'Maria Elena',   'maria.quispe@mail.com', '912345678');
GO

INSERT INTO dbo.Vehiculo (Placa, Marca, Modelo, Anio, ClienteId) VALUES
('ABC-123', 'Toyota', 'Corolla', 2018, 1),
('XYZ-789', 'Hyundai','Tucson',  2020, 1),  -- Carlos tiene 2 vehiculos (relacion 1:N)
('JKL-456', 'Kia',    'Rio',     2021, 2);
GO

INSERT INTO dbo.OrdenServicio
    (FechaIngreso, DescripcionProblema, CostoEstimado, Estado, VehiculoId, TipoServicioId) VALUES
('2026-05-20T09:00:00', 'Ruido en el motor al arrancar', 250.00, 'Pendiente',  1, 4),
('2026-05-22T11:30:00', 'Cambio de aceite programado',    80.00, 'En proceso', 2, 1);
GO

PRINT 'Base de datos TallerMecanicoDB creada y poblada correctamente.';
GO
