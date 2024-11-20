create database dbFinalProject6

use dbFinalProject6

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

create table tipo_vehiculo
(
	Id int identity(1,1) primary key,
	Tipo varchar(100) not null,
	Codigo VARCHAR(8),
	foreign key(Codigo) references ingreso_auto(Codigo)
)

create table Estacionamiento (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TipoVehiculoId INT NOT NULL, -- fK de tipo_vehiculo
    TotalDisponibles INT NOT NULL,
    Ocupados INT NOT NULL DEFAULT 0,
    FOREIGN KEY (TipoVehiculoId) REFERENCES tipo_vehiculo(Id)
)

create table monto_a_pagar
(
	Id int identity(1,1) primary key,
	Codigo VARCHAR(8),
	Precio decimal(10,2),
	foreign key(Codigo) references ingreso_auto(Codigo)
)

--esta vista es para que el administrador tenga todos los registros de los ingresos y fechas de los vehiculos que han sido registrados
create table registro
(
	Id_registro int identity(1,1) primary key,
	Id int,
	Codigo VARCHAR(8),
	foreign key(Id) references monto_a_pagar(Id),
	foreign key(Codigo) references ingreso_auto(Codigo),

)






-- Tipos de vehículos
INSERT INTO tipo_vehiculo (Tipo) VALUES 
('Automovil'),
('Motocicleta'),
('Camion')

select * from tipo_vehiculo


-- Estacionamientos por tipo
INSERT INTO Estacionamiento (TipoVehiculoId, TotalDisponibles) VALUES 
(1, 50),  -- Automóviles: 50 espacios
(2, 30),  -- Motocicletas: 30 espacios
(3, 10)  -- Camiones: 10 espacios







create view ver_disponibilidad
as
select tipo_vehiculo.Tipo, Estacionamiento.TotalDisponibles, Estacionamiento.Ocupados from tipo_vehiculo
inner join Estacionamiento on  tipo_vehiculo.Id = Estacionamiento.TipoVehiculoId







ALTER PROCEDURE RegistrarEntrada
    @TipoVehiculoId INT
AS
BEGIN
    IF EXISTS (
        SELECT 1 
        FROM Estacionamiento 
        WHERE TipoVehiculoId = @TipoVehiculoId AND TotalDisponibles > Ocupados
    )
    BEGIN

        UPDATE Estacionamiento
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




ALTER PROCEDURE RegistrarSalida @Code varchar(8),
	@TipoVehiculoId INT
AS
BEGIN


	DECLARE @Ocupados INT
	DECLARE @TotalDisponibles INT

	SELECT @Ocupados = Ocupados
	FROM Estacionamiento
	WHERE TipoVehiculoId = @TipoVehiculoId
	
	SELECT @Code = Codigo
	FROM ingreso_auto
	WHERE Codigo = @Code

	SELECT @TotalDisponibles = TotalDisponibles
	FROM Estacionamiento
	WHERE TipoVehiculoId = @TipoVehiculoId

    IF EXISTS (
        SELECT 1 
        FROM Estacionamiento 
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

				UPDATE Estacionamiento
				SET Ocupados = Ocupados - 1
				WHERE TipoVehiculoId = @TipoVehiculoId
				PRINT 'Salida registrada.'

				DELETE FROM ingreso_auto
				WHERE Codigo = @Code

				IF @Ocupados > @TotalDisponibles
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











delete from ingreso_auto

select * from ver_disponibilidad

exec RegistrarEntrada @TipoVehiculoId = 1

select * from ingreso_auto

exec RegistrarSalida @Code = 81552037, @TipoVehiculoId = 1 -- aqui hay un BUG, cuando registras una salida con un codigo que no esta en el registro
-- se desocupa un parqueo

UPDATE Estacionamiento SET Ocupados = 0


--ahora falta la parte que el administrador pueda tener una vista completa y el tema de los precios



CREATE VIEW vista_administrador

