create database dbFinalProject1

use dbFinalProject1

--vista del administrador, se debe loguear el admin para
create table cuentas
(
	Correo varchar(255),
	Clave varchar(30),
	Nombre varchar(10),
	primary key(Correo)
	
)



create table ingreso_auto
(
	Codigo VARCHAR(8) primary key,
	hora_entrada DATETIME,
	
)

ALTER TABLE ingreso_auto ADD TipoVehiculo VARCHAR(50)

create table tipo_vehiculos
(
	Id int identity(1,1) primary key,
	Tipo varchar(100),
)



--en el sp debo agrregar el codigo aqui

create table Estacionamientos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TipoVehiculoId INT, -- fK de tipo_vehiculo
    TotalDisponibles INT,
    Ocupados INT DEFAULT 0,
	Precio DECIMAL(10,2),
    FOREIGN KEY (TipoVehiculoId) REFERENCES tipo_vehiculos(Id)
)



--esta vista es para que el administrador tenga todos los registros de los ingresos y fechas de los vehiculos que han sido registrados

CREATE TABLE RegistroVistaAdminss
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	TipoVehiculoId INT,
	EstacionamientoId INT,
	Codigo varchar(8),
	FOREIGN KEY (Codigo) REFERENCES ingreso_auto(Codigo),
	FOREIGN KEY(TipoVehiculoId) REFERENCES tipo_vehiculos(Id),
	FOREIGN KEY(EstacionamientoId) REFERENCES Estacionamientos(Id)
)

--CREAMOS UN TIPO DE LOG JEJE, Estos Logs Lo imprimeros en EF

CREATE TABLE LogMessages
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Mensaje VARCHAR(255),
	FechaMensaje DATETIME,
)


-- Tipos de vehículos
INSERT INTO tipo_vehiculos (Tipo) VALUES 
('Automovil'),
('Motocicleta'),
('Camion')

select * from tipo_vehiculos


-- Estacionamientos por tipo
INSERT INTO Estacionamientos (TipoVehiculoId, TotalDisponibles, Precio) VALUES 
(1, 50, 10),  -- Automóviles: 50 espacios
(2, 30, 20),  -- Motocicletas: 30 espacios
(3, 10, 30)  -- Camiones: 10 espacios
--PRECIO POR HORA



CREATE VIEW ver_disponibilidad
AS
SELECT tipo_vehiculos.Tipo, Estacionamientos.TotalDisponibles, Estacionamientos.Ocupados, Estacionamientos.Precio
FROM tipo_vehiculos
INNER JOIN Estacionamientos on tipo_vehiculos.Id = Estacionamientos.TipoVehiculoId



ALTER PROCEDURE RegistrarEntrada
    @TipoVehiculoId INT
AS
BEGIN
	DECLARE @TipoVehiculo VARCHAR(50)



    IF EXISTS (
        SELECT 1 
        FROM Estacionamientos 
        WHERE TipoVehiculoId = @TipoVehiculoId AND TotalDisponibles > Ocupados
    )
    BEGIN

        UPDATE Estacionamientos
        SET Ocupados = Ocupados + 1
        WHERE TipoVehiculoId = @TipoVehiculoId
		INSERT INTO LogMessages(Mensaje, FechaMensaje) values ('Entrada registrada.', GETDATE())
        PRINT 'Entrada registrada.'

		SELECT @TipoVehiculo = Tipo
		FROM tipo_vehiculos
		WHERE Id = @TipoVehiculoId

		INSERT INTO ingreso_auto
		VALUES (SUBSTRING(CONCAT(FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10), 
                 FLOOR(RAND() * 10)), 1, 8),
				 GETDATE(), @TipoVehiculo)

		
    END
    ELSE
    BEGIN
		INSERT INTO LogMessages(Mensaje, FechaMensaje) values ('No hay espacios disponibles para este tipo de vehículo.', GETDATE())
        PRINT 'No hay espacios disponibles para este tipo de vehículo.'
    END
END







ALTER PROCEDURE RegistrarSalida
	@CostoTotal DECIMAL(10,2) = 0, -- Declarar la variable para el costo total
	@HoraEntrada DATETIME = NULL,
    @HoraSalida DATETIME = NULL, -- Permitimos que sea NULL para asignar GETDATE() dentro del procedimiento
    @PrecioHora DECIMAL(10, 2) = NULL,
	@Code varchar(8),
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
	
	SELECT @horaSalida = GETDATE()

	SELECT @horaEntrada = hora_entrada
	FROM ingreso_auto
	WHERE Codigo = @Code




    IF EXISTS (
        SELECT 1 
        FROM Estacionamientos 
        WHERE TipoVehiculoId = @TipoVehiculoId AND TotalDisponibles > Ocupados
    )
    BEGIN

		IF @Ocupados = 0
		BEGIN
			INSERT INTO LogMessages(Mensaje, FechaMensaje) VALUES('No hay vehiculos en el parqueo', GETDATE())
			PRINT 'No hay vehiculos en el parqueo'
		END
		ELSE
		BEGIN
			
			IF @Code = @Code
			BEGIN

				--calculo de tarifa

				IF @PrecioHora IS NULL
				BEGIN
					SELECT @PrecioHora = Precio
					FROM Estacionamientos
					WHERE Id = @TipoVehiculoId
				END

				IF @HoraEntrada IS NULL
				BEGIN
					SELECT @HoraEntrada = hora_entrada
					FROM ingreso_auto
					WHERE Codigo = @Code
				END
				--definimos que la hora de salida es la hora a la que se hace la peticion
				IF @HoraSalida IS NULL
				BEGIN
					SET @HoraSalida = GETDATE()
				END

				-- Calcular la diferencia en minutos entre la hora de entrada y la hora de salida
				DECLARE @MinutosTotales INT
				SET @MinutosTotales = DATEDIFF(MINUTE, @HoraEntrada, @HoraSalida)

				

				-- Verificar las reglas de costo
				IF @MinutosTotales <= 15
				BEGIN
					SET @CostoTotal = 0 -- No paga
				END
				ELSE
				BEGIN
					-- Calcular las horas completas cobradas (redondeo hacia arriba a la hora más cercana)
					DECLARE @HorasCobradas INT;
					SET @HorasCobradas = CEILING((@MinutosTotales - 15) / 60.0)

					-- Calcular el costo total
					SET @CostoTotal = @HorasCobradas * @PrecioHora


					
				END

				-- Devolver el costo total
				INSERT INTO LogMessages(Mensaje, FechaMensaje) VALUES('Minutos Totales: ' + CAST(@MinutosTotales AS VARCHAR(10)), GETDATE())
				INSERT INTO LogMessages(Mensaje, FechaMensaje) VALUES('Costo Total: ' + CAST(@CostoTotal AS VARCHAR(10)), GETDATE())
				PRINT 'Minutos Totales: ' + CAST(@MinutosTotales AS VARCHAR(10))
				PRINT 'Costo Total: ' + CAST(@CostoTotal AS VARCHAR(10))



				--calculo de tarifa

				UPDATE Estacionamientos
				SET Ocupados = Ocupados - 1
				WHERE TipoVehiculoId = @TipoVehiculoId
				INSERT INTO LogMessages(Mensaje, FechaMensaje) VALUES('Salida registrada.', GETDATE())
				PRINT'Salida registrada.'

				DELETE FROM ingreso_auto
				WHERE Codigo = @Code


				IF @Ocupados = @TotalDisponibles
				BEGIN

					INSERT INTO LogMessages(Mensaje, FechaMensaje) VALUES('No hay mas espacios disponibles para este tipo de vehiculo', GETDATE())
					PRINT 'No hay mas espacios disponibles para este tipo de vehiculo'

				END
			END
			ELSE
			BEGIN
				--Esto esta BUG, no ha sido resuelto, 
				INSERT INTO LogMessages(Mensaje, FechaMensaje) VALUES('No se encontro el Vehiculo o No esta registrado, Por favor Revise el codigo', GETDATE())
				PRINT 'No se encontro el Vehiculo o No esta registrado, Por favor Revise el codigo'
			END


		END


    END
    ELSE
    BEGIN
		--Esto esta BUG, no ha sido resuelto, 
        INSERT INTO LogMessages(Mensaje, FechaMensaje) VALUES('No hay reservas para esta tipo de vehiculo.', GETDATE())
		PRINT 'No hay reservas para esta tipo de vehiculo.'
    END
END



select * from ver_disponibilidad

delete from ingreso_auto


exec RegistrarEntrada @TipoVehiculoId = 3

select * from ingreso_auto

--PENDIENTE
exec RegistrarSalida @Code = '02215149', @TipoVehiculoId = 3 -- aqui hay un BUG, cuando registras una salida con un codigo que 
--En el Front lo envito ya que aqui no pude
--no esta en el registro se desocupa un parqueo de igual manera
UPDATE Estacionamientos SET Ocupados = 0

SELECT * FROM LogMessages

DELETE FROM LogMessages




