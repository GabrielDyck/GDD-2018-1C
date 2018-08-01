---------------------------------------------- Creacion del Modelo de Datos --------------------------------------------------------------------
USE GD1C2018
GO
---------------------------------------------- DROPEO DE FK CONSTRAINTS ----------------------------------------------

-- Tabla Funcionalidad_X_Rol 
IF OBJECT_ID('LOS_BORBOTONES.FK_Funcionalidad_Rol', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Funcionalidad_X_Rol
	DROP CONSTRAINT FK_Funcionalidad_Rol 
GO

IF OBJECT_ID('LOS_BORBOTONES.FK_Rol_Funcionalidad', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Funcionalidad_X_Rol
	DROP CONSTRAINT FK_Rol_Funcionalidad 
GO

-- Tabla Direccion
IF OBJECT_ID('LOS_BORBOTONES.FK_Identidad_Direccion', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Direccion
	DROP CONSTRAINT FK_Identidad_Direccion 
GO

-- Tabla Usuario
IF OBJECT_ID('LOS_BORBOTONES.FK_Identidad_Usuario', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Usuario
	DROP CONSTRAINT FK_Identidad_Usuario 
GO

-- Tabla Rol_X_Usuario
	IF OBJECT_ID('LOS_BORBOTONES.FK_Rol_Usuario', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Rol_X_Usuario
	DROP CONSTRAINT FK_Rol_Usuario 
GO

IF OBJECT_ID('LOS_BORBOTONES.FK_Usuario_Rol', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Rol_X_Usuario
	DROP CONSTRAINT FK_Usuario_Rol 
GO

-- Tabla Cliente
IF OBJECT_ID('LOS_BORBOTONES.FK_Identidad_Cliente', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Cliente
	DROP CONSTRAINT FK_Identidad_Cliente 
GO

-- Tabla Hotel
IF OBJECT_ID('LOS_BORBOTONES.FK_Hotel_Categoria', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Hotel
	DROP CONSTRAINT FK_Hotel_Categoria 
GO

IF OBJECT_ID('LOS_BORBOTONES.FK_Hotel_Direccion', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Hotel
	DROP CONSTRAINT FK_Hotel_Direccion 
GO

IF OBJECT_ID('LOS_BORBOTONES.UNIQUE_Hotel_UniqueNombre', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Habitacion
	DROP CONSTRAINT UNIQUE_Hotel_UniqueNombre
GO

-- Tabla Hotel_X_Usuario
IF OBJECT_ID('LOS_BORBOTONES.FK_Hotel_Usuario', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Hotel_X_Usuario
	DROP CONSTRAINT FK_Hotel_Usuario
GO

IF OBJECT_ID('LOS_BORBOTONES.FK_Usuario_Hotel', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Hotel_X_Usuario
	DROP CONSTRAINT FK_Usuario_Hotel
GO

IF OBJECT_ID('LOS_BORBOTONES.FK_Hotel', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Regimen_X_Hotel
	DROP CONSTRAINT FK_Hotel
GO

IF OBJECT_ID('LOS_BORBOTONES.FK_Regimen', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Regimen_X_Hotel
	DROP CONSTRAINT FK_Regimen
GO

-- Tabla CierreTemporal
IF OBJECT_ID('LOS_BORBOTONES.FK_CierreTemporal_Hotel', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.CierreTemporal
	DROP CONSTRAINT FK_CierreTemporal_Hotel 
GO

-- Tabla Habitacion
IF OBJECT_ID('LOS_BORBOTONES.FK_Hotel_Habitacion', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Habitacion
	DROP CONSTRAINT FK_Hotel_Habitacion 
GO

IF OBJECT_ID('LOS_BORBOTONES.FK_Hotel_TipoHabitacion', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Habitacion
	DROP CONSTRAINT FK_Hotel_TipoHabitacion
GO

IF OBJECT_ID('LOS_BORBOTONES.UNIQUE_Habitacion_UniqueNumeroHotel', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Habitacion
	DROP CONSTRAINT UNIQUE_Habitacion_UniqueNumeroHotel
GO

-- Tabla Estadia
IF OBJECT_ID('LOS_BORBOTONES.FK_Usuario_CheckIn', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Estadia
	DROP CONSTRAINT FK_Usuario_CheckIn 
GO
	
IF OBJECT_ID('LOS_BORBOTONES.FK_Usuario_CheckOut', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Estadia
	DROP CONSTRAINT FK_Usuario_CheckOut
GO
	
-- Tabla Reserva
IF OBJECT_ID('LOS_BORBOTONES.FK_Estadia_Reserva', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Reserva
	DROP CONSTRAINT FK_Estadia_Reserva 
GO
	
IF OBJECT_ID('LOS_BORBOTONES.FK_Regimen_Reserva', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Reserva
	DROP CONSTRAINT FK_Regimen_Reserva 
GO
	
IF OBJECT_ID('LOS_BORBOTONES.FK_Cliente_Reserva', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Reserva
	DROP CONSTRAINT FK_Cliente_Reserva
GO

IF OBJECT_ID('LOS_BORBOTONES.FK_Hotel_Reserva', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Reserva
	DROP CONSTRAINT FK_Hotel_Reserva
GO
	
-- Tabla  Reserva_X_Habitacion_X_Cliente 
IF OBJECT_ID('LOS_BORBOTONES.FK_Reserva_Habitacion_Cliente', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente
	DROP CONSTRAINT FK_Reserva_Habitacion_Cliente 
GO
	
IF OBJECT_ID('LOS_BORBOTONES.FK_Habitacion_X_Cliente_X_Reserva', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente
	DROP CONSTRAINT FK_Habitacion_X_Cliente_X_Reserva
GO
	
IF OBJECT_ID('LOS_BORBOTONES.FK_Cliente_X_Habitacion_Reserva', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente
	DROP CONSTRAINT FK_Cliente_X_Habitacion_Reserva 
GO
	
-- Tabla Factura
IF OBJECT_ID('LOS_BORBOTONES.FK_Estadia_Factura', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Factura
	DROP CONSTRAINT FK_Estadia_Factura 
GO
	
IF OBJECT_ID('LOS_BORBOTONES.FK_Reserva_Factura', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Factura
	DROP CONSTRAINT FK_Reserva_Factura 
GO
	
-- Tabla ItemFactura
IF OBJECT_ID('LOS_BORBOTONES.FK_ItemFactura_Factura', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.ItemFactura
	DROP CONSTRAINT FK_ItemFactura_Factura
GO
	
IF OBJECT_ID('LOS_BORBOTONES.FK_itemFactura_Consumible', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.ItemFactura
	DROP CONSTRAINT FK_itemFactura_Consumible 
GO
	
-- Tabla Estadia_X_Consumible 
IF OBJECT_ID('LOS_BORBOTONES.FK_Estadia_Consumible', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Estadia_X_Consumible
	DROP CONSTRAINT FK_Estadia_Consumible 
GO
	
IF OBJECT_ID('LOS_BORBOTONES.FK_Consumible_Estadia', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.Estadia_X_Consumible
	DROP CONSTRAINT FK_Consumible_Estadia 
GO
	
-- Tabla EstadoReserva
IF OBJECT_ID('LOS_BORBOTONES.FK_Usuario_EstadoReserva', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.EstadoReserva
	DROP CONSTRAINT FK_Usuario_EstadoReserva 
GO
	
IF OBJECT_ID('LOS_BORBOTONES.FK_Reserva_EstadoReserva', 'F') IS NOT NULL
	ALTER TABLE LOS_BORBOTONES.EstadoReserva
	DROP CONSTRAINT FK_Reserva_EstadoReserva 
GO

---------------------------------------------- Dropeo de Tablas ----------------------------------------------------------------------------

IF OBJECT_ID('LOS_BORBOTONES.Funcionalidad_X_Rol','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Funcionalidad_X_Rol;
GO

IF OBJECT_ID('LOS_BORBOTONES.Rol_X_Usuario','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Rol_X_Usuario;
GO

IF OBJECT_ID('LOS_BORBOTONES.Rol','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Rol;
GO

IF OBJECT_ID('LOS_BORBOTONES.Funcionalidad','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Funcionalidad;
GO

IF OBJECT_ID('LOS_BORBOTONES.Estadia_X_Consumible','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Estadia_X_Consumible;
GO

IF OBJECT_ID('LOS_BORBOTONES.Consumible','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Consumible;
GO

IF OBJECT_ID('LOS_BORBOTONES.EstadoReserva','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.EstadoReserva;
GO

IF OBJECT_ID('LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente;
GO

IF OBJECT_ID('LOS_BORBOTONES.Estadia','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Estadia;
GO

IF OBJECT_ID('LOS_BORBOTONES.ItemFactura') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.ItemFactura;
GO
	
IF OBJECT_ID('LOS_BORBOTONES.Factura','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Factura;
GO

IF OBJECT_ID('LOS_BORBOTONES.Hotel_X_Usuario','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Hotel_X_Usuario;
GO

IF OBJECT_ID('LOS_BORBOTONES.CierreTemporal','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.CierreTemporal;
GO
	
IF OBJECT_ID('LOS_BORBOTONES.Regimen','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Regimen;
GO

IF OBJECT_ID('LOS_BORBOTONES.Regimen_X_Hotel','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Regimen_X_Hotel;
GO

IF OBJECT_ID('LOS_BORBOTONES.TipoHabitacion','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.TipoHabitacion;
GO

IF OBJECT_ID('LOS_BORBOTONES.Habitacion','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Habitacion;
GO

IF OBJECT_ID('LOS_BORBOTONES.Hotel','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Hotel;
GO
	
IF OBJECT_ID('LOS_BORBOTONES.Categoria','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Categoria;
GO	

IF OBJECT_ID('LOS_BORBOTONES.Cliente','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Cliente;
GO

IF OBJECT_ID('LOS_BORBOTONES.Usuario','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Usuario;
GO
	
IF OBJECT_ID('LOS_BORBOTONES.Direccion','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Direccion;
GO

IF OBJECT_ID('LOS_BORBOTONES.Identidad','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Identidad;
GO

IF OBJECT_ID('LOS_BORBOTONES.Reserva','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.Reserva;
GO

----Tablas Temporales

-- temporalMontoFactura
IF OBJECT_ID('LOS_BORBOTONES.temporalMontoFactura', 'U') IS NOT NULL
	DROP TABLE LOS_BORBOTONES.temporalMontoFactura;
GO

-- temporalMigracionEstadiasYReserva
IF OBJECT_ID('LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva', 'U') IS NOT NULL
	DROP TABLE LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva;
GO

IF OBJECT_ID('LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva', 'U') IS NOT NULL
	DROP TABLE LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva;
GO

IF OBJECT_ID('LOS_BORBOTONES.TemporalInconsistencias','U') IS NOT NULL
    DROP TABLE LOS_BORBOTONES.TemporalInconsistencias;
GO

---------------------------------------------------------------Funciones---------------------------------------------------------------------------------------------------------------
--Funcion getDate()
IF OBJECT_ID('LOS_BORBOTONES.fn_getDate', 'FN') IS NOT NULL
    DROP FUNCTION LOS_BORBOTONES.fn_getDate
GO

-- Nombre del hotel
IF OBJECT_ID('LOS_BORBOTONES.concatenarNombreHotel', 'FN') IS NOT NULL
    DROP FUNCTION LOS_BORBOTONES.concatenarNombreHotel 
GO

--Costo Total por estadia y regimen
IF OBJECT_ID('LOS_BORBOTONES.fn_costoTotalEstadia', 'FN') IS NOT NULL
    DROP FUNCTION LOS_BORBOTONES.fn_costoTotalEstadia
GO

--Puntos por estadia 
IF OBJECT_ID('LOS_BORBOTONES.fn_puntoTotalEstadia', 'FN') IS NOT NULL
    DROP FUNCTION LOS_BORBOTONES.fn_puntoTotalEstadia
GO

--Puntos por consumible 
IF OBJECT_ID('LOS_BORBOTONES.fn_puntoTotalConsumible', 'FN') IS NOT NULL
    DROP FUNCTION LOS_BORBOTONES.fn_puntoTotalConsumible
GO

---------------------------------------------------------------DROPEO PROCEDURE---------------------------------------------------------------
IF OBJECT_ID('LOS_BORBOTONES.listaMaximosPuntajes', 'P') IS NOT NULL
    DROP PROCEDURE LOS_BORBOTONES.listaMaximosPuntajes
GO
IF OBJECT_ID('LOS_BORBOTONES.listaHabitacionesVecesOcupada', 'P') IS NOT NULL
    DROP PROCEDURE LOS_BORBOTONES.listaHabitacionesVecesOcupada
GO
IF OBJECT_ID('LOS_BORBOTONES.lista_hoteles_maxResCancel', 'P') IS NOT NULL
    DROP PROCEDURE LOS_BORBOTONES.lista_hoteles_maxResCancel
GO
IF OBJECT_ID('LOS_BORBOTONES.lista_Hotel_DiasFueraServ', 'P') IS NOT NULL
    DROP PROCEDURE LOS_BORBOTONES.lista_Hotel_DiasFueraServ
GO
IF OBJECT_ID('LOS_BORBOTONES.lista_hoteles_maxConFacturados', 'P') IS NOT NULL
    DROP PROCEDURE LOS_BORBOTONES.lista_hoteles_maxConFacturados
GO

---------------------------------------------------------------------- Eliminacion de schema LOS_BORBOTONES --------------------------------------------------------------------------

---------------------------------------------------------------------- Eliminacion de schema LOS_BORBOTONES --------------------------------------------------------------------------

IF EXISTS (SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'LOS_BORBOTONES')
    DROP SCHEMA LOS_BORBOTONES;
GO
	
--Creación Inicial del Esquema
CREATE SCHEMA LOS_BORBOTONES AUTHORIZATION gdHotel2018;
GO

--------------------------------------FUNCIONES---------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------- Uso una funcion para obtener la fecha del sistema ya que no hay que usar GETDATE() -------------
----------------------------------------------- La fecha es la misma que en el archivo de configuración de la aplicación -----------------------

CREATE FUNCTION LOS_BORBOTONES.fn_getDate()
RETURNS DATETIME AS
BEGIN
	RETURN CONVERT(DATETIME, '2018/06/01 00:00:00.000', 121)
END
GO

--Funcion para establecer el nombre de hotel

CREATE FUNCTION LOS_BORBOTONES.concatenarNombreHotel 
	( @nombre nvarchar(255), @numero numeric(18,0))
	RETURNS nvarchar(255)
AS
BEGIN
	DECLARE @aux nvarchar(255)
	set @aux = CONVERT(nvarchar(255),@numero) 
	RETURN @nombre+' '+@aux
END
GO

--funcion escalar para calcular el costo por estadia

CREATE FUNCTION LOS_BORBOTONES.fn_costoTotalEstadia(@precioRegimen numeric(18,2), @cantidadDias numeric(18,0))
	RETURNS numeric(18,2)
		AS
			BEGIN
				DECLARE @totalEstadia numeric(18,2)
					SET @totalEstadia = sum(@precioRegimen * @cantidadDias)
				RETURN @totalEstadia
			END
GO

--funcion para calcular puntos por estadia, por cliente y facturacion

CREATE FUNCTION LOS_BORBOTONES.fn_puntoTotalEstadia(@CostoTotalEstadia money)
	RETURNS money
		AS
			BEGIN
				DECLARE @totalPuntoEstadia numeric(18,2)
				DECLARE @pesoPorPunto numeric(18,2)
					SET @pesoPorPunto = 20.0
					SET @totalPuntoEstadia = @CostoTotalEstadia / @pesoPorPunto
				RETURN @totalPuntoEstadia
			END
GO

--funcion para calcular puntos por consumible, por cliente y facturacion

CREATE FUNCTION LOS_BORBOTONES.fn_puntoTotalConsumible(@CostoTotalConsumible numeric(18,2))
	RETURNS numeric(18,2)
		AS
			BEGIN
				DECLARE @totalPuntoConsumible numeric(18,2)
				DECLARE @pesoPorPunto numeric(18,2)
					SET @pesoPorPunto = 10.0
					SET @totalPuntoConsumible = @CostoTotalConsumible / @pesoPorPunto
				RETURN @totalPuntoConsumible
			END
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Creacion de Store Procedures para listado Estadistico
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---Clientes con mayor cantidad de puntos
--------------------------------------------------------------

CREATE PROCEDURE LOS_BORBOTONES.listaMaximosPuntajes
				@trimestre CHAR(1), @anio CHAR(4) 
	AS				
	  BEGIN	
		DECLARE @inicio VARCHAR(10),
				@fin VARCHAR(10),
				@anioAux CHAR(4)
		SET @anioAux = @anio
		
		IF (@trimestre = '1')
			BEGIN
			SET @inicio = '01-01-'+@anioAux
			SET @fin = '31-03-'+@anioAux
			END
			ELSE IF (@trimestre = '2')
				BEGIN
				SET @inicio = '01-04-'+@anioAux
				SET @fin = '30-06-'+@anioAux
				END
				ELSE IF (@trimestre = '3')
					BEGIN 
					SET @inicio = '01-07-'+@anioAux
					SET @fin = '30-09-'+@anioAux
					END
					ELSE IF (@trimestre = '4')
						BEGIN
						SET @inicio = '01-10-'+@anioAux
						SET @fin = '31-12-'+@anioAux
						END
						ELSE 
							BEGIN
							SET @inicio = '01-01-'+@anioAux
							SET @fin = '31-12-'+@anioAux
							END

SELECT TOP 5
	 cliente.idCliente as idCliente
	 ,identidad.Nombre as 'Nombre'
	  ,identidad.Apellido 'Apellido'
	  ,SUM(fact.Puntos) as 'Puntos Totales'
FROM LOS_BORBOTONES.Factura fact
	,LOS_BORBOTONES.Cliente cliente
	,LOS_BORBOTONES.Reserva reserva
	,LOS_BORBOTONES.Consumible cons
	,LOS_BORBOTONES.Estadia_X_Consumible estXcons
	,LOS_BORBOTONES.Estadia estadia
	,LOS_BORBOTONES.Identidad identidad
WHERE fact.idReserva = reserva.idReserva
  AND reserva.idCliente = cliente.idCliente
  AND cons.idConsumible = estXcons.idConsumible
  AND estXcons.idEstadia = estadia.idEstadia
  AND estadia.idEstadia = reserva.idEstadia
  AND YEAR(fact.FechaFacturacion) = YEAR(CONVERT(DATETIME,@inicio,103))
  AND (MONTH(fact.FechaFacturacion) = MONTH(CONVERT(DATETIME,@inicio,103)) 
	OR MONTH(fact.FechaFacturacion) = MONTH(CONVERT(DATETIME,@inicio,103)) + 1
	OR MONTH(fact.FechaFacturacion) = MONTH(CONVERT(DATETIME,@inicio,103)) + 2)
  AND identidad.idIdentidad = cliente.idIdentidad
GROUP BY cliente.idCliente, identidad.Nombre, identidad.Apellido
ORDER BY 4 DESC

END
GO

--------------------------------------------------------------
--Habitaciones  con mayor cantidad de dias y veces ocupada
--------------------------------------------------------------

CREATE PROCEDURE LOS_BORBOTONES.listaHabitacionesVecesOcupada
				@trimestre CHAR(1), @anio CHAR(4)
	AS				
	 BEGIN		
		DECLARE @inicio VARCHAR(10),
				@fin VARCHAR(10),
				@anioAux CHAR(4)
		SET @anioAux = @anio
		
		IF (@trimestre = '1')
			BEGIN
			SET @inicio = '01-01-'+@anioAux
			SET @fin = '31-03-'+@anioAux
			END
			ELSE IF (@trimestre = '2')
				BEGIN
				SET @inicio = '01-04-'+@anioAux
				SET @fin = '30-06-'+@anioAux
				END
				ELSE IF (@trimestre = '3')
					BEGIN 
					SET @inicio = '01-07-'+@anioAux
					SET @fin = '30-09-'+@anioAux
					END
					ELSE IF (@trimestre = '4')
						BEGIN
						SET @inicio = '01-10-'+@anioAux
						SET @fin = '31-12-'+@anioAux
						END
						ELSE 
							BEGIN
							SET @inicio = '01-01-'+@anioAux
							SET @fin = '31-12-'+@anioAux
							END

		SELECT TOP 5
			   hotel.Nombre as 'Nombre Hotel'
			  ,habitacion.Numero as 'Numero Habitacion'
			  ,habitacion.Piso as Piso
			  ,SUM(estadia.CantidadNoches) as 'Cantidad Dias'
			  ,COUNT(*) as 'Cantidad Veces' 
		FROM LOS_BORBOTONES.Estadia estadia
			,LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente rhc
			,LOS_BORBOTONES.hotel hotel
			,LOS_BORBOTONES.Habitacion habitacion
			,LOS_BORBOTONES.Reserva reserva
		WHERE (
			(estadia.FechaEntrada >= CONVERT(DATETIME, @inicio, 103) AND estadia.FechaEntrada <= CONVERT(DATETIME, @fin, 103) AND estadia.FechaSalida >= CONVERT(DATETIME, @inicio, 103) AND estadia.FechaSalida <= CONVERT(DATETIME, @fin, 103))
			OR (estadia.FechaEntrada >= CONVERT(DATETIME, @inicio, 103) AND estadia.FechaEntrada <= CONVERT(DATETIME, @fin, 103) AND estadia.FechaSalida >= CONVERT(DATETIME, @inicio, 103) AND estadia.FechaSalida >= CONVERT(DATETIME, @fin, 103))
			OR (estadia.FechaEntrada <= CONVERT(DATETIME, @inicio, 103) AND estadia.FechaEntrada <= CONVERT(DATETIME, @fin, 103) AND estadia.FechaSalida >= CONVERT(DATETIME, @inicio, 103) AND estadia.FechaSalida <= CONVERT(DATETIME, @fin, 103))
			OR (estadia.FechaEntrada <= CONVERT(DATETIME, @inicio, 103) AND estadia.FechaEntrada <= CONVERT(DATETIME, @fin, 103) AND estadia.FechaSalida >= CONVERT(DATETIME, @inicio, 103) AND estadia.FechaSalida >= CONVERT(DATETIME, @fin, 103))
			)
			AND rhc.idHabitacion = habitacion.idHabitacion
			AND rhc.idReserva = reserva.idReserva
			AND reserva.idEstadia = estadia.idEstadia
			AND hotel.idHotel = habitacion.idHotel
		GROUP BY hotel.Nombre, habitacion.Numero, habitacion.Piso
		ORDER BY 4 DESC, 5 DESC

END
GO

--------------------------------------------------------------
--hoteles con mayor cantidad de reservas canceladas
--------------------------------------------------------------

CREATE PROCEDURE LOS_BORBOTONES.lista_hoteles_maxResCancel
				@trimestre CHAR(1), @anio CHAR(4)
	AS	
	  BEGIN	
		DECLARE @inicio VARCHAR(10),
				@fin VARCHAR(10),
				@anioAux CHAR(4)
		SET @anioAux = @anio
		
		IF (@trimestre = '1')
			BEGIN
			SET @inicio = '01-01-'+@anioAux
			SET @fin = '31-03-'+@anioAux
			END
			ELSE IF (@trimestre = '2')
				BEGIN
				SET @inicio = '01-04-'+@anioAux
				SET @fin = '30-06-'+@anioAux
				END
				ELSE IF (@trimestre = '3')
					BEGIN 
					SET @inicio = '01-07-'+@anioAux
					SET @fin = '30-09-'+@anioAux
					END
					ELSE IF (@trimestre = '4')
						BEGIN
						SET @inicio = '01-10-'+@anioAux
						SET @fin = '31-12-'+@anioAux
						END
						ELSE 
							BEGIN
							SET @inicio = '01-01-'+@anioAux
							SET @fin = '31-12-'+@anioAux
							END
							
		SELECT TOP 5 hot.Nombre as Nombre, COUNT(er.TipoEstado) as Cancelaciones
			FROM LOS_BORBOTONES.Reserva res,
				 LOS_BORBOTONES.Hotel hot,
				 LOS_BORBOTONES.Habitacion hab,
				 LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente rxhxc,
				 LOS_BORBOTONES.EstadoReserva er
		where res.idReserva = er.idReserva 
		  AND er.TipoEstado IN ('RCR', 'RCC', 'RCNS')
		  AND hab.idHabitacion = rxhxc.idHabitacion
		  AND hab.idHotel = hot.idHotel
		  AND rxhxc.idReserva = res.idReserva
		  AND er.Fecha BETWEEN CONVERT(DATETIME,@inicio,103) AND CONVERT(DATETIME,@fin,103)
		GROUP BY hot.Nombre
		ORDER BY Cancelaciones DESC, Nombre ASC
		END
GO
--------------------------------------------------------------
--Hoteles con mayor cantidad de dias fuera de servicio (CierreTemporal)
--------------------------------------------------------------

CREATE PROCEDURE LOS_BORBOTONES.lista_Hotel_DiasFueraServ
				@trimestre CHAR(1), @anio CHAR(4)
	AS		
	  BEGIN
		
		DECLARE @inicio VARCHAR(10),
				@fin VARCHAR(10),
				@anioAux CHAR(4)
		SET @anioAux = @anio
		
		IF (@trimestre = '1')
			BEGIN
			SET @inicio = '01-01-'+@anioAux
			SET @fin = '31-03-'+@anioAux
			END
			ELSE IF (@trimestre = '2')
				BEGIN
				SET @inicio = '01-04-'+@anioAux
				SET @fin = '30-06-'+@anioAux
				END
				ELSE IF (@trimestre = '3')
					BEGIN 
					SET @inicio = '01-07-'+@anioAux
					SET @fin = '30-09-'+@anioAux
					END
					ELSE IF (@trimestre = '4')
						BEGIN
						SET @inicio = '01-10-'+@anioAux
						SET @fin = '31-12-'+@anioAux
						END
						ELSE 
							BEGIN
							SET @inicio = '01-01-'+@anioAux
							SET @fin = '31-12-'+@anioAux
							end

		SELECT TOP 5
			Nombre
			,ISNULL((SELECT SUM(
				CASE WHEN (FechaInicio >= CONVERT(DATETIME, @inicio, 103) AND FechaInicio <= CONVERT(DATETIME, @fin, 103) AND FechaFin >= CONVERT(DATETIME, @inicio, 103) AND FechaFin <= CONVERT(DATETIME, @fin, 103)) THEN DATEDIFF(day, FechaInicio, FechaFin)
					 WHEN (FechaInicio >= CONVERT(DATETIME, @inicio, 103) AND FechaInicio <= CONVERT(DATETIME, @fin, 103) AND FechaFin >= CONVERT(DATETIME, @inicio, 103) AND FechaFin >= CONVERT(DATETIME, @fin, 103)) THEN DATEDIFF(day, FechaInicio, CONVERT(DATETIME, @fin, 103))
					 WHEN (FechaInicio <= CONVERT(DATETIME, @inicio, 103) AND FechaInicio <= CONVERT(DATETIME, @fin, 103) AND FechaFin >= CONVERT(DATETIME, @inicio, 103) AND FechaFin <= CONVERT(DATETIME, @fin, 103)) THEN DATEDIFF(day, CONVERT(DATETIME, @inicio, 103), FechaFin)
					 WHEN (FechaInicio <= CONVERT(DATETIME, @inicio, 103) AND FechaInicio <= CONVERT(DATETIME, @fin, 103) AND FechaFin >= CONVERT(DATETIME, @inicio, 103) AND FechaFin >= CONVERT(DATETIME, @fin, 103)) THEN DATEDIFF(day, CONVERT(DATETIME, @inicio, 103), CONVERT(DATETIME, @fin, 103))
				END)), 0) as CantDias
		FROM LOS_BORBOTONES.CierreTemporal cierreTemp
			,LOS_BORBOTONES.Hotel hotel
		WHERE cierreTemp.idHotel = hotel.idHotel
		GROUP BY Nombre
		HAVING (ISNULL((SELECT SUM(
				CASE WHEN (FechaInicio >= CONVERT(DATETIME, @inicio, 103) AND FechaInicio <= CONVERT(DATETIME, @fin, 103) AND FechaFin >= CONVERT(DATETIME, @inicio, 103) AND FechaFin <= CONVERT(DATETIME, @fin, 103)) THEN DATEDIFF(day, FechaInicio, FechaFin)
					 WHEN (FechaInicio >= CONVERT(DATETIME, @inicio, 103) AND FechaInicio <= CONVERT(DATETIME, @fin, 103) AND FechaFin >= CONVERT(DATETIME, @inicio, 103) AND FechaFin >= CONVERT(DATETIME, @fin, 103)) THEN DATEDIFF(day, FechaInicio, CONVERT(DATETIME, @fin, 103))
					 WHEN (FechaInicio <= CONVERT(DATETIME, @inicio, 103) AND FechaInicio <= CONVERT(DATETIME, @fin, 103) AND FechaFin >= CONVERT(DATETIME, @inicio, 103) AND FechaFin <= CONVERT(DATETIME, @fin, 103)) THEN DATEDIFF(day, CONVERT(DATETIME, @inicio, 103), FechaFin)
					 WHEN (FechaInicio <= CONVERT(DATETIME, @inicio, 103) AND FechaInicio <= CONVERT(DATETIME, @fin, 103) AND FechaFin >= CONVERT(DATETIME, @inicio, 103) AND FechaFin >= CONVERT(DATETIME, @fin, 103)) THEN DATEDIFF(day, CONVERT(DATETIME, @inicio, 103), CONVERT(DATETIME, @fin, 103))
				END)), 0)) > 0
		ORDER BY 2 DESC
END
GO

--------------------------------------------------------------
--Hoteles con mayor cantidad de consumibles facturados
--------------------------------------------------------------

CREATE PROCEDURE LOS_BORBOTONES.lista_hoteles_maxConFacturados
				@trimestre CHAR(1), @anio CHAR(4)
	AS				
		BEGIN
		
		DECLARE @inicio varchar(10),
				@fin varchar(10),
				@anioAux CHAR(4)
		SET @anioAux = @anio
		
		IF (@trimestre = '1')
			BEGIN
			SET @inicio = '01-01-'+@anioAux
			SET @fin = '31-03-'+@anioAux
			END
			ELSE IF (@trimestre = '2')
				BEGIN
				SET @inicio = '01-04-'+@anioAux
				SET @fin = '30-06-'+@anioAux
				END
				ELSE IF (@trimestre = '3')
					BEGIN 
					SET @inicio = '01-07-'+@anioAux
					SET @fin = '30-09-'+@anioAux
					END
					ELSE IF (@trimestre = '4')
						BEGIN
						SET @inicio = '01-10-'+@anioAux
						SET @fin = '31-12-'+@anioAux
						END
						ELSE 
							BEGIN
							SET @inicio = '01-01-'+@anioAux
							SET @fin = '31-12-'+@anioAux
							END

				SELECT TOP 5 
				     Nombre as Hotel
					,COUNT(*) as 'Consumibles Facturados'
				FROM LOS_BORBOTONES.Consumible consu
					,LOS_BORBOTONES.Estadia estad
					,LOS_BORBOTONES.Estadia_X_Consumible estXcons
					,LOS_BORBOTONES.Reserva reserva
					,LOS_BORBOTONES.Hotel hotel
					,LOS_BORBOTONES.Factura factura
				WHERE consu.idConsumible = estXcons.idConsumible
				  AND estXcons.idEstadia = estad.idEstadia
				  AND reserva.idEstadia = estad.idEstadia
				  AND hotel.idHotel = reserva.idHotel
				  AND factura.idEstadia = estad.idEstadia
				  AND factura.FechaFacturacion BETWEEN CONVERT(DATETIME,@inicio,103) AND CONVERT(DATETIME,@fin,103)
				GROUP BY Nombre
				ORDER by 2 DESC
							
				END
GO
-----------------------------------------------------------------------Creacion Tablas---------------------------------------------------------------------------------------------------------
--Tabla Rol
CREATE TABLE LOS_BORBOTONES.Rol (

	idRol	INT			IDENTITY(1,1)	NOT NULL		UNIQUE,
	Nombre	VARCHAR(45) 				NOT NULL,
	Activo	BIT							NOT NULL,
)
GO

-- Tabla Funcionalidad
CREATE TABLE LOS_BORBOTONES.Funcionalidad (

	idFuncionalidad	INT			IDENTITY(1,1)	NOT NULL,
	Descripcion 	VARCHAR(45)					NOT NULL,
)
GO

--Tabla Asociación Funcionalidad - Rol
CREATE TABLE LOS_BORBOTONES.Funcionalidad_X_Rol (

	idFuncionalidad	INT			NOT NULL,
	idRol			INT			NOT NULL,		
)
GO

--Tabla Identidad 
CREATE TABLE LOS_BORBOTONES.Identidad (

	idIdentidad				INT				IDENTITY(1,1)		NOT NULL,
	TipoIdentidad			VARCHAR(45)		NOT NULL,		
	Nombre					NVARCHAR(255)	NOT NULL,
	Apellido				NVARCHAR(255)	NOT NULL,
	TipoDocumento			VARCHAR(45)		NOT NULL,
	NumeroDocumento			VARCHAR(45)		NOT NULL,
	Mail					NVARCHAR(255)	NOT NULL, 
	FechaNacimiento			DATETIME		NOT NULL,
	Nacionalidad			NVARCHAR(255),
	Telefono				VARCHAR(45)		DEFAULT 0			NOT NULL
)
GO

--el mail debe ser unico.
--como hay inconsistencias en la migracion (mails o tipo y numero de documento repetido)
--y los clientes son validos (tienen reservas y estadias validas(?))
--se marca al cliente como inconsistente para que por GUI se edite la información
--por GUI no se puede crear o modificar un cliente para que tenga un mail o tipo y numero de documento que ya este registrado
--por lo que mostrar por pantalla que el cliente tiene información inconsistente y luego actualizarlo
--garantiza que a partir de la migración no hayan nuevos clientes inconsistentes

--Tabla Direccion
CREATE TABLE LOS_BORBOTONES.Direccion (

	idDireccion		INT				IDENTITY(1,1)				NOT NULL,
	Pais			VARCHAR(45)		DEFAULT 'Argentina'			NOT NULL,
	Ciudad			NVARCHAR(255),
	Calle			NVARCHAR(255)	NOT NULL,
	NumeroCalle		INT				NOT NULL,
	Piso			INT				DEFAULT 0,
	Depto			NVARCHAR(50)	DEFAULT '',
	idIdentidad		INT,	
)
GO

--Tabla Usuario 
CREATE TABLE LOS_BORBOTONES.Usuario (

	idUsuario				INT				IDENTITY(1,1)	NOT NULL,
	Username				VARCHAR(255)	NOT NULL,
	Password				VARCHAR(255)	NOT NULL,
	IntentosFallidosLogin	INT				DEFAULT 0, 
	Activo					BIT				DEFAULT 1,
	idIdentidad				INT				NOT NULL,
)
GO

--Tabla Asociacion Rol - Usuario
CREATE TABLE LOS_BORBOTONES.Rol_X_Usuario (

	idRol			INT			NOT NULL,
	idUsuario		INT			NOT NULL,	
)
GO

--Tabla Cliente
CREATE TABLE LOS_BORBOTONES.Cliente (

	idCliente		INT				IDENTITY(1,1)	NOT NULL,
	Activo			BIT,
	idIdentidad		INT				NOT NULL,
	Inconsistente 	BIT				NOT NULL DEFAULT 0
)
GO

--Tabla Categoria
CREATE TABLE LOS_BORBOTONES.Categoria (

	idCategoria			INT				IDENTITY(1,1)	NOT NULL,
	Estrellas			INT				NOT NULL,
	RecargaEstrellas	NUMERIC(18,2)
)
GO

-- Tabla Hotel
CREATE TABLE LOS_BORBOTONES.Hotel (

	idHotel					INT					IDENTITY(1,1)	NOT NULL	UNIQUE, 
	Nombre					NVARCHAR(255)		NOT NULL,
	Mail					NVARCHAR(255)		DEFAULT 'No Posee',
	Telefono				VARCHAR(45)			DEFAULT 'No Posee',
	FechaInicioActividades	DATETIME			NOT NULL,
	idCategoria				INT					NOT NULL,
	idDireccion				INT					NOT NULL,
)
GO

--Tabla Asociacion Hotel - Usuario
CREATE TABLE LOS_BORBOTONES.Hotel_X_Usuario (

	idHotel			INT			NOT NULL,
	idUsuario		INT			NOT NULL,
)
GO

--Tabla CierreTemporal
CREATE TABLE LOS_BORBOTONES.CierreTemporal (

	idEstadoHotel	INT				IDENTITY(1,1)	NOT NULL,
	FechaInicio		DATETIME		NOT NULL,
	FechaFin		DATETIME		NOT NULL,
	Descripcion		VARCHAR(45)		NOT NULL,
	idHotel			INT				NOT NULL,	
)
GO

--Tabla Regimen
CREATE TABLE LOS_BORBOTONES.Regimen (
	
	idRegimen		INT				IDENTITY(1,1)	NOT NULL,
	Codigo			VARCHAR(45)		NOT NULL,		
	Descripcion		NVARCHAR(255)	DEFAULT 'No Posee',
	Precio			NUMERIC(18,2),
	Activo			BIT
)
GO

--Tabla Regimen
CREATE TABLE LOS_BORBOTONES.Regimen_X_Hotel (
	
	idRegimenXHotel		INT				IDENTITY(1,1)	NOT NULL,
	idHotel				INT				NOT NULL,	
	idRegimen			INT				NOT NULL,	
)
GO

--Tabla TipoHabitacion
CREATE TABLE LOS_BORBOTONES.TipoHabitacion (

	idTipoHabitacion	INT				IDENTITY(1,1)	NOT NULL,
	Codigo				VARCHAR(45)		NOT NULL,
	Descripcion			VARCHAR(45)		NOT NULL,
	Porcentual			NUMERIC(18,2)	NOT NULL,	
)
GO

--Tabla Habitacion
CREATE TABLE LOS_BORBOTONES.Habitacion (

	idHabitacion		INT				IDENTITY(1,1)	NOT NULL,
	Activa				BIT				DEFAULT 1,
	Numero				INT	NOT NULL,
	Piso				INT	NOT NULL,
	Descripcion			VARCHAR(500) DEFAULT 'No Posee',
	Ubicacion			NVARCHAR(50)	NOT NULL,
	idHotel				INT				NOT NULL,
	idTipoHabitacion	INT				NOT NULL,	
)
GO


-- Tabla Estadia
CREATE TABLE LOS_BORBOTONES.Estadia (

	idEstadia		INT			IDENTITY(1,1)	NOT NULL	UNIQUE,
	FechaEntrada	DATETIME	NOT NULL,
	FechaSalida		DATETIME	,--NOT NULL,
	CantidadNoches	NUMERIC(18,0),
	Facturada		BIT			DEFAULT 1,
	idUsuarioIn		INT			NOT NULL,
	idUsuarioOut	INT,
		
)
GO

-- Tabla Reserva
CREATE TABLE LOS_BORBOTONES.Reserva (

	idReserva		INT				IDENTITY(1,1)	NOT NULL	UNIQUE,
	CodigoReserva	NUMERIC(18,0)	NOT NULL,
	FechaCreacion	DATETIME,
	FechaDesde		DATETIME		NOT NULL,
	FechaHasta		DATETIME		NOT NULL,
	DiasAlojados	NUMERIC(18,0),
	idHotel			INT				NOT NULL,
	idEstadia		INT,
	idRegimen		INT				NOT NULL,
	idCliente		INT				NOT NULL,
)
GO

-- Tabla Asociacion Reserva - Habitacion - Cliente
CREATE TABLE LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente (

	idReserva		INT		NOT NULL,
	idHabitacion	INT		NOT NULL,
	idCliente		INT		NOT NULL,
)
GO

-- Tabla Factura
CREATE TABLE LOS_BORBOTONES.Factura (

	idFactura			INT					IDENTITY(1, 1)		NOT NULL,
	NumeroFactura		NUMERIC(18,0)		NOT NULL,
	FechaFacturacion	DATETIME			NOT NULL,
	Total				NUMERIC(18,2)		NOT NULL,
	Puntos				NUMERIC(18,2),
	TipoPago			VARCHAR(45),
	Titular 			NVARCHAR(255),
	CodigoSeguridad 	INT,
	NroTarjeta 			NUMERIC(16,0), 
	Vencimiento 		INT,
	idEstadia			INT					NOT NULL,
	idReserva			INT					NOT NULL,
)
GO

-- Tabla Consumible
CREATE TABLE LOS_BORBOTONES.Consumible (

	idConsumible		INT				IDENTITY(1,1)	NOT NULL,
	Codigo				NUMERIC(18,0)	NOT NULL,
	Descripcion			NVARCHAR(255)	NOT NULL,
	Precio				NUMERIC(18,2)	NOT NULL,
)
GO

-- Tabla ItemFactura
CREATE TABLE LOS_BORBOTONES.ItemFactura (

		idItemFactura		INT				IDENTITY(1,1)	NOT NULL,
		Cantidad			NUMERIC(18,0)	NOT NULL,
		Monto				NUMERIC(18,2)	NOT NULL,
		FechaCreacion		DATETIME		DEFAULT LOS_BORBOTONES.fn_getDate(),
		idFactura			INT				NOT NULL,
		idConsumible		INT				NOT NULL,		
)
GO

-- Tabla Asociacion Estadia - Consumible
CREATE TABLE LOS_BORBOTONES.Estadia_X_Consumible (

	idEstadia		INT		NOT NULL,
	idConsumible	INT		NOT NULL,
	Cantidad		INT 	NOT NULL
)
GO

-- Tabla EstadoReserva
CREATE TABLE LOS_BORBOTONES.EstadoReserva (

	idEstado		INT				IDENTITY(1,1)	NOT NULL,
	TipoEstado		VARCHAR(45),
	Fecha			DATETIME		NOT NULL,
	Descripcion		VARCHAR(45),
	idUsuario		INT				NOT NULL,
	idReserva		INT				NOT NULL,
)
GO

--------------------------------------------- Creacion de constraint PK para la base de datos ----------------------------------------------------------------------------------------
-- Tabla Rol
ALTER TABLE LOS_BORBOTONES.Rol
ADD CONSTRAINT PK_Rol_idRol PRIMARY KEY (idRol) 

-- Tabla Funcionalidad
ALTER TABLE LOS_BORBOTONES.Funcionalidad
ADD CONSTRAINT PK_Funcionalidad_idFuncionalidad PRIMARY KEY (idFuncionalidad)

-- Tabla Identidad
ALTER TABLE LOS_BORBOTONES.Identidad
ADD CONSTRAINT PK_Identidad_idIdentidad PRIMARY KEY (idIdentidad)

-- Tabla Direccion
ALTER TABLE LOS_BORBOTONES.Direccion
ADD CONSTRAINT PK_Direccion_idDireccion PRIMARY KEY (idDireccion)

-- Tabla Usuario
ALTER TABLE LOS_BORBOTONES.Usuario
ADD CONSTRAINT PK_Usuario_idUsuario PRIMARY KEY (idUsuario)

-- Tabla Cliente
ALTER TABLE LOS_BORBOTONES.Cliente
ADD CONSTRAINT PK_Cliente_idCliente PRIMARY KEY (idCliente)

-- Tabla Categoria
ALTER TABLE LOS_BORBOTONES.Categoria
ADD CONSTRAINT PK_Categoria_idCategoria PRIMARY KEY (idCategoria)

-- Tabla Hotel
ALTER TABLE LOS_BORBOTONES.Hotel
ADD CONSTRAINT PK_Hotel_idHotel PRIMARY KEY (idHotel)

-- Tabla CierreTemporal
ALTER TABLE LOS_BORBOTONES.CierreTemporal
ADD CONSTRAINT PK_CierreTemporal_idEstadoHotel PRIMARY KEY (idEstadoHotel)

-- Tabla Regimen
ALTER TABLE LOS_BORBOTONES.Regimen
ADD CONSTRAINT PK_Regimen_idRegimen PRIMARY KEY (idRegimen)

-- Tabla TipoHabitacion
ALTER TABLE LOS_BORBOTONES.TipoHabitacion
ADD CONSTRAINT PK_TipoHabitacion_idTipoHabitacion PRIMARY KEY (idTipoHabitacion)

-- Tabla Habitacion
ALTER TABLE LOS_BORBOTONES.Habitacion
ADD CONSTRAINT PK_Habitacion_idHabitacion PRIMARY KEY (idHabitacion)

-- Tabla Estadia
ALTER TABLE LOS_BORBOTONES.Estadia
ADD CONSTRAINT PK_Estadia_idEstadia PRIMARY KEY (idEstadia)

-- Tabla Reserva
ALTER TABLE LOS_BORBOTONES.Reserva
ADD CONSTRAINT PK_Reserva_idReserva PRIMARY KEY (idReserva)

-- Tabla Reserva
ALTER TABLE LOS_BORBOTONES.Factura
ADD CONSTRAINT PK_Factura_idFactura PRIMARY KEY (idFactura)

-- Tabla Consumible
ALTER TABLE LOS_BORBOTONES.Consumible
ADD CONSTRAINT PK_Consumible_idConsumible PRIMARY KEY (idConsumible)

-- Tabla ItemFactura
ALTER TABLE LOS_BORBOTONES.ItemFactura
ADD CONSTRAINT PK_ItemFactura_idItemFactura PRIMARY KEY (idItemFactura)

-- Tabla EstadoReserva
ALTER TABLE LOS_BORBOTONES.EstadoReserva
ADD CONSTRAINT PK_EstadoReserva_idEstado PRIMARY KEY (idEstado)

---------------------------------------------------------- Creacion de constraint FK para la base de datos ---------------------------------------------------------------------------

-- Tabla Funcionalidad_X_Rol 
ALTER TABLE LOS_BORBOTONES.Funcionalidad_X_Rol
ADD CONSTRAINT FK_Funcionalidad_Rol FOREIGN KEY(idFuncionalidad) REFERENCES LOS_BORBOTONES.Funcionalidad(idFuncionalidad) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.Funcionalidad_X_Rol
ADD CONSTRAINT FK_Rol_Funcionalidad FOREIGN KEY(idRol) REFERENCES LOS_BORBOTONES.Rol(idRol) ON DELETE CASCADE ON UPDATE CASCADE

-- Tabla Direccion
ALTER TABLE LOS_BORBOTONES.Direccion
ADD CONSTRAINT FK_Identidad_Direccion FOREIGN KEY(idIdentidad) REFERENCES LOS_BORBOTONES.Identidad(idIdentidad) ON DELETE CASCADE ON UPDATE CASCADE

-- Tabla Usuario
ALTER TABLE LOS_BORBOTONES.Usuario
ADD CONSTRAINT FK_Identidad_Usuario FOREIGN KEY(idIdentidad) REFERENCES LOS_BORBOTONES.Identidad(idIdentidad) ON DELETE CASCADE ON UPDATE CASCADE

-- Tabla Rol_X_Usuario
ALTER TABLE LOS_BORBOTONES.Rol_X_Usuario
ADD CONSTRAINT FK_Rol_Usuario FOREIGN KEY(idRol) REFERENCES LOS_BORBOTONES.Rol(idRol) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.Rol_X_Usuario
ADD CONSTRAINT FK_Usuario_Rol FOREIGN KEY(idUsuario) REFERENCES LOS_BORBOTONES.Usuario(idUsuario) ON DELETE CASCADE ON UPDATE CASCADE

-- Tabla Cliente
ALTER TABLE LOS_BORBOTONES.Cliente
ADD CONSTRAINT FK_Identidad_Cliente FOREIGN KEY(idIdentidad) REFERENCES LOS_BORBOTONES.Identidad(idIdentidad) ON DELETE CASCADE ON UPDATE CASCADE

-- Tabla Hotel
ALTER TABLE LOS_BORBOTONES.Hotel
ADD CONSTRAINT FK_Hotel_Categoria FOREIGN KEY(idCategoria) REFERENCES LOS_BORBOTONES.Categoria(idCategoria) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.Hotel
ADD CONSTRAINT FK_Hotel_Direccion FOREIGN KEY(idDireccion) REFERENCES LOS_BORBOTONES.Direccion(idDireccion) ON DELETE CASCADE ON UPDATE CASCADE


ALTER TABLE LOS_BORBOTONES.Hotel
ADD CONSTRAINT UNIQUE_Hotel_UniqueNombre UNIQUE (Nombre)



-- Tabla Hotel_X_Usuario
ALTER TABLE LOS_BORBOTONES.Hotel_X_Usuario
ADD CONSTRAINT FK_Hotel_Usuario FOREIGN KEY(idHotel) REFERENCES LOS_BORBOTONES.Hotel(idHotel) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.Hotel_X_Usuario
ADD CONSTRAINT FK_Usuario_Hotel FOREIGN KEY(idUsuario) REFERENCES LOS_BORBOTONES.Usuario(idUsuario)

-- Tabla Regimen_X_Hotel
ALTER TABLE LOS_BORBOTONES.Regimen_X_Hotel 
ADD CONSTRAINT FK_Regimen FOREIGN KEY(idRegimen) REFERENCES LOS_BORBOTONES.Regimen(idRegimen);

ALTER TABLE LOS_BORBOTONES.Regimen_X_Hotel 
ADD CONSTRAINT FK_Hotel FOREIGN KEY(idHotel) REFERENCES LOS_BORBOTONES.Hotel(idHotel);

-- Tabla CierreTemporal
ALTER TABLE LOS_BORBOTONES.CierreTemporal
ADD CONSTRAINT FK_CierreTemporal_Hotel FOREIGN KEY(idHotel) REFERENCES LOS_BORBOTONES.Hotel(idHotel) ON DELETE CASCADE ON UPDATE CASCADE

-- Tabla Habitacion
ALTER TABLE LOS_BORBOTONES.Habitacion
ADD CONSTRAINT FK_Hotel_Habitacion FOREIGN KEY(idHotel) REFERENCES LOS_BORBOTONES.Hotel(idHotel) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.Habitacion
ADD CONSTRAINT FK_Hotel_TipoHabitacion FOREIGN KEY(idTipoHabitacion) REFERENCES LOS_BORBOTONES.TipoHabitacion(idTipoHabitacion) ON DELETE CASCADE ON UPDATE CASCADE


ALTER TABLE LOS_BORBOTONES.Habitacion
ADD CONSTRAINT UNIQUE_Habitacion_UniqueNumeroHotel UNIQUE NONCLUSTERED (Numero,idHotel)


-- Tabla Estadia
ALTER TABLE LOS_BORBOTONES.Estadia
ADD CONSTRAINT FK_Usuario_CheckIn FOREIGN KEY(idUsuarioIn) REFERENCES LOS_BORBOTONES.Usuario(idUsuario)

ALTER TABLE LOS_BORBOTONES.Estadia
ADD CONSTRAINT FK_Usuario_CheckOut FOREIGN KEY(idUsuarioOut) REFERENCES LOS_BORBOTONES.Usuario(idUsuario)

-- Tabla Reserva
ALTER TABLE LOS_BORBOTONES.Reserva
ADD CONSTRAINT FK_Estadia_Reserva FOREIGN KEY(idEstadia) REFERENCES LOS_BORBOTONES.Estadia(idEstadia) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.Reserva
ADD CONSTRAINT FK_Regimen_Reserva FOREIGN KEY(idRegimen) REFERENCES LOS_BORBOTONES.Regimen(idRegimen) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.Reserva
ADD CONSTRAINT FK_Cliente_Reserva FOREIGN KEY(idCliente) REFERENCES LOS_BORBOTONES.Cliente(idCliente) 


ALTER TABLE LOS_BORBOTONES.Reserva
ADD CONSTRAINT FK_Hotel_Reserva FOREIGN KEY(idHotel) REFERENCES LOS_BORBOTONES.Hotel(idHotel) 


-- Tabla  Reserva_X_Habitacion_X_Cliente 
ALTER TABLE LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente
ADD CONSTRAINT FK_Reserva_Habitacion_Cliente FOREIGN KEY(idReserva) REFERENCES LOS_BORBOTONES.Reserva(idReserva) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente
ADD CONSTRAINT FK_Habitacion_X_Cliente_X_Reserva FOREIGN KEY(idHabitacion) REFERENCES LOS_BORBOTONES.Habitacion(idHabitacion)

ALTER TABLE LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente
ADD CONSTRAINT FK_Cliente_X_Habitacion_Reserva FOREIGN KEY(idCliente) REFERENCES LOS_BORBOTONES.Cliente(idCliente)

-- Tabla Factura
ALTER TABLE LOS_BORBOTONES.Factura
ADD CONSTRAINT FK_Estadia_Factura FOREIGN KEY(idEstadia) REFERENCES LOS_BORBOTONES.Estadia(idEstadia) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.Factura
ADD CONSTRAINT FK_Reserva_Factura FOREIGN KEY(idReserva) REFERENCES LOS_BORBOTONES.Reserva(idReserva) 

-- Tabla ItemFactura
ALTER TABLE LOS_BORBOTONES.ItemFactura
ADD CONSTRAINT FK_ItemFactura_Factura FOREIGN KEY(idFactura) REFERENCES LOS_BORBOTONES.Factura(idFactura) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.ItemFactura
ADD CONSTRAINT FK_itemFactura_Consumible FOREIGN KEY(idConsumible) REFERENCES LOS_BORBOTONES.Consumible(idConsumible) ON DELETE CASCADE ON UPDATE CASCADE

-- Tabla Estadia_X_Consumible 
ALTER TABLE LOS_BORBOTONES.Estadia_X_Consumible
ADD CONSTRAINT FK_Estadia_Consumible FOREIGN KEY(idEstadia) REFERENCES LOS_BORBOTONES.Estadia(idEstadia) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.Estadia_X_Consumible
ADD CONSTRAINT FK_Consumible_Estadia FOREIGN KEY(idConsumible) REFERENCES LOS_BORBOTONES.Consumible(idConsumible) ON DELETE CASCADE ON UPDATE CASCADE

-- Tabla EstadoReserva
ALTER TABLE LOS_BORBOTONES.EstadoReserva
ADD CONSTRAINT FK_Usuario_EstadoReserva FOREIGN KEY(idUsuario) REFERENCES LOS_BORBOTONES.Usuario(idUsuario) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE LOS_BORBOTONES.EstadoReserva
ADD CONSTRAINT FK_Reserva_EstadoReserva FOREIGN KEY(idReserva) REFERENCES LOS_BORBOTONES.Reserva(idReserva)

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------FIN CREACION--------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Creacion Procedimiento Migracion Tabla Maestra

CREATE INDEX IDX_DIRECCION01 ON LOS_BORBOTONES.Direccion (Calle); --se crea indice a la tabla Direccion, el campo Calle
CREATE INDEX IDX_IDENTIDAD01 ON LOS_BORBOTONES.Identidad (Mail); -- se crea un indice a la tabla Identidad el campo Mail

CREATE INDEX IDX_Reserva_CodigoReserva ON LOS_BORBOTONES.Reserva(CodigoReserva);
CREATE INDEX IDX_Reserva_Fecha ON LOS_BORBOTONES.Reserva(FechaDesde,FechaHasta);
CREATE INDEX IDX_ReservaXHabitacionXCliente_Reserva ON LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente(idReserva);
CREATE INDEX IDX_ReservaXHabitacionXCliente_Habitacion ON LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente(idHabitacion);
CREATE INDEX IDX_ReservaXHabitacionXCliente_Cliente ON LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente(idCliente);
CREATE INDEX IDX_EstadoReserva_Reserva ON LOS_BORBOTONES.EstadoReserva(idReserva);
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Carga de  Roles Iniciales

INSERT INTO LOS_BORBOTONES.Rol (Nombre, Activo)
VALUES ('AdminDelEnunciado', 1), ('Administrador General', 1), ('Recepcionista', 1), ('Guest', 1), ('RolDummy', 0);
GO
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Carga de  Funcionalidades

INSERT INTO LOS_BORBOTONES.Funcionalidad (Descripcion)
VALUES ('ABMRol'), ('ABMReserva'), ('ABMUsuario'), ('ABMCliente'), ('ABMHotel'),
('ABMHabitacion'), ('ABMRegimenEstadia'), ('RegistrarEstadia'), ('RegistrarConsumible'), ('FacturarEstadia'), ('GenerarListadoEstadistico');
GO
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Asociación Inicial Roles Funcionalidad
--Permisos del administrador que inferimos segun el enunciado

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'ABMRol'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'AdminDelEnunciado'));
GO

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'ABMUsuario'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'AdminDelEnunciado'));
GO

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'ABMHotel'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'AdminDelEnunciado'));
GO

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'ABMHabitacion'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'AdminDelEnunciado'));
GO

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'ABMRegimenEstadia'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'AdminDelEnunciado'));
GO

--Permisos del administrador full control que pide el enunciado para la entrega
-------------------------------------------------------------------------------

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
SELECT f.idFuncionalidad, r.idRol
FROM LOS_BORBOTONES.Funcionalidad f
CROSS JOIN LOS_BORBOTONES.Rol r
WHERE r.Nombre = 'Administrador General';
GO

--Permisos del rol recepcionista
-------------------------------------------------------------------------------

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'ABMCliente'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'Recepcionista'));
GO

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'ABMReserva'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'Recepcionista'));
GO

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'RegistrarEstadia'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'Recepcionista'));
GO

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Estos últimos tres permisos no están validados (inferimos los roles asociados a la funcionalidad)

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'RegistrarConsumible'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'Recepcionista'));
GO

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'FacturarEstadia'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'Recepcionista'));
GO

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'GenerarListadoEstadistico'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'Recepcionista'));
GO

--Permisos del guest
-------------------------------------------------------------------------------

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'ABMReserva'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'Guest'));
GO

--Permisos del RolDummy (tiene que tener al menos un rol)
-------------------------------------------------------------------------------

INSERT INTO LOS_BORBOTONES.Funcionalidad_X_Rol (idFuncionalidad, idRol)
VALUES ((SELECT idFuncionalidad FROM LOS_BORBOTONES.Funcionalidad WHERE Descripcion = 'GenerarListadoEstadistico'),(SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'RolDummy'));
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Genero Identidad de los Usuarios

INSERT INTO LOS_BORBOTONES.Identidad(TipoIdentidad, Nombre, Apellido, TipoDocumento, NumeroDocumento, Mail, FechaNacimiento, Nacionalidad)
	   VALUES('Usuario', 'Jose', 'Perez', 'DNI', '30213210',  'admin2@frba_utn.com', CONVERT(DATETIME, '1968-01-09 00:00:00.000', 121), 'ARGENTINO')
GO

INSERT INTO LOS_BORBOTONES.Identidad(TipoIdentidad, Nombre, Apellido, TipoDocumento, NumeroDocumento, Mail, FechaNacimiento, Nacionalidad)
	   VALUES('Usuario', 'Guest', 'Guest', 'DNI', '1',  'guest@frba_utn.com', CONVERT(DATETIME, '1998-05-05 00:00:00.000',121), 'GUEST')
GO

INSERT INTO LOS_BORBOTONES.Identidad(TipoIdentidad, Nombre, Apellido, TipoDocumento, NumeroDocumento, Mail, FechaNacimiento, Nacionalidad)
	   VALUES('Usuario', 'Carolina', 'Mengoche', 'DNI', '17309573',  'recepcionista@frba_utn.com', CONVERT(DATETIME, '1988-09-11 00:00:00.000',121), 'COLOMBIANO')
GO

--Identidad del admin del enunciado
INSERT INTO LOS_BORBOTONES.Identidad(TipoIdentidad, Nombre, Apellido, TipoDocumento, NumeroDocumento, Mail, FechaNacimiento, Nacionalidad)
	   VALUES('Usuario', 'Pedro', 'Uteniano', 'DNI', '28450395',  'admin@frba_utn.com', CONVERT(DATETIME, '1984-02-07 00:00:00.000',121), 'ARGENTINO')
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Genero Direccion de los Usuarios

INSERT INTO LOS_BORBOTONES.Direccion (Pais, Ciudad, Calle, NumeroCalle, Piso, Depto, idIdentidad)
	VALUES ('Argentina','Capital Federal', 'Mexico', 645, 23, 'C', (SELECT idIdentidad FROM LOS_BORBOTONES.Identidad WHERE TipoDocumento = 'DNI' and NumeroDocumento like '30213210' and TipoIdentidad = 'Usuario'));
GO

INSERT INTO LOS_BORBOTONES.Direccion (Pais, Ciudad, Calle, NumeroCalle, Piso, Depto, idIdentidad)
	VALUES ('Guest','Guest', 'Guest', 0, 0, '', (SELECT idIdentidad FROM LOS_BORBOTONES.Identidad WHERE TipoDocumento = 'DNI' and NumeroDocumento like '1' and TipoIdentidad = 'Usuario'));
GO

INSERT INTO LOS_BORBOTONES.Direccion (Pais, Ciudad, Calle, NumeroCalle, Piso, Depto, idIdentidad)
	VALUES ('Argentina','Dock Sud', 'Boulevard San Martin', 576, 0, '', (SELECT idIdentidad FROM LOS_BORBOTONES.Identidad WHERE TipoDocumento = 'DNI' and NumeroDocumento like '17309573' and TipoIdentidad = 'Usuario'));
GO

--Inserto direccion del admin uteniano
INSERT INTO LOS_BORBOTONES.Direccion (Pais, Ciudad, Calle, NumeroCalle, Piso, Depto, idIdentidad)
	VALUES ('Argentina','Lugano', 'Mozart', 2300, 0, '', (SELECT idIdentidad FROM LOS_BORBOTONES.Identidad WHERE TipoDocumento = 'DNI' and NumeroDocumento like '28450395' and TipoIdentidad = 'Usuario'));
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- La password es 1234 para todos los usuarios
-- Este es el admin que inferimos por las funcionalidades del enunciado
INSERT INTO LOS_BORBOTONES.Usuario (Username,Password, idIdentidad)
	VALUES ('adminOriginal','03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', (SELECT idIdentidad FROM LOS_BORBOTONES.Identidad WHERE TipoDocumento = 'DNI' and NumeroDocumento like '30213210' and TipoIdentidad = 'Usuario'));
GO

INSERT INTO LOS_BORBOTONES.Usuario (Username,Password, idIdentidad)
	VALUES ('guest','03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', (SELECT idIdentidad FROM LOS_BORBOTONES.Identidad WHERE TipoDocumento = 'DNI' and NumeroDocumento like '1' and TipoIdentidad = 'Usuario'));
GO

INSERT INTO LOS_BORBOTONES.Usuario (Username,Password, idIdentidad)
	VALUES ('recepcionista','03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', (SELECT idIdentidad FROM LOS_BORBOTONES.Identidad WHERE TipoDocumento = 'DNI' and NumeroDocumento like '17309573' and TipoIdentidad = 'Usuario'));
GO

--Este es el usuario administrador que pide el enunciado
INSERT INTO LOS_BORBOTONES.Usuario (Username,Password, idIdentidad)
	VALUES ('admin','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', (SELECT idIdentidad FROM LOS_BORBOTONES.Identidad WHERE TipoDocumento = 'DNI' and NumeroDocumento like '28450395' and TipoIdentidad = 'Usuario'));
GO

--Carga Rol_X_Usuario
INSERT INTO LOS_BORBOTONES.Rol_X_Usuario (idRol, idUsuario)
	VALUES ((SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'AdminDelEnunciado'),(SELECT idUsuario FROM LOS_BORBOTONES.Usuario WHERE Username = 'adminOriginal'));
GO

INSERT INTO LOS_BORBOTONES.Rol_X_Usuario (idRol, idUsuario)
	VALUES ((SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'Guest'),(SELECT idUsuario FROM LOS_BORBOTONES.Usuario WHERE Username = 'guest'));
GO

INSERT INTO LOS_BORBOTONES.Rol_X_Usuario (idRol, idUsuario)
	VALUES ((SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'Recepcionista'),(SELECT idUsuario FROM LOS_BORBOTONES.Usuario WHERE Username = 'recepcionista'));
GO

INSERT INTO LOS_BORBOTONES.Rol_X_Usuario (idRol, idUsuario)
	VALUES ((SELECT idRol FROM LOS_BORBOTONES.Rol WHERE Nombre = 'Administrador General'),(SELECT idUsuario FROM LOS_BORBOTONES.Usuario WHERE Username = 'admin'));
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- MIGRACION Identidad
--telefono quedo por defecto en 0

INSERT INTO LOS_BORBOTONES.Identidad(TipoIdentidad, Nombre, Apellido, TipoDocumento, NumeroDocumento, Mail, FechaNacimiento, Nacionalidad)
	   SELECT  DISTINCT	'Cliente', Cliente_Nombre, Cliente_Apellido, 'Pasaporte', Cliente_Pasaporte_Nro,  Cliente_Mail, Cliente_Fecha_Nac, Cliente_Nacionalidad
		FROM gd_esquema.Maestra
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	
-- MIGRACION Direccion 
--los que no tienen idIdentidad, son los hoteles

INSERT INTO LOS_BORBOTONES.Direccion(Ciudad, Calle, NumeroCalle, Piso, Depto, idIdentidad)
(
	SELECT  'No Posee', m.Cliente_Dom_Calle, m.Cliente_Nro_Calle, m.Cliente_Piso, m.Cliente_Depto, i.idIdentidad
	FROM gd_esquema.Maestra m, LOS_BORBOTONES.Identidad i
	WHERE i.NumeroDocumento = m.Cliente_Pasaporte_Nro
UNION
	SELECT LTRIM(RTRIM(Hotel_Ciudad)), Hotel_Calle, Hotel_Nro_Calle, NULL, NULL, NULL
	FROM gd_esquema.Maestra 
)	
GO

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- MIGRACION Categoria 

INSERT INTO LOS_BORBOTONES.Categoria(Estrellas, RecargaEstrellas) 
		SELECT  DISTINCT Hotel_CantEstrella, Hotel_Recarga_Estrella
		FROM gd_esquema.Maestra
GO

INSERT INTO LOS_BORBOTONES.Categoria(Estrellas, RecargaEstrellas) 
	VALUES(2, 5)
GO

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Migracion y Carga de Hotel
--Se define como FechaInicioActividades, la fecha mas antigua de las reservas y como Nombre del Hotel Calle+NroCalle
 
 INSERT INTO LOS_BORBOTONES.Hotel (idCategoria, Nombre, Mail, Telefono, FechaInicioActividades, idDireccion)
	  SELECT c.idCategoria, LOS_BORBOTONES.concatenarNombreHotel(d.Calle, d.NumeroCalle) AS Nombre, 'No Posee', 'No Posee', (SELECT DISTINCT TOP 1 Reserva_Fecha_Inicio FROM gd_esquema.Maestra ORDER BY Reserva_Fecha_Inicio), d.idDireccion 
	  FROM LOS_BORBOTONES.Categoria c
	  JOIN  gd_esquema.Maestra m ON m.Hotel_CantEstrella = c.Estrellas AND m.Hotel_Recarga_Estrella = c.RecargaEstrellas
	  JOIN LOS_BORBOTONES.Direccion d ON m.Hotel_Ciudad = d.Ciudad AND m.Hotel_Calle = d.Calle AND m.Hotel_Nro_Calle = d.NumeroCalle
	  WHERE d.Ciudad IS NOT NULL
	  GROUP BY c.idCategoria, d.idDireccion, LOS_BORBOTONES.concatenarNombreHotel(d.Calle, d.NumeroCalle) 
GO
	  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	  

--Carga CLIENTE
--por defecto los clientes aparecen con Activo = 1
INSERT INTO LOS_BORBOTONES.Cliente(idIdentidad, Activo) 
		SELECT DISTINCT idIdentidad, 1
		FROM LOS_BORBOTONES.Identidad
		WHERE TipoIdentidad = 'Cliente'
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- MIGRACION Regimen

INSERT INTO LOS_BORBOTONES.Regimen(Codigo, Descripcion, Precio, Activo)
		SELECT  DISTINCT 'RGAI', m.Regimen_Descripcion, m.Regimen_Precio, 1
		FROM gd_esquema.maestra m
		WHERE  m.Regimen_Descripcion = 'All inclusive'
GO

INSERT INTO LOS_BORBOTONES.Regimen(Codigo, Descripcion, Precio, Activo)
		SELECT  DISTINCT 'RGAIM', m.Regimen_Descripcion, m.Regimen_Precio, 1
		FROM gd_esquema.maestra m
		WHERE  m.Regimen_Descripcion = 'All Inclusive moderado'
GO

INSERT INTO LOS_BORBOTONES.Regimen(Codigo, Descripcion, Precio, Activo)
		SELECT  DISTINCT 'RGPC', m.Regimen_Descripcion, m.Regimen_Precio, 1
		FROM gd_esquema.maestra m
		WHERE  m.Regimen_Descripcion = 'Pension Completa'
GO

INSERT INTO LOS_BORBOTONES.Regimen(Codigo, Descripcion, Precio, Activo)
		SELECT  DISTINCT 'RGMP', m.Regimen_Descripcion, m.Regimen_Precio, 1
		FROM gd_esquema.maestra m
		WHERE  m.Regimen_Descripcion = 'Media Pensión'
GO


INSERT INTO LOS_BORBOTONES.Regimen_X_Hotel(idHotel,idRegimen)
SELECT H.idHotel, R.idRegimen FROM gd_esquema.Maestra AS M
JOIN LOS_BORBOTONES.Hotel AS H ON H.Nombre=LOS_BORBOTONES.concatenarNombreHotel(M.Hotel_Calle,M.Hotel_Nro_Calle)
JOIN LOS_BORBOTONES.Regimen AS R ON R.Descripcion=M.Regimen_Descripcion
GROUP BY idHotel ,idRegimen
ORDER BY idHotel ,idRegimen;
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Migración Reserva y Estadía
--------------------------------------------------------------------------------

--EN ESTA ETAPA SOLO MIGRO LAS RESERVAS Y ESTADIAS QUE YA FUERON FACTURADAS (YA FINALIZARON COMPLETAMENTE SU CICLO)
--(Estado Reserva: RF, Migro la Estadia, Sus consumibles, Su Factura, Sus item Factura)

--Primer Tabla Temporal de la Migración
SELECT DISTINCT
	  (SELECT hotel.idHotel
	  FROM LOS_BORBOTONES.Hotel hotel
	  WHERE hotel.Nombre = LOS_BORBOTONES.concatenarNombreHotel(Hotel_Calle, Hotel_Nro_Calle)) as idHotel
      ,(SELECT regimen.idRegimen
	  FROM LOS_BORBOTONES.Regimen regimen
	  WHERE regimen.Descripcion = Regimen_Descripcion) as idRegimen
      ,[Habitacion_Numero]
      ,[Habitacion_Piso]
      ,[Reserva_Fecha_Inicio]
      ,[Reserva_Codigo]
      ,[Reserva_Cant_Noches]
      ,[Cliente_Pasaporte_Nro]
	  ,[Cliente_Nombre]
	  ,[Cliente_Apellido]
INTO LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva
FROM [GD1C2018].[gd_esquema].[Maestra]
WHERE Estadia_Fecha_Inicio IS NOT NULL
  AND DATEADD(day, Estadia_Cant_Noches, Estadia_Fecha_Inicio) < LOS_BORBOTONES.fn_getDate() --LINEA QUE FILTRA SOLO LAS ESTADIAS QUE YA FINALIZARON
  AND Factura_Fecha <= LOS_BORBOTONES.fn_getDate() --LINEA QUE FILTRA LAS QUE YA FUERON FACTURADAS
ORDER BY Reserva_Codigo
GO

--Segunda Tabla Temporal de la Migración
SELECT cliente.idCliente, identidad.NumeroDocumento, identidad.Nombre, identidad.Apellido
INTO LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva
FROM LOS_BORBOTONES.Identidad identidad
    ,LOS_BORBOTONES.Cliente cliente
WHERE identidad.TipoIdentidad = 'Cliente'
  AND cliente.idIdentidad = identidad.idIdentidad
  AND identidad.TipoDocumento = 'Pasaporte'
GO

--CURSOR DE LA Migración Reserva y Estadía
DECLARE migracionReservasYEstadias CURSOR FOR 
SELECT ptemp.idHotel
	  ,ptemp.idRegimen
	  ,stemp.idCliente
      ,ptemp.Reserva_Fecha_Inicio
      ,ptemp.Reserva_Codigo
      ,ptemp.Reserva_Cant_Noches
FROM LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva ptemp
	,LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva stemp
WHERE ptemp.Cliente_Pasaporte_Nro = stemp.NumeroDocumento
  AND ptemp.Cliente_Nombre = stemp.Nombre
  AND Cliente_Apellido = stemp.Apellido
ORDER BY Reserva_Codigo

DECLARE @idHotel INT,
		@idRegimen INT,
		@idCliente INT,
		@Reserva_Fecha_Inicio DATETIME,
		@Reserva_Codigo NUMERIC(18,0),		
		@Reserva_Cant_Noches NUMERIC(18,0)

OPEN migracionReservasYEstadias

-- Perform the first fetch.
FETCH NEXT FROM migracionReservasYEstadias
INTO @idHotel, @idRegimen, @idCliente, @Reserva_Fecha_Inicio, @Reserva_Codigo, @Reserva_Cant_Noches;

-- Check @@FETCH_STATUS to see if there are any more rows to fetch.
WHILE @@FETCH_STATUS = 0
BEGIN

    INSERT INTO LOS_BORBOTONES.Estadia(FechaEntrada, FechaSalida, CantidadNoches, idUsuarioIn, idUsuarioOut)
	VALUES (@Reserva_Fecha_Inicio, DATEADD(DAY, @Reserva_Cant_Noches, @Reserva_Fecha_Inicio), @Reserva_Cant_Noches, 2, 2) --Uso el Valor del Usuario 2 para todos (recepcionista)
	
	DECLARE @idEstadia INT
	SET @idEstadia = SCOPE_IDENTITY();
	
	--PONGO QUE LA FECHA DE CREACION DE LAS RESERVAS MIGRADAS ES LA FECHA-DESDE DE LA RESERVA
	INSERT INTO LOS_BORBOTONES.Reserva(CodigoReserva, FechaCreacion, FechaDesde,  FechaHasta, DiasAlojados, idHotel, idEstadia, idRegimen, idCliente)
	VALUES (@Reserva_Codigo, @Reserva_Fecha_Inicio, @Reserva_Fecha_Inicio, DATEADD(DAY, @Reserva_Cant_Noches, @Reserva_Fecha_Inicio), @Reserva_Cant_Noches, @idHotel, @idEstadia, @idRegimen, @idCliente)

    -- This is executed as long as the previous fetch succeeds.
    FETCH NEXT FROM migracionReservasYEstadias
    INTO @idHotel, @idRegimen, @idCliente, @Reserva_Fecha_Inicio, @Reserva_Codigo, @Reserva_Cant_Noches;
END

CLOSE migracionReservasYEstadias
DEALLOCATE migracionReservasYEstadias
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Migracion Consumible

INSERT INTO LOS_BORBOTONES.Consumible(Codigo, Descripcion, Precio)
		SELECT DISTINCT Consumible_Codigo, Consumible_Descripcion, Consumible_Precio
		FROM gd_esquema.maestra 
		WHERE Consumible_Codigo IS NOT NULL
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Migracion TipoHabitacion

INSERT INTO LOS_BORBOTONES.TipoHabitacion(Codigo, Descripcion, Porcentual) 
		SELECT DISTINCT  Habitacion_Tipo_Codigo, Habitacion_Tipo_Descripcion, Habitacion_Tipo_Porcentual
		FROM gd_esquema.maestra
		WHERE Habitacion_Tipo_Codigo IS NOT NULL

GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Migracion Habitacion

INSERT INTO LOS_BORBOTONES.Habitacion(Numero, Piso, Ubicacion, idHotel, idTipoHabitacion) 
		SELECT DISTINCT m.Habitacion_Numero, m.Habitacion_Piso, (CASE WHEN m.Habitacion_Frente = 'S' THEN 'Vista Exterior' ELSE 'Interior' END), h.idHotel, t.idTipoHabitacion
		FROM LOS_BORBOTONES.Hotel h
		JOIN gd_esquema.maestra m ON LOS_BORBOTONES.concatenarNombreHotel(m.Hotel_Calle, m.Hotel_Nro_Calle) = h.Nombre
		JOIN LOS_BORBOTONES.TipoHabitacion t ON m.Habitacion_Tipo_Codigo = t.Codigo
		WHERE t.Codigo IS NOT NULL
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--Carga Hotel_X_Usuario

INSERT INTO LOS_BORBOTONES.Hotel_X_Usuario(idHotel, idUsuario) 
		SELECT DISTINCT r.idHotel, e.idUsuarioIn
		FROM LOS_BORBOTONES.Reserva r
		JOIN LOS_BORBOTONES.Hotel h  
			ON r.idHotel = h.idHotel
		JOIN LOS_BORBOTONES.Estadia e
			ON r.idEstadia = e.idEstadia
		JOIN LOS_BORBOTONES.Usuario u 	
			ON e.idUsuarioIn =  u.idUsuario	OR e.idUsuarioOut =  u.idUsuario
	ORDER BY r.idHotel, e.idUsuarioIn
GO

--El admin que se pide trabaja en todos los hoteles
-------------------------------------------------------------------------------

INSERT INTO LOS_BORBOTONES.Hotel_X_Usuario (idHotel, idUsuario)
SELECT h.idHotel, u.idUsuario
FROM LOS_BORBOTONES.Hotel h
CROSS JOIN LOS_BORBOTONES.Usuario u
WHERE u.Username = 'admin';
GO

--El admin que asumimos del enunciado tambien trabaja en todos los hoteles
-------------------------------------------------------------------------------

INSERT INTO LOS_BORBOTONES.Hotel_X_Usuario (idHotel, idUsuario)
SELECT h.idHotel, u.idUsuario
FROM LOS_BORBOTONES.Hotel h
CROSS JOIN LOS_BORBOTONES.Usuario u
WHERE u.Username = 'adminOriginal';
GO

--El usuario recepcionista trabaja en el hotel 1 y 3
-------------------------------------------------------------------------------

INSERT INTO LOS_BORBOTONES.Hotel_X_Usuario (idHotel, idUsuario)
SELECT h.idHotel, u.idUsuario
FROM LOS_BORBOTONES.Hotel h, LOS_BORBOTONES.Usuario u
WHERE u.Username = 'recepcionista'
AND (h.idHotel = 1 OR h.idhotel = 3)
GO

------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Migracion Factura (*) trae montototal con importes inconsistentes, se corrige luego de migrar itemFactura
-- se define tipoDePago Efectivo, para los clientes migrados de la tablaMaestra

INSERT INTO LOS_BORBOTONES.Factura(NumeroFactura, FechaFacturacion, Total, TipoPago, e.idEstadia, r.idReserva)
		SELECT DISTINCT  m.Factura_Nro, m.Factura_Fecha, m.Factura_Total, 'Efectivo', e.idEstadia, r.idReserva
		FROM  LOS_BORBOTONES.Estadia e
		JOIN gd_esquema.maestra m 
			ON  m.Estadia_Fecha_Inicio = e.FechaEntrada AND m.Estadia_Cant_Noches = e.CantidadNoches
		JOIN LOS_BORBOTONES.Reserva r 
			ON  m.Reserva_Codigo = r.CodigoReserva AND r.idEstadia = e.idEstadia
			WHERE m.Factura_Nro IS NOT NULL
		ORDER BY  m.Factura_Nro, m.Factura_Fecha, e.idEstadia, r.idReserva
GO

--------------------------------------------------------------------------------------------------------------------------------------------------------------
--ItemFactura
--Estan intercambiados en la tabla maestra la cantidad y el monto de los item factura, por lo cual se invierten en la query.
--En la tabla maestra todas las estadias tienen 3 consumibles asociados.
--Se corrige la migracion de los Item Factura.
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO LOS_BORBOTONES.ItemFactura(Cantidad, Monto, FechaCreacion, idFactura, idConsumible)
SELECT 
	COUNT(c.idConsumible) AS CantidadReal
	,m.Item_Factura_Cantidad AS Monto
	,f.FechaFacturacion AS FechaCreacion
	,f.idFactura AS idFactura
	,c.idConsumible AS idConsumible
FROM LOS_BORBOTONES.Factura f
	,LOS_BORBOTONES.Consumible c
    ,gd_esquema.maestra m
WHERE m.Factura_Nro = f.NumeroFactura
  AND m.Consumible_Codigo = c.Codigo
GROUP BY f.idFactura, f.FechaFacturacion, c.idConsumible, m.Item_Factura_Cantidad
ORDER BY f.idFactura
GO

--Creacion de Tabla Temporal para guardar los importes segun el precio del regimen, cantidad de dias y cantidad de consumibles incluidos en itemFactura.
-- El regimen All inclusive no carga consumibles en el monto total.

SELECT  DISTINCT f.NumeroFactura, r.idReserva, g.Descripcion, sum(c.Precio * i.Cantidad) AS CostoConsumible, LOS_BORBOTONES.fn_costoTotalEstadia(g.Precio, e.CantidadNoches) AS CostoEstadia,
		CASE g.Descripcion WHEN 'All Inclusive' THEN LOS_BORBOTONES.fn_costoTotalEstadia(g.Precio, e.CantidadNoches)
		ELSE sum(c.Precio * i.Cantidad) + LOS_BORBOTONES.fn_costoTotalEstadia(g.Precio, e.CantidadNoches) END "MontoTotal"
  INTO LOS_BORBOTONES.temporalMontoFactura
	FROM  LOS_BORBOTONES.Factura f
		JOIN  LOS_BORBOTONES.Reserva r
			ON  f.idReserva = r.idReserva
		JOIN  LOS_BORBOTONES.Estadia e
			ON   r.idEstadia = e.idEstadia
		JOIN LOS_BORBOTONES.Regimen g
			ON r.idRegimen = g.idRegimen
		JOIN LOS_BORBOTONES.ItemFactura i
			ON f.idFactura = i.idFactura
		JOIN LOS_BORBOTONES.Consumible c
			ON i.idConsumible = c.idConsumible
		WHERE  f.idFactura = i.idFactura
	GROUP BY f.NumeroFactura, r.idReserva, g.Descripcion, g.Precio, e.CantidadNoches
GO
	
--se corrige el montototal de cada factura, y los puntos obtenidos, de acuerdo al enunciado, teniendo en cuenta algunos campos de itemFactura
	 
UPDATE  t1
SET t1.Total =   t2.MontoTotal,
	t1.Puntos = LOS_BORBOTONES.fn_puntoTotalConsumible(t2.CostoConsumible) + LOS_BORBOTONES.fn_puntoTotalEstadia(t2.CostoEstadia)
FROM  LOS_BORBOTONES.Factura t1
		JOIN  LOS_BORBOTONES.temporalMontoFactura t2
		ON  t1.NumeroFactura = t2.NumeroFactura
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Carga EstadoReserva, todas las cargo como FACTURADAS PORQUE HASTA AHORA SOLO MIGRE LAS QUE YA HABIAN SIDO FACTURADAS

INSERT INTO LOS_BORBOTONES.EstadoReserva(TipoEstado, Fecha, Descripcion, idUsuario, idReserva)
	SELECT DISTINCT 'RF', r.FechaCreacion, 'Reserva Facturada', u.idUsuario, r.idReserva
	FROM LOS_BORBOTONES.Reserva r
		,LOS_BORBOTONES.Usuario u
	WHERE u.Username = 'admin'
	  AND r.idEstadia IS NOT NULL
	ORDER BY r.idReserva, r.FechaCreacion
GO

--------------------------------------------------------------------------------------------------------------------------------
--  LAS INCONSISTENCIAS DE LOS CLIENTES SE MANEJAN DE LA SIGUIENTE MANERA:
-- SI HAY DOS CLIENTES
--	SE MARCA EL CLIENTE COMO INCONSISTENTE, Y LA PROXIMA VEZ QUE SE INTENTA RESERVAR CON UN CLIENTE O MODIFICARLO
--	SE MUESTRA POR GUI UN CARTEL QUE DEBE ACTUALIZAR LOS DATOS PRIMERO.
--  INCONSISTENCIA: MAIL DUPLICADO O TIPO Y NUMERO DE DOCUMENTO DUPLICADO.
--------------------------------------------------------------------------------------------------------------------------------

--TemporalInconsistencias para mejorar performance cursor
SELECT cliente2.idCliente as idClienteInconsistente
INTO LOS_BORBOTONES.TemporalInconsistencias
FROM LOS_BORBOTONES.Cliente cliente1
	,LOS_BORBOTONES.Cliente cliente2
	,LOS_BORBOTONES.Identidad id1
	,LOS_BORBOTONES.Identidad id2
	WHERE cliente1.idIdentidad = id1.idIdentidad
	AND cliente2.idIdentidad = id2.idIdentidad
	AND id2.NumeroDocumento = id1.NumeroDocumento
	AND id2.TipoDocumento = id1.TipoDocumento
	AND id1.idIdentidad < id2.idIdentidad
UNION
SELECT cliente1.idCliente as idClienteInconsistente
FROM LOS_BORBOTONES.Cliente cliente1
	,LOS_BORBOTONES.Cliente cliente2
	,LOS_BORBOTONES.Identidad id1
	,LOS_BORBOTONES.Identidad id2
WHERE cliente1.idIdentidad = id1.idIdentidad
  AND cliente2.idIdentidad = id2.idIdentidad
  AND id2.NumeroDocumento = id1.NumeroDocumento
  AND id2.TipoDocumento = id1.TipoDocumento
  AND id1.idIdentidad < id2.idIdentidad
GO

INSERT INTO LOS_BORBOTONES.TemporalInconsistencias
SELECT cliente2.idCliente as idClienteInconsistente
FROM LOS_BORBOTONES.Cliente cliente1
	,LOS_BORBOTONES.Cliente cliente2
	,LOS_BORBOTONES.Identidad id1
	,LOS_BORBOTONES.Identidad id2
WHERE cliente1.idIdentidad = id1.idIdentidad
  AND cliente2.idIdentidad = id2.idIdentidad
  AND id2.Mail = id1.Mail
  AND id1.idIdentidad < id2.idIdentidad
  --AND cliente2.idCliente NOT IN (SELECT idClienteInconsistente as idCliente FROM LOS_BORBOTONES.TemporalInconsistencias)
UNION
SELECT cliente1.idCliente as idClienteInconsistente
FROM LOS_BORBOTONES.Cliente cliente1
	,LOS_BORBOTONES.Cliente cliente2
	,LOS_BORBOTONES.Identidad id1
	,LOS_BORBOTONES.Identidad id2
WHERE cliente1.idIdentidad = id1.idIdentidad
  AND cliente2.idIdentidad = id2.idIdentidad
  AND id2.Mail = id1.Mail
  AND id1.idIdentidad < id2.idIdentidad
  --AND cliente2.idCliente NOT IN (SELECT idClienteInconsistente as idCliente FROM LOS_BORBOTONES.TemporalInconsistencias)
GO

/*
INSERT INTO LOS_BORBOTONES.TemporalInconsistencias
SELECT cliente2.idCliente as idClienteInconsistente
FROM LOS_BORBOTONES.Cliente cliente1
	,LOS_BORBOTONES.Cliente cliente2
	,LOS_BORBOTONES.Identidad id1
	,LOS_BORBOTONES.Identidad id2
WHERE cliente1.idIdentidad = id1.idIdentidad
  AND cliente2.idIdentidad = id2.idIdentidad
  AND id2.Mail = id1.Mail
  AND id1.idIdentidad < id2.idIdentidad
  --AND cliente2.idCliente NOT IN (SELECT idClienteInconsistente as idCliente FROM LOS_BORBOTONES.TemporalInconsistencias)
GO
*/

DECLARE cursorClienteInconsistente CURSOR FOR
SELECT DISTINCT * 
FROM LOS_BORBOTONES.TemporalInconsistencias
ORDER BY idClienteInconsistente

DECLARE @idClienteInconsistente INT

OPEN cursorClienteInconsistente

-- Perform the first fetch.
FETCH NEXT FROM cursorClienteInconsistente
INTO @idClienteInconsistente

-- Check @@FETCH_STATUS to see if there are any more rows to fetch.
WHILE @@FETCH_STATUS = 0
BEGIN

	--SETEO EL BIT DE INCONSISTENTE
	UPDATE LOS_BORBOTONES.Cliente
	SET Inconsistente = 1
	WHERE idCliente = @idClienteInconsistente;
	
    -- This is executed as long as the previous fetch succeeds.
    FETCH NEXT FROM cursorClienteInconsistente
    INTO @idClienteInconsistente
END

CLOSE cursorClienteInconsistente
DEALLOCATE cursorClienteInconsistente

GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Creación Consumible Habitacion

INSERT INTO LOS_BORBOTONES.Consumible(Codigo,Descripcion,Precio) 
VALUES (1,'Costo por habitacion',0)		
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Creación Consumible All inclusive

INSERT INTO LOS_BORBOTONES.Consumible(Codigo,Descripcion,Precio) 
VALUES (2,'Descuento por regimen de estadia',0)		
GO

--------------------------------------------------------------------------------------
-------INSERT DE RESERVAS FUTURAS-----------------------------------------------------
--------------------------------------------------------------------------------------

--ESTAS SON LAS RESERVAS QUE ESTAN EN LA BASE MAESTRA, QUE COMIENZAN LUEGO DE LA "FECHA DE HOY"
--PARA ESTAS RESERVAS NO SE MIGRAN NI ESTADIAS, NI CONSUMIBLES, NI ITEM FACTURA, NI FACTURA
--ESTADO "RC" RESERVA CORRECTA

-- REUSO LAS TABLAS TEMPORALES
IF OBJECT_ID('LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva', 'U') IS NOT NULL
	DROP TABLE LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva;
GO

IF OBJECT_ID('LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva', 'U') IS NOT NULL
	DROP TABLE LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva;
GO

SELECT DISTINCT
	  Reserva_Codigo as CodigoReserva
	  ,Reserva_Fecha_Inicio as FechaDesde
	  ,Reserva_Cant_Noches as DiasAlojados
	  ,Hotel_Calle as HotelCalle
	  ,Hotel_Nro_Calle as HotelNroCalle
	  ,Regimen_Descripcion as Regimen
INTO LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva
FROM [GD1C2018].[gd_esquema].[Maestra]
WHERE Factura_Nro IS NOT NULL
  AND Reserva_Fecha_Inicio >= LOS_BORBOTONES.fn_getDate()
ORDER BY [Reserva_Codigo]
GO

SELECT DISTINCT
	  Reserva_Codigo as CodigoReserva
	  ,Cliente_Mail as ClienteMail
	  ,Cliente_Pasaporte_Nro as ClientePasaporte
	  ,cliente.idCliente as idCliente
INTO LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva
FROM [GD1C2018].[gd_esquema].[Maestra]
	,LOS_BORBOTONES.Identidad identidad
	,LOS_BORBOTONES.Cliente cliente
WHERE Factura_Nro IS NOT NULL
  AND Reserva_Fecha_Inicio >= LOS_BORBOTONES.fn_getDate()
  AND cliente.idIdentidad = identidad.idIdentidad
  AND identidad.NumeroDocumento = Cliente_Pasaporte_Nro
  AND identidad.TipoDocumento = 'Pasaporte'
  AND identidad.Mail = Cliente_Mail
ORDER BY Cliente_Pasaporte_Nro
GO

INSERT INTO LOS_BORBOTONES.Reserva(CodigoReserva, FechaCreacion, FechaDesde,  FechaHasta, DiasAlojados, idHotel, idEstadia, idRegimen, idCliente)
SELECT temp1.CodigoReserva as CodigoReserva
	  ,LOS_BORBOTONES.fn_getDate() as FechaCreacion
	  ,temp1.FechaDesde as FechaDesde
	  ,DATEADD(day, temp1.DiasAlojados, temp1.FechaDesde) as FechaHasta
	  ,temp1.DiasAlojados as DiasAlojados
	  ,(SELECT hotel.idHotel
		FROM LOS_BORBOTONES.Hotel hotel
		WHERE hotel.Nombre = LOS_BORBOTONES.concatenarNombreHotel(HotelCalle, HotelNroCalle)) as idHotel
	  ,NULL as idEstadia
	  ,(SELECT regimen.idRegimen
		FROM LOS_BORBOTONES.Regimen regimen
		WHERE regimen.Descripcion = temp1.Regimen
		) as idRegimen
	  ,temp2.idCliente as idCliente
FROM LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva temp1
	,LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva temp2
WHERE temp1.CodigoReserva = temp2.CodigoReserva
GO

INSERT INTO LOS_BORBOTONES.EstadoReserva(TipoEstado, Fecha, Descripcion, idUsuario, idReserva)
	SELECT DISTINCT 'RC', LOS_BORBOTONES.fn_getDate(), 'Reserva Correcta', u.idUsuario, r.idReserva
	FROM LOS_BORBOTONES.Reserva r
		,LOS_BORBOTONES.Usuario u
	WHERE u.Username = 'admin'
	  AND r.idEstadia IS NULL
	ORDER BY r.idReserva
GO

--------------------------------------------------------------------------------------
--INSERT DE RESERVAS QUE YA FINALIZARON SU ESTADIA PERO NO FUERON FACTURADAS TODAVIA -
-------------(TIENEN SU FECHA DE FACTURACION EN EL FUTURO)----------------------------
---------(TIENEN SU FECHA DE FIN DE ESTADIA ANTES QUE LA 'FECHA DE HOY'---------------
--------------------------------------------------------------------------------------

--PARA ESTAS RESERVAS SE MIGRAN SU ESTADIA Y SUS CONSUMIBLES, PERO NO SUS ITEM FACTURA NI SU FACTURA
--ESTADO "RCE" RESERVA CON EGRESO, SE PODRIAN FACTURAR

-- REUSO LAS TABLAS TEMPORALES
IF OBJECT_ID('LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva', 'U') IS NOT NULL
	DROP TABLE LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva;
GO

IF OBJECT_ID('LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva', 'U') IS NOT NULL
	DROP TABLE LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva;
GO

SELECT DISTINCT
	  Reserva_Codigo as CodigoReserva
	  ,Reserva_Fecha_Inicio as FechaDesde
	  ,Reserva_Cant_Noches as DiasAlojados
	  ,Hotel_Calle as HotelCalle
	  ,Hotel_Nro_Calle as HotelNroCalle
	  ,Regimen_Descripcion as Regimen
INTO LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva
FROM [GD1C2018].[gd_esquema].[Maestra]
WHERE Factura_Nro IS NOT NULL
  AND Estadia_Fecha_Inicio < LOS_BORBOTONES.fn_getDate()
  AND DATEADD(day, Reserva_Cant_Noches, Reserva_Fecha_Inicio) < LOS_BORBOTONES.fn_getDate()
  AND Reserva_Codigo NOT IN (SELECT CodigoReserva FROM LOS_BORBOTONES.Reserva)
ORDER BY [Reserva_Codigo]
GO

SELECT DISTINCT
	  Reserva_Codigo as CodigoReserva
	  ,Cliente_Mail as ClienteMail
	  ,Cliente_Pasaporte_Nro as ClientePasaporte
	  ,cliente.idCliente as idCliente
INTO LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva
FROM [GD1C2018].[gd_esquema].[Maestra]
	,LOS_BORBOTONES.Identidad identidad
	,LOS_BORBOTONES.Cliente cliente
WHERE Factura_Nro IS NOT NULL
  AND Estadia_Fecha_Inicio < LOS_BORBOTONES.fn_getDate()
  AND DATEADD(day, Reserva_Cant_Noches, Reserva_Fecha_Inicio) < LOS_BORBOTONES.fn_getDate()
  AND Reserva_Codigo NOT IN (SELECT CodigoReserva FROM LOS_BORBOTONES.Reserva)
  AND cliente.idIdentidad = identidad.idIdentidad
  AND identidad.NumeroDocumento = Cliente_Pasaporte_Nro
  AND identidad.TipoDocumento = 'Pasaporte'
  AND identidad.Mail = Cliente_Mail
ORDER BY Cliente_Pasaporte_Nro
GO

--MAS CURSORES...
DECLARE migracionReservasEnCheckOut CURSOR FOR 
SELECT temp1.CodigoReserva as CodigoReserva
	  ,temp1.FechaDesde as FechaCreacion
	  ,temp1.FechaDesde as FechaDesde
	  ,DATEADD(day, temp1.DiasAlojados, temp1.FechaDesde) as FechaHasta
	  ,temp1.DiasAlojados as DiasAlojados
	  ,(SELECT hotel.idHotel
		FROM LOS_BORBOTONES.Hotel hotel
		WHERE hotel.Nombre = LOS_BORBOTONES.concatenarNombreHotel(HotelCalle, HotelNroCalle)) as idHotel
	  ,(SELECT regimen.idRegimen
		FROM LOS_BORBOTONES.Regimen regimen
		WHERE regimen.Descripcion = temp1.Regimen
		) as idRegimen
	  ,temp2.idCliente as idCliente
	  ,usuario.idUsuario as idUsuario
FROM LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva temp1
	,LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva temp2
	,LOS_BORBOTONES.Usuario usuario
WHERE temp1.CodigoReserva = temp2.CodigoReserva
  AND usuario.Username = 'admin'

DECLARE @CodigoReserva INT,
		@FechaCreacion DATETIME,
		@FechaDesde DATETIME,
		@FechaHasta DATETIME,
		@DiasAlojados INT,		
		@idHotel INT,
		@idRegimen INT,
		@idCliente INT,
		@idUsuario INT

OPEN migracionReservasEnCheckOut

-- Perform the first fetch.
FETCH NEXT FROM migracionReservasEnCheckOut
INTO @CodigoReserva, @FechaCreacion, @FechaDesde, @FechaHasta, @DiasAlojados, @idHotel, @idRegimen, @idCliente, @idUsuario;

-- Check @@FETCH_STATUS to see if there are any more rows to fetch.
WHILE @@FETCH_STATUS = 0
BEGIN

	INSERT INTO LOS_BORBOTONES.Estadia(FechaEntrada, FechaSalida, CantidadNoches, Facturada, idUsuarioIn, idUsuarioOut)
	VALUES (@FechaDesde, @FechaHasta, @DiasAlojados, 0, @idUsuario, @idUsuario)
	
	DECLARE @idEstadia INT
	SET @idEstadia = SCOPE_IDENTITY();

	INSERT INTO LOS_BORBOTONES.Reserva(CodigoReserva, FechaCreacion, FechaDesde,  FechaHasta, DiasAlojados, idHotel, idEstadia, idRegimen, idCliente)
	VALUES (@CodigoReserva, @FechaCreacion, @FechaDesde, @FechaHasta, @DiasAlojados, @idHotel, @idEstadia, @idRegimen, @idCliente)
	
	DECLARE @idReserva INT
	SET @idReserva = SCOPE_IDENTITY();
	
	INSERT INTO LOS_BORBOTONES.EstadoReserva(TipoEstado, Fecha, Descripcion, idUsuario, idReserva)
	VALUES ('RCE', LOS_BORBOTONES.fn_getDate(), 'Reserva Con Egreso', @idUsuario, @idReserva)

    -- This is executed as long as the previous fetch succeeds.
    FETCH NEXT FROM migracionReservasEnCheckOut
    INTO @CodigoReserva, @FechaCreacion, @FechaDesde, @FechaHasta, @DiasAlojados, @idHotel, @idRegimen, @idCliente, @idUsuario;
END

CLOSE migracionReservasEnCheckOut
DEALLOCATE migracionReservasEnCheckOut
GO

------------------------------------------------------------------------------------------
--INSERT DE RESERVAS QUE YA COMENZARON SU ESTADIA PERO AUN NO TERMINA (ESTAN EN EL HOTEL)-
-------------(TIENEN SU FECHA DE FACTURACION EN EL FUTURO)--------------------------------
---------(TIENEN SU FECHA DE FIN DE ESTADIA DESPUES QUE LA 'FECHA DE HOY')----------------
------------------------------------------------------------------------------------------

--PARA ESTAS RESERVAS SE MIGRAN SU ESTADIA (CON FECHA DE SALIDA NULL) Y SUS CONSUMIBLES, PERO NO SUS ITEM FACTURA NI SU FACTURA
--ESTADO "RCI" RESERVA CON INGRESO, SE PODRIA HACER EL CHECKOUT ANTES DE TIEMPO Y VALIDAR EL COMPORTAMIENTO
--ADECUADO DE LAS FUNCIONES SOLICITADAS

-- REUSO LAS TABLAS TEMPORALES
IF OBJECT_ID('LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva', 'U') IS NOT NULL
	DROP TABLE LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva;
GO

IF OBJECT_ID('LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva', 'U') IS NOT NULL
	DROP TABLE LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva;
GO

SELECT DISTINCT
	  Reserva_Codigo as CodigoReserva
	  ,Reserva_Fecha_Inicio as FechaDesde
	  ,Reserva_Cant_Noches as DiasAlojados
	  ,Hotel_Calle as HotelCalle
	  ,Hotel_Nro_Calle as HotelNroCalle
	  ,Regimen_Descripcion as Regimen
INTO LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva
FROM [GD1C2018].[gd_esquema].[Maestra]
 WHERE Reserva_Codigo NOT IN (SELECT CodigoReserva FROM LOS_BORBOTONES.Reserva)
	AND Estadia_Fecha_Inicio IS NOT NULL
	AND Factura_Fecha IS NOT NULL
ORDER BY [Reserva_Codigo]
GO

SELECT DISTINCT
	  Reserva_Codigo as CodigoReserva
	  ,Cliente_Mail as ClienteMail
	  ,Cliente_Pasaporte_Nro as ClientePasaporte
	  ,cliente.idCliente as idCliente
INTO LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva
FROM [GD1C2018].[gd_esquema].[Maestra]
	,LOS_BORBOTONES.Identidad identidad
	,LOS_BORBOTONES.Cliente cliente
WHERE Reserva_Codigo NOT IN (SELECT CodigoReserva FROM LOS_BORBOTONES.Reserva)
  AND Estadia_Fecha_Inicio IS NOT NULL
  AND Factura_Fecha IS NOT NULL
  AND cliente.idIdentidad = identidad.idIdentidad
  AND identidad.NumeroDocumento = Cliente_Pasaporte_Nro
  AND identidad.TipoDocumento = 'Pasaporte'
  AND identidad.Mail = Cliente_Mail
ORDER BY Cliente_Pasaporte_Nro
GO

--MAS CURSORES...
DECLARE migracionReservasEnEstadia CURSOR FOR
SELECT temp1.CodigoReserva as CodigoReserva
	  ,temp1.FechaDesde as FechaCreacion
	  ,temp1.FechaDesde as FechaDesde
	  ,DATEADD(day, temp1.DiasAlojados, temp1.FechaDesde) as FechaHasta
	  ,temp1.DiasAlojados as DiasAlojados
	  ,(SELECT hotel.idHotel
		FROM LOS_BORBOTONES.Hotel hotel
		WHERE hotel.Nombre = LOS_BORBOTONES.concatenarNombreHotel(HotelCalle, HotelNroCalle)) as idHotel
	  ,(SELECT regimen.idRegimen
		FROM LOS_BORBOTONES.Regimen regimen
		WHERE regimen.Descripcion = temp1.Regimen
		) as idRegimen
	  ,temp2.idCliente as idCliente
	  ,usuario.idUsuario as idUsuario
FROM LOS_BORBOTONES.temporalPrimeraMigracionEstadiasYReserva temp1
	,LOS_BORBOTONES.temporalSegundaMigracionEstadiasYReserva temp2
	,LOS_BORBOTONES.Usuario usuario
WHERE temp1.CodigoReserva = temp2.CodigoReserva
  AND usuario.Username = 'admin'
  
DECLARE @CodigoReserva INT,
		@FechaCreacion DATETIME,
		@FechaDesde DATETIME,
		@FechaHasta DATETIME,
		@DiasAlojados INT,		
		@idHotel INT,
		@idRegimen INT,
		@idCliente INT,
		@idUsuario INT

OPEN migracionReservasEnEstadia

-- Perform the first fetch.
FETCH NEXT FROM migracionReservasEnEstadia
INTO @CodigoReserva, @FechaCreacion, @FechaDesde, @FechaHasta, @DiasAlojados, @idHotel, @idRegimen, @idCliente, @idUsuario;

-- Check @@FETCH_STATUS to see if there are any more rows to fetch.
WHILE @@FETCH_STATUS = 0
BEGIN

	INSERT INTO LOS_BORBOTONES.Estadia(FechaEntrada, FechaSalida, CantidadNoches, Facturada, idUsuarioIn, idUsuarioOut)
	VALUES (@FechaDesde, NULL, NULL, 0, @idUsuario, NULL)
	
	DECLARE @idEstadia INT
	SET @idEstadia = SCOPE_IDENTITY();

	INSERT INTO LOS_BORBOTONES.Reserva(CodigoReserva, FechaCreacion, FechaDesde,  FechaHasta, DiasAlojados, idHotel, idEstadia, idRegimen, idCliente)
	VALUES (@CodigoReserva, @FechaCreacion, @FechaDesde, @FechaHasta, @DiasAlojados, @idHotel, @idEstadia, @idRegimen, @idCliente)
	
	DECLARE @idReserva INT
	SET @idReserva = SCOPE_IDENTITY();
	
	INSERT INTO LOS_BORBOTONES.EstadoReserva(TipoEstado, Fecha, Descripcion, idUsuario, idReserva)
	VALUES ('RCI', @FechaCreacion, 'Reserva Con Ingreso', @idUsuario, @idReserva)

    -- This is executed as long as the previous fetch succeeds.
    FETCH NEXT FROM migracionReservasEnEstadia
    INTO @CodigoReserva, @FechaCreacion, @FechaDesde, @FechaHasta, @DiasAlojados, @idHotel, @idRegimen, @idCliente, @idUsuario;
END

CLOSE migracionReservasEnEstadia
DEALLOCATE migracionReservasEnEstadia
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Asociacion Reserva_X_Habitacion_X_Cliente
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO LOS_BORBOTONES.Reserva_X_Habitacion_X_Cliente(idReserva, idHabitacion, idCliente) 
		SELECT DISTINCT r.idReserva, a.idHabitacion, c.idCliente
		FROM LOS_BORBOTONES.Reserva r
		JOIN gd_esquema.maestra m
			ON r.CodigoReserva = m.Reserva_Codigo
		JOIN LOS_BORBOTONES.Cliente c
			ON r.idCliente = c.idCliente
		JOIN LOS_BORBOTONES.Hotel h
			ON r.idHotel = h.idHotel 
		JOIN LOS_BORBOTONES.Habitacion a
			ON h.idHotel = a.idHotel AND m.Habitacion_Numero = a.Numero AND m.Habitacion_Piso = a.Piso
		JOIN LOS_BORBOTONES.TipoHabitacion t
			ON a.idTipoHabitacion = t.idTipoHabitacion AND m.Habitacion_Tipo_Codigo = t.Codigo
	ORDER BY a.idHabitacion, r.idReserva,  c.idCliente
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Asociacion Estadia_X_Consumible
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO LOS_BORBOTONES.Estadia_X_Consumible(idEstadia, idConsumible, Cantidad)
SELECT idEstadia, idConsumible, Cantidad
FROM (
	SELECT DISTINCT 
			[Reserva_Codigo]
			,[Consumible_Codigo]
			,COUNT(*) as Cantidad
			,estadia.idEstadia as idEstadia
			,consumible.idConsumible as idConsumible
	  FROM [GD1C2018].[gd_esquema].[Maestra]
		,LOS_BORBOTONES.Reserva reserva
		,LOS_BORBOTONES.EstadoReserva estado
		,LOS_BORBOTONES.estadia estadia
		,LOS_BORBOTONES.Consumible consumible 
	  WHERE Consumible_Codigo IS NOT NULL
		AND reserva.idReserva = estado.idReserva
		AND estado.TipoEstado IN ('RF','RCE','RCI')
		AND reserva.CodigoReserva = [Reserva_Codigo]
		AND estadia.idEstadia = reserva.idEstadia
		AND Consumible_Codigo = consumible.Codigo
		GROUP BY Reserva_codigo, Consumible_Codigo, estadia.idEstadia, consumible.idConsumible
) AS Temp1
