USE GD1C2013
GO

IF NOT EXISTS (SELECT [schema_id] FROM [sys].[schemas] WHERE [name] = 'DEL_NAVAL')
EXECUTE ('CREATE SCHEMA [DEL_NAVAL]')

if OBJECT_ID ('del_naval.cancelaciones','U') is not null
drop table del_naval.cancelaciones 

if OBJECT_ID ('del_naval.canjes','U') is not null
drop table del_naval.canjes

if OBJECT_ID ('del_naval.productos','U') is not null
drop table del_naval.productos 

if OBJECT_ID ('del_naval.puntos','U') is not null
drop table del_naval.puntos

if OBJECT_ID ('del_naval.servicios_micro','U') is not null
drop table del_naval.servicios_micro

if OBJECT_ID ('del_naval.encomiendas','U') is not null
drop table del_naval.encomiendas

if OBJECT_ID ('del_naval.pasajes','U') is not null
drop table del_naval.pasajes 

if OBJECT_ID ('del_naval.compras_viajes','U') is not null
drop table del_naval.compras_viajes

if OBJECT_ID ('del_naval.compras','U') is not null
drop table del_naval.compras 

if OBJECT_ID ('del_naval.clientes','U') is not null
drop table del_naval.clientes

if OBJECT_ID ('del_naval.butacas_ocupadas','U') is not null
drop table del_naval.butacas_ocupadas

if OBJECT_ID ('del_naval.viajes','U') is not null
drop table del_naval.viajes

if OBJECT_ID ('del_naval.butacas','U') is not null
drop table del_naval.butacas 

if OBJECT_ID ('del_naval.micros','U') is not null
drop table del_naval.micros

if OBJECT_ID ('del_naval.marcas','U') is not null
drop table del_naval.marcas 

if OBJECT_ID ('del_naval.recorridos','U') is not null
drop table del_naval.recorridos 

if OBJECT_ID ('del_naval.tipos_servicio','U') is not null
drop table del_naval.tipos_servicio

if OBJECT_ID ('del_naval.ciudades','U') is not null
drop table del_naval.ciudades 

if OBJECT_ID ('del_naval.usuarios','U') is not null
drop table del_naval.usuarios 

if OBJECT_ID ('del_naval.roles_funcionalidades','U') is not null
drop table del_naval.roles_funcionalidades 

if OBJECT_ID ('del_naval.funcionalidades','U') is not null
drop table del_naval.funcionalidades

if OBJECT_ID ('del_naval.roles','U') is not null
drop table del_naval.roles



create table del_naval.roles (
	id_rol int identity (1,1),
	nombre_rol nvarchar(255) not null unique,
	activo bit,
	constraint pk_rol primary key (id_rol)
)

create table del_naval.funcionalidades (
	id_funcionalidad int identity (1,1),
	nombre_funcionalidad nvarchar(255) not null unique,
	constraint pk_funcionalidad primary key (id_funcionalidad)
)

create table del_naval.roles_funcionalidades (
	rol int not null,
	funcionalidad int not null,
	constraint fk_roles_funcionalidades foreign key (rol) references del_naval.roles (id_rol),
	constraint fk_funcionalidades_roles foreign key (funcionalidad) references del_naval.funcionalidades (id_funcionalidad)
)

create table del_naval.usuarios (
	id_usuario int identity (1,1),
	nombre_usuario nvarchar(255) not null unique,
	rol int,
	pass varchar(255) not null,
	intentos int not null,
	activo bit,
	constraint pk_usuario primary key (id_usuario),
	constraint fk_usuarios_roles foreign key (rol) references del_naval.roles (id_rol)
)

create table del_naval.ciudades (
	id_ciudad int identity (1,1),
	nombre_ciudad nvarchar(255),
	constraint pk_ciudad primary key (id_ciudad)
)

create table del_naval.tipos_servicio (
	id_servicio int identity (1,1),
	nombre_servicio nvarchar(255),
	porcentaje numeric(18,2),
	constraint pk_tipo_servicio primary key (id_servicio)
)

create table del_naval.recorridos (
	id_recorrido int identity (1,1),
	codigo_recorrido numeric(18,0)unique,
	origen int,
	destino int,
	tipo_servicio int,
	precio_base_pasaje numeric(18,2),
	precio_kg_encomienda numeric(18,2),
	constraint pk_recorridos primary key (id_recorrido),
    constraint fk_origen foreign key (origen) references del_naval.ciudades (id_ciudad),
    constraint fk_destino foreign key (destino) references del_naval.ciudades (id_ciudad),
	constraint fk_recorridos_tipo_servicio foreign key (tipo_servicio) references del_naval.tipos_servicio (id_servicio)
)

create table del_naval.marcas (
	id_marca int identity (1,1),
	marca nvarchar(255) unique,
	constraint pk_marcas primary key (id_marca)
)

create table del_naval.micros (
	id_micro int identity (1,1),
	tipo_servicio int,
	kilos_bodega numeric(18,0),
	cantidad_asientos int,
	marca int, 
	modelo nvarchar(255),
	patente nvarchar(255) unique,
	baja_fin_vida_util bit,
	baja_servicio bit,
	fecha_alta datetime,
	fecha_servicio_desde datetime,
	fecha_servicio_hasta datetime,
	fecha_baja datetime,
	constraint pk_micros primary key (id_micro),
	constraint fk_micros_tipos_servicio foreign key (tipo_servicio) references del_naval.tipos_servicio (id_servicio),
	constraint fk_micros_marcas foreign key (marca) references del_naval.marcas (id_marca)
)

create table del_naval.butacas (
	id_butaca int identity (1,1),
	micro int,
	tipo nvarchar(255),
	piso numeric(18,0),
	numero numeric(18,0),
	constraint pk_butacas primary key (id_butaca),
	constraint fk_butacas_micros foreign key (micro) references del_naval.micros (id_micro)
)

create table del_naval.viajes (
	id_viaje int identity (1,1),
	recorrido int,
	micro int,
	fecha_salida datetime,
	fecha_llegada datetime,
	fecha_estimada datetime,
	cancelado bit,
	constraint pk_viajes primary key (id_viaje),
	constraint fk_viajes_recorridos foreign key (recorrido) references del_naval.recorridos (id_recorrido),
	constraint fk_viajes_micros foreign key (micro) references del_naval.micros (id_micro)
)

create table del_naval.butacas_ocupadas (
	id_butaca_ocupada int identity (1,1),
	viaje int,
	butaca int,
	ocupado bit,
	constraint pk_butacas_ocupadas primary key (id_butaca_ocupada),
	constraint fk_butacas_ocupada_viajes foreign key (viaje) references del_naval.viajes (id_viaje),
	constraint fk_butacas_ocupada_butacas foreign key (butaca) references del_naval.butacas (id_butaca)
)

create table del_naval.clientes (
	id_dni numeric(18,0),
	nombre nvarchar(255),
	apellido nvarchar(255),
	fecha_nacimiento datetime,
	sexo char,
	discapacitado bit,
	direccion nvarchar(255),
	telefono numeric(18,0),
	mail nvarchar(255),
	constraint pk_clientes primary key (id_dni)
)

create table del_naval.compras (
	id_voucher int identity (1,1),
	comprador numeric(18,0),
	forma_pago nvarchar (255),
	fecha_compra datetime,
	aux_migracion numeric(18,0), --Se utiliza para no perder la relacion uno a uno entre compra y pasaje o encomienda en los datos del sistema anterior, luego de la migracion se elimina esta columna
	constraint pk_compras primary key (id_voucher),
	constraint fk_compras_clientes foreign key (comprador) references del_naval.clientes (id_dni),
)

create table del_naval.compras_viajes(
	voucher int not null,
	viaje int not null
)

create table del_naval.pasajes (
	id_pasaje int identity (1,1),	
	voucher int,
	viaje int,
	pasajero numeric(18,0),
	butaca int,
	cancelado bit,
	monto numeric(18,2),
	codigo_pasaje numeric(18,0),
	constraint pk_pasajes primary key (id_pasaje)
)

create table del_naval.encomiendas (
	id_encomienda int identity (1,1),	
	voucher int,
	viaje int,
	peso numeric(18,0),
	cancelado bit,
	monto numeric(18,2),
	codigo_encomienda numeric(18,0),
	constraint pk_encomiendas primary key (id_encomienda),
)

create table del_naval.servicios_micro (
	id_servicio int identity (1,1),
	micro int,
	fecha_desde datetime,
	fecha_hasta datetime,
	constraint pk_servicios_micro primary key (id_servicio),
	constraint fk_servicios_micro foreign key (micro) references del_naval.micros (id_micro),
)

create table del_naval.puntos (
	id_punto int identity (1,1),
	cliente numeric(18,0),
	puntos int,
	fecha datetime,
	pasaje int,
	encomienda int,
	usados int,
	constraint pk_puntos primary key (id_punto),
	constraint fk_puntos_clientes foreign key (cliente) references del_naval.clientes (id_dni),
	constraint fk_puntos_pasajes foreign key (pasaje) references del_naval.pasajes (id_pasaje),
	constraint fk_puntos_encomiendas foreign key (encomienda) references del_naval.encomiendas (id_encomienda)
)

create table del_naval.productos (
	id_producto int identity (1,1),
	descripcion nvarchar(255),
	stock int,
	valor_puntos int,
	constraint pk_productos primary key (id_producto),
)

create table del_naval.canjes (
	id_canje int identity (1,1),
	cliente numeric(18,0),
	producto int,
	cantidad int,
	fecha_canje datetime,
	puntos_canjeados int,
	constraint pk_canjes primary key (id_canje),
	constraint fk_canjes_clientes foreign key (cliente) references del_naval.clientes (id_dni),
	constraint fk_canjes_productos foreign key (producto) references del_naval.productos (id_producto),
)

create table del_naval.cancelaciones (
	id_cancelacion int identity (1,1),
	codigo_devolucion nvarchar(255),
	pasaje int,
	encomienda int,
	voucher int,
	fecha_devolucion datetime,
	motivo nvarchar(255),
	constraint pk_cancelaciones primary key (id_cancelacion),
	constraint fk_cancelaciones_pasajes foreign key (pasaje) references del_naval.pasajes (id_pasaje),
	constraint fk_cancelaciones_encomiendas foreign key (encomienda) references del_naval.encomiendas (id_encomienda),
	constraint fk_cancelaciones_compras foreign key (voucher) references del_naval.compras (id_voucher)
)

/*
create table del_naval.puntos_vigentes (
	id_punto_vigente int identity (1,1),
	cliente numeric(18,0),
	fecha_punto datetime,
	puntos_vigentes int,
	constraint pk_puntos_vigentes primary key (id_punto_vigente),
	constraint fk_puntos_vigentes_clientes foreign key (cliente) references del_naval.clientes (id_dni)
	)
*/

-- trigger inhabilitar rol
-- trigger cancelacion de pasajes ante baja de recorrido
-- trigger actualizar asientos y fecha_alta al dar de alta un micro
-- trigger liberar asientos/espacio y devolver plata

