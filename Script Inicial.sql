USE GD1C2013
GO
SET DATEFORMAT dmy;
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
	cancelado bit,
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

------------------------------------------------------------------------------------------------

USE GD1C2013
GO

insert into DEL_NAVAL.marcas
  select distinct micro_marca 
  from gd_esquema.Maestra

insert into DEL_NAVAL.tipos_servicio
  select distinct tipo_servicio, (round (Pasaje_Precio / recorrido_precio_basepasaje,2) -1)*100
  from gd_esquema.Maestra
  where Recorrido_Precio_BasePasaje <> 0 


insert into DEL_NAVAL.ciudades
select distinct(Recorrido_Ciudad_Destino)
from gd_esquema.Maestra
union
select distinct (recorrido_ciudad_origen)
from gd_esquema.Maestra


insert into DEL_NAVAL.clientes
select distinct(Cli_Dni), Cli_Nombre, Cli_Apellido, Cli_Fecha_Nac, null, 0, Cli_Dir, Cli_Telefono, Cli_Mail
from gd_esquema.Maestra


insert into del_naval.recorridos
select distinct recorrido_codigo, ORI, DES, id_servicio, sum(recorrido_precio_basepasaje), sum(Recorrido_Precio_BaseKG), 0
from(

select distinct recorrido_codigo, ORIG.id_ciudad as ORI, DEST.id_ciudad as DES, id_servicio, recorrido_precio_basepasaje, Recorrido_Precio_BaseKG
 from gd_esquema.Maestra MA 
 inner join GD1C2013.DEL_NAVAL.tipos_servicio TS
 on TS.nombre_servicio = MA.tipo_servicio
 inner join GD1C2013.DEL_NAVAL.ciudades ORIG
 on ORIG.nombre_ciudad = MA.recorrido_ciudad_origen
 inner join GD1C2013.DEL_NAVAL.ciudades DEST
 on DEST.nombre_ciudad = MA.recorrido_ciudad_destino
 
 ) subconsulta
 
 group by recorrido_codigo, ORI, DES, id_servicio
 
 insert into DEL_NAVAL.micros
select t.id_servicio, Micro_KG_Disponibles, MAX(Butaca_Nro) + 1, ma.id_marca, Micro_Modelo, Micro_Patente, 0, 0, null, null, null, null
from gd_esquema.Maestra m, DEL_NAVAL.tipos_servicio t, DEL_NAVAL.marcas ma
where t.nombre_servicio = m.Tipo_Servicio
and marca = micro_marca
group by Micro_Patente, Micro_KG_Disponibles, Micro_Marca, ma.id_marca, Micro_Modelo, t.id_servicio
  
  
  
 insert into del_naval.viajes
select distinct id_recorrido, id_micro, fechasalida, fechallegada, Fecha_LLegada_Estimada, 0 
from gd_esquema.Maestra 
inner join del_naval.micros
on Micro_Patente = patente
inner join del_naval.recorridos
on Recorrido_Codigo = codigo_recorrido

insert into DEL_NAVAL.butacas
select distinct id_micro, Butaca_Tipo, Butaca_Piso, Butaca_Nro
from gd_esquema.Maestra ma, DEL_NAVAL.micros mi
where mi.patente = ma.Micro_Patente
and Butaca_Tipo in ('Ventanilla', 'Pasillo')
order by id_micro, Butaca_Nro

insert into DEL_NAVAL.compras
select ma.Cli_Dni, null,
case when ma.Paquete_FechaCompra > ma.Pasaje_FechaCompra then ma.Paquete_FechaCompra else ma.Pasaje_FechaCompra end,
case when ma.Pasaje_Codigo > ma.Paquete_Codigo then ma.Pasaje_Codigo else ma.Paquete_Codigo end
from gd_esquema.Maestra ma, DEL_NAVAL.viajes vi, DEL_NAVAL.recorridos re, DEL_NAVAL.micros mi
where vi.fecha_estimada = ma.Fecha_LLegada_Estimada
and vi.fecha_salida = ma.FechaSalida
and vi.fecha_llegada = ma.FechaLLegada
and ma.Recorrido_Codigo = re.codigo_recorrido
and vi.recorrido = re.id_recorrido
and ma.Micro_Patente = mi.patente
and vi.micro = mi.id_micro

insert into DEL_NAVAL.compras_viajes
select co.id_voucher, vi.id_viaje
from gd_esquema.Maestra ma, DEL_NAVAL.viajes vi, DEL_NAVAL.compras co, DEL_NAVAL.micros mi, DEL_NAVAL.recorridos re
where vi.fecha_llegada = ma.FechaLLegada
and vi.fecha_salida = ma.FechaSalida
and vi.micro = mi.id_micro
and vi.recorrido = re.id_recorrido
and mi.patente = ma.Micro_Patente
and re.codigo_recorrido = ma.Recorrido_Codigo
and co.comprador = ma.Cli_Dni
and co.fecha_compra = case when ma.Pasaje_FechaCompra > ma.Paquete_FechaCompra then ma.Pasaje_FechaCompra else ma.Paquete_FechaCompra end
and co.aux_migracion = case when ma.Pasaje_Codigo > ma.Paquete_Codigo then ma.Pasaje_Codigo else ma.Paquete_Codigo end

insert into DEL_NAVAL.encomiendas
select co.id_voucher, vi.id_viaje, ma.Paquete_KG, 0, ma.Paquete_Precio, ma.Paquete_Codigo
from gd_esquema.Maestra ma,
DEL_NAVAL.compras co, DEL_NAVAL.viajes vi, DEL_NAVAL.recorridos re, DEL_NAVAL.micros mi, DEL_NAVAL.compras_viajes cv
where cv.viaje = vi.id_viaje
and cv.voucher = co.id_voucher
and vi.recorrido = re.id_recorrido
and vi.micro = mi.id_micro
and ma.Cli_Dni = co.comprador
and ma.Paquete_FechaCompra = co.fecha_compra
and ma.Recorrido_Codigo = re.codigo_recorrido
and ma.Micro_Patente = mi.patente
and ma.Paquete_Codigo = co.aux_migracion
and ma.Paquete_Codigo <> 0

insert into DEL_NAVAL.pasajes
select co.id_voucher, vi.id_viaje, ma.Cli_Dni, bu.id_butaca, 0, ma.Pasaje_Precio, ma.Pasaje_Codigo
from gd_esquema.Maestra ma, DEL_NAVAL.compras_viajes cv,
DEL_NAVAL.compras co, DEL_NAVAL.viajes vi, DEL_NAVAL.recorridos re, DEL_NAVAL.micros mi, DEL_NAVAL.butacas bu
where cv.viaje = vi.id_viaje
and cv.voucher = co.id_voucher
and vi.recorrido = re.id_recorrido
and vi.micro = mi.id_micro
and vi.micro = bu.micro
and ma.Cli_Dni = co.comprador
and ma.Pasaje_FechaCompra = co.fecha_compra
and ma.Recorrido_Codigo = re.codigo_recorrido
and ma.Micro_Patente = mi.patente
and ma.Pasaje_Codigo = co.aux_migracion
and ma.Pasaje_Codigo <> 0
and bu.numero = ma.Butaca_Nro
and bu.piso = ma.Butaca_Piso
and bu.tipo = ma.Butaca_Tipo

insert into DEL_NAVAL.butacas_ocupadas
select vi.id_viaje, bu.id_butaca, 1
from DEL_NAVAL.viajes vi, DEL_NAVAL.pasajes pa, DEL_NAVAL.micros mi, DEL_NAVAL.butacas bu
where vi.micro = mi.id_micro
and pa.butaca = bu.id_butaca
and pa.viaje = vi.id_viaje
and bu.micro = mi.id_micro

--Para la migracion hardcodeamos el valor 5 para tomar $5 por cada punto
insert into del_naval.puntos
select pasajero, cast (monto / 5 as int),  fecha_llegada, id_pasaje, NULL, 0
from DEL_NAVAL.pasajes PA, DEL_NAVAL.viajes VI
where PA.viaje = VI.id_viaje
union
select comprador, cast (monto / 5 as int), fecha_llegada, NULL, id_encomienda, 0
from DEL_NAVAL.encomiendas EN, DEL_NAVAL.viajes VI, DEL_NAVAL.compras CO
where EN.viaje = VI.id_viaje 
and   EN.voucher = CO.id_voucher


--Se elimina la columna aux_migración que solo se utilizo para poder relacionar uno a uno las compras con las encomiendas o pasajes
alter table del_naval.compras 
drop column aux_migracion;

insert into del_naval.productos
Values 
('Termo Lu Milagros', 150, 500),
('Cafetera de bolsillo', 1, 100),
('Disfraz del hombre invisible',2,1500),
('Anteojos infrarojos',0,10),
('Agua pesada (bidon de 5L)',1,50),
('Tp de Gestion de datos 1C 2013 Completo', 0,20000),
('Recoge plátanos', 1,20)
 
insert into del_naval.roles
values
('Administrador',1), --1=activo
('Cliente',1) 
 
 insert into DEL_NAVAL.usuarios
values 
('Jorge_adm',
(select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7',0,1),
 ('Admin',
(select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7',0,1),
 ('usr_adm2',
(select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7',2,1),
 ('usr_adm3',
(select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7',3,0),
  ('client',
(select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Cliente'),'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7',3,0),
  ('cliente2',
(select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Cliente'),'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7',0,1)
 
 


insert into del_naval.funcionalidades
values 
('ABMRoles'),
('ABMRecorridos'),
('ABMMicros'),
('GenerarViajes'), 
('RegistrarLlegadasDestino'),
('ComprarPasajesEncomiendas'),
('CancelarPasajesEncomiendas'),
('ConsultarPuntos'),
('CanjearPuntos'),
('VerListadoEstadistico')

insert into DEL_NAVAL.roles_funcionalidades
values
--alta de funcionalidades administrador
((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),
 (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'ABMRoles')),
 
((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),
  (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'ABMRecorridos')),
 
 ((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),
  (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'ABMMicros')),
 
 ((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),
  (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'GenerarViajes')),
 
 ((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),
  (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'RegistrarLlegadasDestino')),
 
 ((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),
  (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'ComprarPasajesEncomiendas')),
 
 ((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),
 (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'CancelarPasajesEncomiendas')),
 
 ((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),
 (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'ConsultarPuntos')),
 
 ((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),
 (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'CanjearPuntos')),
 
 ((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Administrador'),
 (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'VerListadoEstadistico')),

--alta de funcionalidades cliente
 ((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Cliente'),
  (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'ComprarPasajesEncomiendas')),

 ((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Cliente'),
 (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'ConsultarPuntos')),
 
  ((select id_rol
 from DEL_NAVAL.roles 
 where nombre_rol = 'Cliente'),
 (select id_funcionalidad
 from DEL_NAVAL.funcionalidades 
 where nombre_funcionalidad = 'CanjearPuntos'))
 





--Las siguientes claves foráneas se agregan ahora porque de hacerlo previo a la migración de datos cae la performance considerablmente
alter table del_naval.compras_viajes
add constraint fk_compras_viajes foreign key (voucher) references del_naval.compras (id_voucher)

alter table del_naval.compras_viajes
add constraint fk_viajes_compras foreign key (viaje) references del_naval.viajes (id_viaje)

alter table del_naval.pasajes
add constraint fk_pasajes_compras foreign key (voucher) references del_naval.compras (id_voucher)

alter table del_naval.encomiendas
add constraint fk_encomiendas_compras foreign key (voucher) references del_naval.compras (id_voucher)

alter table del_naval.pasajes
add constraint fk_pasajes_clientes foreign key (pasajero) references del_naval.clientes (id_dni)

alter table del_naval.pasajes
add constraint fk_pasajes_butacas foreign key (butaca) references del_naval.butacas (id_butaca)


----------------------------------------------------------------------------------------------

use GD1C2013
go

--Se hacen drops si los objetos ya existen.

if OBJECT_ID ('PesoLibreEncomiendasXviaje','FN') is not null
drop function PesoLibreEncomiendasXviaje

if OBJECT_ID ('ButacasDisponiblesXviaje','TF') is not null
drop function ButacasDisponiblesXviaje 

if OBJECT_ID ('devolverPasaje','P') is not null
drop procedure devolverPasaje

if OBJECT_ID ('devolverEncomienda','P') is not null
drop procedure devolverEncomienda

if OBJECT_ID ('DEL_NAVAL.bloqueo_usuarios','TR') is not null
  drop trigger DEL_NAVAL.bloqueo_usuarios

if OBJECT_ID ('cancelarViaje','P') is not null
 drop procedure cancelarViaje
 
 
if OBJECT_ID ('inhabilitar_rol','P') is not null
 drop procedure inhabilitar_rol


go

--Devuelve espacio libre en bodega para un viaje determinado
create function PesoLibreEncomiendasXviaje 

(@viaje int)

returns numeric(18,0)

as

Begin
declare @kilosBodega numeric(18,0)
declare @pesoOcupado numeric(18,0)

 set @kilosBodega = (select top 1 MI.kilos_bodega 
 from del_naval.viajes VI, del_naval.micros MI
 where MI.id_micro = VI.micro
 and VI.id_viaje = @viaje
 and VI.cancelado = 0
 )       
 
 set @pesoOcupado = (select SUM (peso)
 from DEL_NAVAL.encomiendas
 where viaje = @viaje
 and cancelado = 0)
 
 return isnull(@kilosBodega - @pesoOcupado,0)       
End;
go

create function ButacasDisponiblesXviaje 
(@viaje int)

returns @butacas_disponibles TABLE
( id_butaca int, 
  micro int,
  tipo nvarchar(255), 
  piso numeric(18,0),
  numero numeric(18,0)
  )
  --,ocupado bit )
  --no traigo el campo ocupado, ya q solo traigo los desocupados
 as
Begin
 insert @butacas_disponibles
 select BU.id_butaca, BU.micro, BU.tipo, BU.piso, BU.numero --,OC.ocupado 
 from del_naval.butacas BU
 left join del_naval.butacas_ocupadas OC
  on OC.viaje = @viaje
 and OC.butaca = BU.id_butaca
 inner join del_naval.viajes VI
 on VI.id_viaje = @viaje
 and BU.micro = VI.micro
 where ((OC.ocupado is null) OR OC.ocupado = 0)
 and VI.cancelado = 0

return 
    
End;
go

--el siguiente procedimiento realiza las operaciones a nivel BD relacionadas con
--La devolución de un pasaje


create procedure devolverPasaje
(@pasaje int,
@codigo_devolucion nvarchar(255),
@fecha_devolucion datetime,
@motivo nvarchar(255),
@monto numeric(18,2) output
)

as
begin

   declare @butaca int
   declare @viaje int
   
   set @butaca = (select butaca 
                  from DEL_NAVAL.pasajes 
                  where id_pasaje = @pasaje)
                  
   set @viaje = (select viaje
                  from DEL_NAVAL.pasajes 
                  where id_pasaje = @pasaje)

--si ya fue cancelado no permite continuar con la cancelacion
  if (select cancelado 
    from DEL_NAVAL.pasajes 
    where id_pasaje = @pasaje) = 1
   begin
    return -1
   end

--para permitir una cancelacion la fecha de cancelacion debe ser menor 
-- a la fecha de salida del viaje   
  if @fecha_devolucion >(select fecha_salida 
                           from del_naval.viajes  
                           where id_viaje = @viaje)
  begin
    return -2                            
  end 
    
   
   

 begin transaction
  insert DEL_NAVAL.cancelaciones
  values 
  (@codigo_devolucion,
   @pasaje, 
   NULL,
   --El enunciado pide registrar voucher:
   (select voucher from del_naval.pasajes where id_pasaje = @pasaje),    
   @fecha_devolucion,
   @motivo)
   
                  
                  
   update DEL_NAVAL.butacas_ocupadas 
    set ocupado = 0
    where viaje = @viaje 
      and butaca = @butaca
    
   
   update DEL_NAVAL.pasajes
   set cancelado = 1  
   where id_pasaje = @pasaje
   
 

  
 set @monto = ( select monto
                  from DEL_NAVAL.pasajes 
                  where id_pasaje = @pasaje) 
                  
commit                  
return 0
End;
go




--el siguiente procedimiento realiza las operaciones a nivel BD relacionadas con
--La devolución de una encomienda

create procedure devolverEncomienda
(@encomienda int,
@codigo_devolucion nvarchar(255),
@fecha_devolucion datetime,
@motivo nvarchar(255),
@monto numeric(18,2) output
)

as
begin

  
   declare @viaje int
                 
   set @viaje = (select viaje
                  from DEL_NAVAL.encomiendas  
                  where id_encomienda  = @encomienda )

--si ya fue cancelado no permite continuar con la cancelacion
  if (select cancelado 
    from DEL_NAVAL.encomiendas  
    where id_encomienda  = @encomienda ) = 1
   begin
    return -1
   end

--para permitir una cancelacion la fecha de cancelacion debe ser menor 
-- a la fecha de salida del viaje   
  if @fecha_devolucion >(select fecha_salida 
                           from del_naval.viajes  
                           where id_viaje = @viaje)
  begin
    return -2                            
  end 
    
   
   

 begin transaction
  insert DEL_NAVAL.cancelaciones
  values 
  (@codigo_devolucion,
   NULL, 
   @encomienda,
   --El enunciado pide registrar voucher:
   (select voucher from del_naval.encomiendas  where id_encomienda  = @encomienda),    
   @fecha_devolucion,
   @motivo)
 
   
   update DEL_NAVAL.encomiendas
   set cancelado = 1  
   where id_encomienda = @encomienda
   
 

  
 set @monto = ( select monto
                  from DEL_NAVAL.encomiendas
                  where id_encomienda = @encomienda) 
                  
commit                  
return 0
End;
go





create trigger bloqueo_usuarios
on del_naval.usuarios
after update
as 
 if (select top 1 intentos from inserted) >= 3
 begin
  update del_naval.usuarios
   set activo = 0
   where id_usuario = (select top 1 id_usuario from inserted)
 end  
 
 go
 
 -----------------------------------------------------------------------------------------------


--el siguiente procedimiento realiza las operaciones a nivel BD relacionadas con
--La cancelacion de un viaje, realizando ademas las cancelaciones correspondientes 
--de todos sus pasajes y encomiendas asociados, 
--utilizando para esto los procedimientos ya creados



create procedure cancelarViaje
(@viaje int,
@codigo_cancelacion nvarchar(255),
@fecha_cancelacion datetime,
@motivo nvarchar(255)
)

as
begin

--utilizamos cursores para ir cancelando uno a uno los pasajes y encomiendas
--pertenecientes a este viaje que estamos cancelando.

begin transaction

--cancelacion pasajes
declare @pasaje int

declare cPasajes cursor for
  select id_pasaje 
   from DEL_NAVAL.pasajes
   where viaje=@viaje
                     
 open cPasajes 
 fetch cPasajes into @pasaje
 
 while (@@FETCH_STATUS = 0)
 begin
  exec devolverPasaje @pasaje,@codigo_cancelacion,@fecha_cancelacion,@motivo,-1 
  fetch cPasajes into @pasaje
 end
 
 close cPasajes
 deallocate cPasajes
 
 --cancelacion encomiendas
 
declare @encomienda int

declare cEncomiendas cursor for 
  select id_encomienda
   from DEL_NAVAL.encomiendas
   where viaje=@viaje     
       
 open cEncomiendas 
 fetch cEncomiendas into @encomienda
 
 while (@@FETCH_STATUS = 0)
 begin
  exec devolverEncomienda @encomienda,@codigo_cancelacion,@fecha_cancelacion,@motivo,-1 
  fetch cEncomiendas into @encomienda
 end
 
 close cEncomiendas
 deallocate cEncomiendas
                  
   update DEL_NAVAL.viajes
   set cancelado = 1
   where id_viaje = @viaje               
                  
commit                  
Return 
End;
go



create procedure inhabilitar_rol
(@rol int)
as
begin
begin transaction

update DEL_NAVAL.roles
set activo = 0
where id_rol = @rol

--Desasignamos los roles a los usuarios y deshabilitamos los usuarios 
--para evitar que un usuario sin rol pueda logearse en el sistema
update DEL_NAVAL.usuarios
set activo = 0,
    rol = NULL
where rol = @rol

commit
end;
go
