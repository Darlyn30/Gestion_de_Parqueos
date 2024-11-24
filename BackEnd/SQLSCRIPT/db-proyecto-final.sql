create database dbFinalProject7

use dbFinalProject7

--vista del administrador, se debe loguear el admin para
create table cuentas
(
	Correo varchar(255),
	Clave varchar(30),
	primary key(Correo)
	
)

create table ingreso_auto
(
	Codigo VARCHAR(8) primary key,
	hora_entrada DATETIME,
	
)

create table tipo_vehiculos
(
	Id int identity(1,1) primary key,
	Tipo varchar(100),
	Codigo VARCHAR(8),
	foreign key(Codigo) references ingreso_auto(Codigo)
)

create table Estacionamientos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TipoVehiculoId INT, -- fK de tipo_vehiculo
    TotalDisponibles INT,
    Ocupados INT DEFAULT 0,
    FOREIGN KEY (TipoVehiculoId) REFERENCES tipo_vehiculos(Id)
)

CREATE table montoPagarCar
(
	Id int identity(1,1) primary key,
	Codigo VARCHAR(8),
	Precio decimal(10,2),
	IdEstacionamiento INT
	foreign key(IdEstacionamiento) references Estacionamientos(Id),
	foreign key(Codigo) references ingreso_auto(Codigo)
)

--esta vista es para que el administrador tenga todos los registros de los ingresos y fechas de los vehiculos que han sido registrados
--En lugar de crear una tabla para guardar todo, mejor creare una vista para el administrador

CREATE TABLE RegistroVistaAdmin
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Codigo varchar(8),
	TipoVehiculoId INT,
	EstacionamientoId INT,
	MontoPagarId INT,
	FOREIGN KEY(TipoVehiculoId) REFERENCES tipo_vehiculos(Id),
	FOREIGN KEY(EstacionamientoId) REFERENCES Estacionamientos(Id),
	FOREIGN KEY (MontoPagarId) REFERENCES montoPagarCar(Id)
)




-- Tipos de vehículos
INSERT INTO tipo_vehiculos (Tipo) VALUES 
('Automovil'),
('Motocicleta'),
('Camion')

select * from tipo_vehiculos


-- Estacionamientos por tipo
INSERT INTO Estacionamientos (TipoVehiculoId, TotalDisponibles) VALUES 
(1, 50),  -- Automóviles: 50 espacios
(2, 30),  -- Motocicletas: 30 espacios
(3, 10)  -- Camiones: 10 espacios







CREATE view ver_disponibilidad
as
select tipo_vehiculos.Tipo, Estacionamientos.TotalDisponibles, Estacionamientos.Ocupados from tipo_vehiculos
inner join Estacionamientos on  tipo_vehiculos.Id = Estacionamientos.TipoVehiculoId





CREATE PROCEDURE RegistrarEntrada
    @TipoVehiculoId INT
AS
BEGIN
    IF EXISTS (
        SELECT 1 
        FROM Estacionamientos 
        WHERE TipoVehiculoId = @TipoVehiculoId AND TotalDisponibles > Ocupados
    )
    BEGIN

        UPDATE Estacionamientos
        SET Ocupados = Ocupados + 1
        WHERE TipoVehiculoId = @TipoVehiculoId
        PRINT 'Entrada registrada.'


		INSERT INTO ingreso_auto
		VALUES (SUBSTRING(CONCAT(FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10)), 1, 8),
				 GETDATE())
    END
    ELSE
    BEGIN
        PRINT 'No hay espacios disponibles para este tipo de vehículo.'
    END
END

--le pasa el codigo donde sea que pasa este codigo

CREATE TRIGGER asignarCodeAll
ON ingreso_auto
AFTER INSERT
AS
BEGIN
	INSERT INTO tipo_vehiculos(Codigo)
	SELECT Codigo
	FROM inserted

	INSERT INTO montoPagarCar(Codigo)
	SELECT Codigo
	FROM inserted
END




ALTER PROCEDURE RegistrarSalida @Code varchar(8),
	@TipoVehiculoId INT
AS
BEGIN


	DECLARE @Ocupados INT
	DECLARE @TotalDisponibles INT

	SELECT @Ocupados = Ocupados
	FROM Estacionamientos
	WHERE TipoVehiculoId = @TipoVehiculoId
	
	SELECT @Code = Codigo
	FROM ingreso_auto
	WHERE Codigo = @Code

	SELECT @TotalDisponibles = TotalDisponibles
	FROM Estacionamientos
	WHERE TipoVehiculoId = @TipoVehiculoId

    IF EXISTS (
        SELECT 1 
        FROM Estacionamientos 
        WHERE TipoVehiculoId = @TipoVehiculoId AND TotalDisponibles > Ocupados
    )
    BEGIN

		IF @Ocupados = 0
		BEGIN
			print 'No hay vehiculos en el parqueo'
		END
		ELSE
		BEGIN
			
			IF @Code = @Code
			BEGIN

				UPDATE Estacionamientos
				SET Ocupados = Ocupados - 1
				WHERE TipoVehiculoId = @TipoVehiculoId
				PRINT 'Salida registrada.'


				DELETE FROM montoPagarCar
				WHERE Codigo = @Code

				DELETE FROM tipo_vehiculos
				WHERE Codigo = @Code

				--esta es la tabla del trigger, esta se debe borrar de ultimo, ya que da error, porque los datos
				--almacenados por el trigger, se deber borrar primero
				DELETE FROM ingreso_auto
				WHERE Codigo = @Code


				IF @Ocupados = @TotalDisponibles
				BEGIN

					PRINT 'No hay mas espacios disponibles para este tipo de vehiculo'

				END
			END
			ELSE
			BEGIN
				PRINT 'No se encontro el Vehiculo o No esta registrado, Por favor Revise el codigo'
			END


		END


    END
    ELSE
    BEGIN
        PRINT 'No hay reservas para esta tipo de vehiculo.'
    END
END


delete from tipo_vehiculos where Codigo IS NOT NULL

delete from montoPagarCar where Codigo IS NOT NULL

delete from ingreso_auto

select * from ver_disponibilidad

exec RegistrarEntrada @TipoVehiculoId = 1

select * from ingreso_auto

--PENDIENTE
exec RegistrarSalida @Code = 96219480, @TipoVehiculoId = 1 -- aqui hay un BUG, cuando registras una salida con un codigo que no esta en el registro
-- se desocupa un parqueo de igual manera

UPDATE Estacionamientos SET Ocupados = 0



--ahora falta la parte que el administrador pueda tener una vista completa y el tema de los precios


--PENDIENTE: en tipo vehiculo al no ponerse codigo, no me deja visualizar, voy a intentar hacerlo independiente dentro
--de la misma vista
CREATE VIEW vista_administrador
AS
SELECT ingreso_auto.Codigo, ingreso_auto.hora_entrada, montoPagarCar.Precio
FROM ingreso_auto
INNER JOIN montoPagarCar on ingreso_auto.Codigo = montoPagarCar.Codigo

select * from vista_administrador