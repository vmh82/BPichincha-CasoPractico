create  database Transferencias
go
use Transferencias
go
create table Persona(
PersonaId int identity(1,1),
Nombre varchar(100),
genero varchar(15),
edad varchar(10),
identificacion varchar(15),
direccion varchar(256),
telefono varchar(256),
primary key(PersonaId)
)
create table Cliente(
ClienteId int identity(1,1),
Contrasena varchar(256),
PersonaId int FOREIGN KEY REFERENCES Persona(PersonaId),
Estado bit,
primary key(ClienteId)
)
go
create table Cuenta(
CuentaId int identity(1,1),
NumeroCuenta int,
Tipo varchar(100),
SaldoInicial decimal (18,2),
Estado bit,
ClienteId int FOREIGN KEY REFERENCES cliente(ClienteId),
MontoDiario decimal(18,2) default(1000),
primary key(CuentaId),
)
go
create table Movimientos(
MovimientoId int identity(1,1),
CuentaId int FOREIGN KEY REFERENCES cuenta(CuentaId),
TipoMovimiento varchar(50),
Valor decimal(18,2),
Saldo decimal(18,2),
FechaMovimiento Datetime,
primary key(MovimientoId),
)
go
use Transferencias
go
if exists(select 1 from sysobjects where name = 'spi_creditodebito')
drop procedure dbo.spi_creditodebito
go
create procedure spi_creditodebito(
@i_numero_cuenta int,
@i_valor decimal(18,2)
)
as begin

	declare @w_montoDiario decimal(18,2),
	@w_saldo_inicial decimal(18,2),
	@w_saldo_anterior decimal(18,2),
	@i_total_operacion decimal(18,2),
	@w_es_saldo_anterior decimal(18,2),
	@w_id_numero_cuenta int,
	@i_tipo_operacion varchar(10)
	--- permite verificar si la operacion es credito(1) o debito(-1)

	select @w_montoDiario = montodiario, @w_saldo_inicial = SaldoInicial,
	@w_id_numero_cuenta = cuentaId
	from Cuenta where NumeroCuenta = @i_numero_cuenta --Consulto el monto diario de la cuenta

	select top 1 @w_saldo_anterior = saldo 
		from movimientos mv
		where cuentaId = @w_id_numero_cuenta order by fechaMovimiento desc 

	SET @w_saldo_anterior = ISNULL(@w_saldo_anterior, @w_saldo_inicial)

	if @i_valor < 0 --DEBITO
	begin
		set @i_tipo_operacion = 'Retiro'
		if @w_saldo_anterior  <= 0
		begin
			RAISERROR ('Saldo no disponible',  16,  1 )
			return
		end
		if ABS(@i_valor) > @w_montoDiario
		begin
			  RAISERROR ('Cupo diario Excedido',  16,  1 );  
			  return
		end
		if ABS(@i_valor) > @w_saldo_anterior
		begin
			  RAISERROR ('Saldo no disponible',  16,  1 );  
			  return
		end
		set @i_total_operacion = isnull(@w_saldo_anterior,0) - ABS(@i_valor)
		
	end
	else
	begin
		set @i_tipo_operacion = 'Deposito'
		set @i_total_operacion = ISNULL(@w_saldo_anterior,0)  + @i_valor

	end
	insert into movimientos values(@w_id_numero_cuenta, @i_tipo_operacion, @i_valor, @i_total_operacion, getdate())

end
go