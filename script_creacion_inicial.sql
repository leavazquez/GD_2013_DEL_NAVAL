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

if OBJECT_ID ('del_naval.pagos_tarjetas','U') is not null
drop table del_naval.pagos_tarjetas 

if OBJECT_ID ('del_naval.tarjetas','U') is not null
drop table del_naval.tarjetas

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
	codigo_recorrido numeric(18,0), 
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
	numero int,
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
	jubilado_pensionado bit, 
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

create table del_naval.tarjetas (
 id_tarjeta int identity (1,1),
 nombre nvarchar(255),
 cuotas bit,
constraint pk_tarjetas primary key (id_tarjeta)
)

create table del_naval.pagos_tarjetas (
 id_pago_tarjeta int identity (1,1),
 voucher int, 
 tarjeta int,
 numero nvarchar(255),
 cod_ser nvarchar(255),
 vencimiento datetime, 
constraint pk_pago_tarjeta primary key (id_pago_tarjeta),
constraint fk_compras_pagos foreign key (voucher) references del_naval.compras (id_voucher),
constraint fk_tarjeta_pagos foreign key (tarjeta) references del_naval.tarjetas (id_tarjeta)
)


create table del_naval.pasajes (
	id_pasaje int identity (1,1),	
	voucher int,
	viaje int,
	pasajero numeric(18,0),
	butaca int,
	cancelado bit,
	monto numeric(18,2),
	codigo_pasaje numeric(18,0) unique,
	constraint pk_pasajes primary key (id_pasaje)
)

create table del_naval.encomiendas (
	id_encomienda int identity (1,1),	
	voucher int,
	viaje int,
	remitente numeric(18,0),  
	peso numeric(18,0),
	cancelado bit,
	monto numeric(18,2),
	codigo_encomienda numeric(18,0) unique,
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
select distinct(Cli_Dni), Cli_Nombre, Cli_Apellido, Cli_Fecha_Nac, null, 0, 0, Cli_Dir, Cli_Telefono, Cli_Mail
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
select NULL, t.id_servicio, Micro_KG_Disponibles, MAX(Butaca_Nro) + 1, ma.id_marca, Micro_Modelo, Micro_Patente, 0, 0, null, null, null, null
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
select ma.Cli_Dni, 'EFECTIVO',
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
select co.id_voucher, vi.id_viaje, co.comprador, ma.Paquete_KG, 0, ma.Paquete_Precio, ma.Paquete_Codigo
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
select vi.id_viaje, bu.id_butaca
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
select EN.remitente, cast (monto / 5 as int), fecha_llegada, NULL, id_encomienda, 0
from DEL_NAVAL.encomiendas EN, DEL_NAVAL.viajes VI
where EN.viaje = VI.id_viaje 


--Se elimina la columna aux_migración que solo se utilizo para poder relacionar uno a uno las compras con las encomiendas o pasajes
alter table del_naval.compras 
drop column aux_migracion;

insert into del_naval.productos
Values 
('Termo Lu Milagros', 150, 500),
('Cafetera de bolsillo', 3, 100),
('Disfraz del hombre invisible',2,1500),
('Anteojos infrarojos',1,10),
('Agua pesada (bidon de 5L)',10,50),
('Tp de Gestion de datos 1C 2013 Completo', 0,20000),
('Recoge plátanos', 10,20)

insert into del_naval.tarjetas
Values 
('Visa', 1),
('Master Card', 1),
('Italcred',0),
('American Express',0)

 
 
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
 where nombre_funcionalidad = 'ConsultarPuntos'))
 

--Las siguientes claves foráneas se agregan ahora porque de hacerlo previo a la migración de datos cae la performance considerablmente
alter table del_naval.compras_viajes
add constraint fk_compras_viajes foreign key (voucher) references del_naval.compras (id_voucher)

alter table del_naval.compras_viajes
add constraint fk_viajes_compras foreign key (viaje) references del_naval.viajes (id_viaje)

alter table del_naval.pasajes
 add constraint fk_pasajes_compras foreign key (voucher) references del_naval.compras (id_voucher)
alter table del_naval.pasajes 
 add constraint fk_pasajes_viajes foreign key (viaje) references del_naval.viajes (id_viaje)

alter table del_naval.encomiendas
 add constraint fk_encomiendas_compras foreign key (voucher) references del_naval.compras (id_voucher)
alter table del_naval.encomiendas
 add constraint fk_encomiendas_viajes foreign key (viaje) references del_naval.viajes (id_viaje)
alter table del_naval.encomiendas
 add constraint fk_encomienda_remitente foreign key (remitente) references del_naval.clientes (id_dni)

alter table del_naval.pasajes
add constraint fk_pasajes_clientes foreign key (pasajero) references del_naval.clientes (id_dni)

alter table del_naval.pasajes
add constraint fk_pasajes_butacas foreign key (butaca) references del_naval.butacas (id_butaca)


--Se modifica el tipo de dato de "codigo_recorrido" luego de la migracion, para
--adpatarse a requerimiento del sistema de que acepte alfanumericos
--se realiza la modificacion en este momento porque detectamos problemas de performance
--en algun equipo por la cantidad de casteos que sea hacen en algunas querys con joins, como por ejemplo
--la que usamos para migrar encomiendas y pasajes

alter table del_naval.recorridos
alter column codigo_recorrido nvarchar(255) 

alter table del_naval.recorridos
add constraint unique_nombre unique (codigo_recorrido)
----------------------------------------------------------------------------------------------

use GD1C2013
go

--Se hacen drops si los objetos ya existen.

if OBJECT_ID ('del_naval.PesoLibreEncomiendasXviaje','FN') is not null
drop function del_naval.PesoLibreEncomiendasXviaje

if OBJECT_ID ('del_naval.ButacasDisponiblesXviaje','TF') is not null
drop function del_naval.ButacasDisponiblesXviaje 

if OBJECT_ID ('del_naval.devolverPasaje','P') is not null
drop procedure del_naval.devolverPasaje

if OBJECT_ID ('del_naval.devolverEncomienda','P') is not null
drop procedure del_naval.devolverEncomienda

if OBJECT_ID ('DEL_NAVAL.bloqueo_usuarios','TR') is not null
  drop trigger DEL_NAVAL.bloqueo_usuarios

if OBJECT_ID ('del_naval.cancelarViaje','P') is not null
 drop procedure del_naval.cancelarViaje
 
if OBJECT_ID ('del_naval.inhabilitar_rol','P') is not null
 drop procedure del_naval.inhabilitar_rol

if OBJECT_ID ('del_naval.cancelarRecorrido','P') is not null
 drop procedure del_naval.cancelarRecorrido

if OBJECT_ID ('del_naval.intentarBajarMicro','P') is not null
drop procedure del_naval.intentarBajarMicro 

if OBJECT_ID ('del_naval.bajaOserviceMicro','P') is not null
drop procedure del_naval.bajaOserviceMicro 

if OBJECT_ID ('del_naval.cancelarViajesDeUnMicro','P') is not null
 drop procedure del_naval.cancelarViajesDeUnMicro

if OBJECT_ID ('DEL_NAVAL.actualizar_butacas_ocupadas','TR') is not null
  drop trigger DEL_NAVAL.actualizar_butacas_ocupadas   
   
if OBJECT_ID ('del_naval.reemplazarMicro','P') is not null
 drop procedure del_naval.reemplazarMicro 
 
if OBJECT_ID ('DEL_NAVAL.destinos_comprados','TF') is not null
 drop function DEL_NAVAL.destinos_comprados 

if OBJECT_ID ('DEL_NAVAL.destinos_micros_vacios','TF') is not null
 drop function DEL_NAVAL.destinos_micros_vacios
 
if OBJECT_ID ('DEL_NAVAL.clientes_mayor_puntaje','TF') is not null
 drop function DEL_NAVAL.clientes_mayor_puntaje
 
if OBJECT_ID ('DEL_NAVAL.destinos_pasajes_cancelados','TF') is not null
 drop function DEL_NAVAL.destinos_pasajes_cancelados

if OBJECT_ID ('DEL_NAVAL.micros_fuera_servicio','TF') is not null
 drop function DEL_NAVAL.micros_fuera_servicio
 
if OBJECT_ID ('del_naval.insertarViaje','P') is not null
drop procedure del_naval.insertarViaje

if OBJECT_ID ('DEL_NAVAL.actualizar_servicios_microUPD','TR') is not null
  drop trigger DEL_NAVAL.actualizar_servicios_microUPD    
 
 if OBJECT_ID ('DEL_NAVAL.actualizar_servicios_microINS','TR') is not null
  drop trigger DEL_NAVAL.actualizar_servicios_microINS     

if OBJECT_ID ('del_naval.registrarLlegada','P') is not null
drop procedure del_naval.registrarLlegada

if OBJECT_ID ('del_naval.pasajeroPuedeViajar','P') is not null
drop procedure del_naval.pasajeroPuedeViajar

if OBJECT_ID ('del_naval.canjearPuntos','P') is not null
drop procedure del_naval.canjearPuntos

if OBJECT_ID ('del_naval.consultarPuntos','TF') is not null
drop function del_naval.consultarPuntos 

if OBJECT_ID ('del_naval.insertarCompra','P') is not null
drop procedure del_naval.insertarCompra

if OBJECT_ID ('del_naval.insertarDatosTarjeta','P') is not null
drop procedure del_naval.insertarDatosTarjeta

if OBJECT_ID ('del_naval.insertarPasaje','P') is not null
drop procedure del_naval.insertarPasaje

if OBJECT_ID ('del_naval.insertarEncomienda','P') is not null
drop procedure del_naval.insertarEncomienda

 
go

--Devuelve espacio libre en bodega para un viaje determinado
create function del_naval.PesoLibreEncomiendasXviaje 

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
 
 return (@kilosBodega - isnull(@pesoOcupado,0))       
End;
go

create function del_naval.ButacasDisponiblesXviaje 
(@viaje int)

returns @butacas_disponibles TABLE
( id_butaca int, 
  micro int,
  tipo nvarchar(255), 
  piso numeric(18,0),
  numero numeric(18,0)
  )
  
 as
Begin
 insert @butacas_disponibles
 select BU.id_butaca, BU.micro, BU.tipo, BU.piso, BU.numero
 from del_naval.butacas BU, del_naval.viajes VI
 where BU.micro = VI.micro
 and VI.cancelado = 0
 and VI.id_viaje = @viaje
 --Que no este en la tabla de ocupadas para ese viaje
 and not exists (select * 
                  from DEL_NAVAL.butacas_ocupadas
                  where viaje = VI.id_viaje
                  and butaca = BU.id_butaca )

return 
    
End;
go

--el siguiente procedimiento realiza las operaciones a nivel BD relacionadas con
--La devolución de un pasaje


create procedure del_naval.devolverPasaje
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
   
                  
                  
   delete DEL_NAVAL.butacas_ocupadas 
      where viaje = @viaje 
      and butaca = @butaca
    
   
   update DEL_NAVAL.pasajes
   set cancelado = 1  
   where id_pasaje = @pasaje
   
 
  
 set @monto = ( select monto
                  from DEL_NAVAL.pasajes 
                  where id_pasaje = @pasaje) 
                  
commit                  
return 
End;
go




--el siguiente procedimiento realiza las operaciones a nivel BD relacionadas con
--La devolución de una encomienda

create procedure del_naval.devolverEncomienda
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





create trigger del_naval.bloqueo_usuarios
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



create procedure del_naval.cancelarViaje
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
  exec del_naval.devolverPasaje @pasaje,@codigo_cancelacion,@fecha_cancelacion,@motivo,-1 
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
  exec del_naval.devolverEncomienda @encomienda,@codigo_cancelacion,@fecha_cancelacion,@motivo,-1 
  fetch cEncomiendas into @encomienda
 end
 
 close cEncomiendas
 deallocate cEncomiendas
                  
   update DEL_NAVAL.viajes
   set cancelado = 1
   where id_viaje = @viaje               
                  
commit                  
Return 0
End;
go



create procedure del_naval.inhabilitar_rol
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
return 0
end;
go




--Cancela todos los viajes que aun no han partido, para un mismo recorrido
--(y q no hayan sidos cancelados con anterioridad)
create procedure del_naval.cancelarRecorrido
(@recorrido int,
@codigo_cancelacion nvarchar(255),
@fecha_cancelacion datetime,
@motivo nvarchar(255)
)

as
begin

--utilizamos un cursor para ir cancelando uno a uno los viajes
--pertenecientes a este recorrido que estamos cancelando.
--Atencion, cancelar un recorrido de los datos migrados, con todos sus viajes
--demora muchos minutos.


begin transaction

--cancelacion viajes
declare @viaje int

declare cViajes cursor for
  select id_viaje 
   from DEL_NAVAL.viajes
   where recorrido = @recorrido 
   and fecha_salida >= @fecha_cancelacion
   and cancelado = 0
                     
 open cViajes 
 fetch cViajes into @viaje
 
 while (@@FETCH_STATUS = 0)
 begin
  exec del_naval.cancelarViaje @viaje,@codigo_cancelacion,@fecha_cancelacion,@motivo 
  fetch cViajes into @viaje
 end
 
 close cViajes
 deallocate cViajes
 
                  
   update DEL_NAVAL.recorridos 
   set cancelado = 1
   where id_recorrido = @recorrido               
                  
commit                  
Return 0
End;
go




--Este procedimiento no suele llamarse individualmente
--sino que se llama desde otros procedimientos, se usa simplemente
--para delegar funcionalidad
create procedure del_naval.bajaOserviceMicro
(@micro int,
 @desde datetime,
 @hasta datetime)
as 
begin

--si se trata de una baja
 if @desde is not null and (@hasta is null or @hasta = '31/12/9999')
  begin
  -- baja
  update DEL_NAVAL.micros
  set baja_fin_vida_util = 1,
      fecha_baja = @desde
  where id_micro = @micro    
  end
 else 
  begin
  -- servicio
    update DEL_NAVAL.micros
  set baja_servicio = 1,
      fecha_servicio_desde  = @desde,
      fecha_servicio_hasta  = @hasta
  where id_micro = @micro    
  end
   

end;
go




-- A- si no hay viajes asociados a ese micro en esa fecha
-- realiza la baja (ya sea definitiva o por service) y devuelve -1 

-- B- si hay viajes pero no encuentra micro de reemplazo, devuelve -2
-- (no hace ninguna baja ni reemplazo)

-- C- si hay viajes y encuentra micro de reemplazo, devuelve el id de micro
-- (pero no realiza el reemplazo aun, solo informa el micro que podria 
-- funcionar como reemplazante)

--Solo el caso A tiene efecto de lado.

create procedure del_naval.intentarBajarMicro
(@micro int, 
 @desde datetime,
 @hasta datetime,
 @retorno int output)

 as
 begin
 --en el caso de que venga NULL por parametro se trata de una baja


set @hasta = ISNULL(@hasta, '31/12/9999')

--si esta en la hora 0 del dia, lo lleva a la ultima hora de ese dia para que contemple el dia completo
if CONVERT (time, @hasta) = '00:00:00' 
 set @hasta = @hasta + '23:59:59'

 
if (select COUNT (*)
 from DEL_NAVAL.viajes
 where micro = @micro
 and fecha_salida >= @desde
 and fecha_estimada <= @hasta
 and cancelado = 0 ) = 0 
begin 
 -- si no hay viajes afectados da la baja 
 -- o el ingreso a servicio segun corresponda y retorna -1
  exec del_naval.bajaOserviceMicro @micro, @desde,@hasta
 
 set @retorno = -1
 return  
end 
 
 


declare
@tipo_servicio int,
@kilos_bodega numeric(18,0),
@cantidad_asientos int,
@marca int,
@fecha_servicio_desde datetime,
@fecha_servicio_hasta datetime

set @tipo_servicio = (select tipo_servicio from DEL_NAVAL.micros where id_micro = @micro)
set @kilos_bodega = (select kilos_bodega from DEL_NAVAL.micros where id_micro = @micro)
set @cantidad_asientos = (select cantidad_asientos from DEL_NAVAL.micros where id_micro = @micro)
set @marca = (select marca from DEL_NAVAL.micros where id_micro = @micro)


-- retorna el primer micro que cumple con las condiciones de reemplazo 
-- o null en caso de que no haya ninguno que cumpla
       
            
 set @retorno = (      
            
select top 1 id_micro
 from DEL_NAVAL.micros
 where tipo_servicio = @tipo_servicio
 and kilos_bodega >= @kilos_bodega
 and cantidad_asientos >= @cantidad_asientos
 and marca = @marca
 and baja_fin_vida_util = 0
 and id_micro <> @micro
 -- pido que el micro reemplazante tenga un servicio que finalice 
 -- antes de la fecha en que lo necesito o que comienze despues 
 and (isnull(fecha_servicio_hasta,'01/01/1900') < @desde  
      OR isnull(fecha_servicio_desde,'31/12/9999') >= @hasta)
 -- y que no este en la lista de micros ocupados para esa fecha
 and not exists (select micro
 from DEL_NAVAL.viajes
 where micro = id_micro
 and ((fecha_salida >= @desde and fecha_estimada <= @hasta) or
      (@desde between fecha_salida and fecha_estimada)   or
      (@hasta between fecha_salida and fecha_estimada)) 
 and cancelado = 0  )
           
 )
 
set @retorno = isnull(@retorno, -2)

return
 
 
 end;
 go
 







--cancela todos los viajes para liberar a un micro entre dos fechas determinadas
create procedure del_naval.cancelarViajesDeUnMicro
(@micro int, 
 @desde datetime,
 @hasta datetime,
 @codigo_cancelacion nvarchar(255),
 @fecha_cancelacion datetime,
 @motivo nvarchar(255)
)

as
begin



 --en el caso de que venga NULL por parametro se trata de una baja
 set @hasta = ISNULL(@hasta, '31/12/9999')
 
 --si esta en la hora 0 del dia, lo lleva a la ultima hora de ese dia para que contemple el dia completo
if CONVERT (time, @hasta) = '00:00:00' 
 set @hasta = @hasta + '23:59:59'
 
--utilizamos un cursor para ir cancelando uno a uno los viajes
--pertenecientes a este micro que estamos cancelando.
--Atencion, cancelar viajes para  un micro con un rango de fechas amplio que involucre
--muchos viaje demora muchos minutos.

begin transaction

--cancelacion viajes
declare @viaje int

declare cViajes cursor for
  select id_viaje 
   from DEL_NAVAL.viajes
   where micro=@micro 
    and fecha_salida >= @desde
    and fecha_estimada <= @hasta
    and cancelado = 0
   
                        
 open cViajes 
 fetch cViajes into @viaje
 
 while (@@FETCH_STATUS = 0)
 begin
  exec del_naval.cancelarViaje @viaje,@codigo_cancelacion,@fecha_cancelacion,@motivo 
  fetch cViajes into @viaje
 end
 
 close cViajes
 deallocate cViajes
 
                  

commit                  
Return 0
End;
go




       


create trigger del_naval.actualizar_butacas_ocupadas
on del_naval.pasajes
after insert
as 

   declare @butaca int, @viaje int
   set @butaca =(select top 1 butaca from inserted)
   set @viaje = (select top 1 viaje from inserted)

 
  if not exists (select * 
                  from del_naval.butacas_ocupadas 
                  where butaca = @butaca
                   and  viaje =  @viaje)
      insert into del_naval.butacas_ocupadas
        values(@viaje,@butaca)
        
        
 go                         
 
 
 


--reemplaza un micro "origen" por otro que "destino" en un periodo 
--determinado. Acepta null como fecha "hasta" lo cual significa que lo reemplaza
--en todos los viajes. 
--ESTE PROCEDIMIENTO NO REALIZA VALIDACION, DEBE VALIDARSE PREVIAMENTE
--EL REEMPLAZO CON EL PROCEDIMIENTO intentarBajarMicro
create procedure del_naval.reemplazarMicro
(@orig int,
@dest int,
@desde datetime,
@hasta datetime
)

as
begin

 --en el caso de que venga NULL por parametro se reemplaza de aqui en adelante
 set @hasta = ISNULL(@hasta, '31/12/9999')
 
 --si esta en la hora 0 del dia, lo lleva a la ultima hora de ese dia para que contemple el dia completo
if CONVERT (time, @hasta) = '00:00:00' 
 set @hasta = @hasta + '23:59:59'
 
-- asigna micro nuevo a viaje para poder contar ya con la funcionalidad
-- de la funcion "butacas disponibles x viaje"

begin transaction

update DEL_NAVAL.viajes 
set micro = @dest --micro destino
where id_viaje in (select id_viaje
 from DEL_NAVAL.viajes
 where micro = @orig --micro origen
 and fecha_salida >= @desde
 and fecha_estimada <= @hasta
 and cancelado = 0 )

--libera todas las butacas ocupadas en los viajes que involucran 
--al micro que acabamos de sustituir
 delete DEL_NAVAL.butacas_ocupadas 
  where viaje in (select id_viaje
 from DEL_NAVAL.viajes
 where micro = @dest --micro destino (porque lo acabo de actualizar)
 and fecha_salida >= @desde
 and fecha_estimada <= @hasta
 and cancelado = 0 )



--actualiza butacas en pasajes y les cambia el status a ocupadas
declare @pasaje int, @viaje int

declare cPasajes cursor for
  select id_pasaje, viaje
   from DEL_NAVAL.pasajes
   where viaje in (select id_viaje
 from DEL_NAVAL.viajes
 where micro = @dest --micro destino (porque lo acabo de actualizar)
 and fecha_salida >= @desde
 and fecha_estimada <= @hasta
 and cancelado = 0 ) 
                     
 open cPasajes 
 fetch cPasajes into @pasaje, @viaje
 
 while (@@FETCH_STATUS = 0)
 begin
  

--obtengo la primer butaca disponible (corresponden al nuevo micro)
--de la lista de disponibles para ese viaje
declare @butaca int
set @butaca = (select top 1 id_butaca from del_naval.ButacasDisponiblesXviaje (@viaje))

--actualizo la butaca del pasaje con la butaca disponible del nuevo micro
update DEL_NAVAL.pasajes 
set butaca = @butaca
where id_pasaje = @pasaje  

--si la butaca figura como desocupada para ese viaje, lo actualiza a ocupada
--poniendola en la tabla correspondiente
  if not exists (select * 
                  from del_naval.butacas_ocupadas 
                  where butaca = @butaca
                   and  viaje =  @viaje)    
      insert into del_naval.butacas_ocupadas
        values(@viaje,@butaca)         
 
  
  fetch cPasajes into @pasaje, @viaje
 end
 
commit


 close cPasajes
 deallocate cPasajes

return 0 
 end
 go



 
create function DEL_NAVAL.destinos_comprados
(@desde datetime,
 @hasta datetime)
returns @destinos_cantidad table (
destino nvarchar(255),
cantidad int
)
as
begin

--si esta en la hora 0 del dia, lo lleva a la ultima hora de ese dia para que contemple el dia completo
if CONVERT (time, @hasta) = '00:00:00' 
 set @hasta = @hasta + '23:59:59'
 
 
insert @destinos_cantidad
select CI.nombre_ciudad, COUNT (*) as cantidad_vendida
 from DEL_NAVAL.pasajes PA, DEL_NAVAL.viajes VI, DEL_NAVAL.recorridos RE, 
      DEL_NAVAL.ciudades CI, DEl_NAVAL.compras CO
 where PA.viaje = VI.id_viaje
 and   VI.recorrido = RE.id_recorrido
 and   RE.destino = CI.id_ciudad
 and   CO.id_voucher = PA.voucher 
 and   CO.fecha_compra between @desde and @hasta
 group by CI.nombre_ciudad
 order by cantidad_vendida desc
 
 return
end

go



create function DEL_NAVAL.destinos_micros_vacios
(@desde datetime,
 @hasta datetime)
returns @destinos_micros_vacios table (
destino nvarchar(255),
butacas_libres int
)
as
begin

--si esta en la hora 0 del dia, lo lleva a la ultima hora de ese dia para que contemple el dia completo
if CONVERT (time, @hasta) = '00:00:00' 
 set @hasta = @hasta + '23:59:59'

insert @destinos_micros_vacios
select CI.nombre_ciudad as ciudad, count(BU.id_butaca) as butacas_libres
 from  DEL_NAVAL.viajes VI, DEL_NAVAL.recorridos RE, 
      DEL_NAVAL.ciudades CI, del_naval.butacas BU
 where VI.recorrido = RE.id_recorrido
 and   BU.micro = VI.micro
 and   VI.cancelado = 0
 and   RE.destino = CI.id_ciudad
 and   VI.fecha_salida >= @desde 
 and   VI.fecha_llegada < @hasta
 and not exists (select * 
                  from DEL_NAVAL.butacas_ocupadas
                  where viaje = VI.id_viaje
                  and butaca = BU.id_butaca ) 
 group by CI.nombre_ciudad
 order by butacas_libres desc
 
 

 
 return
end
go



create function DEL_NAVAL.clientes_mayor_puntaje
(@desde datetime,
 @hasta datetime)
returns @clientes_mayor_puntaje table (
cliente int,
puntaje int
)
as
begin

--si esta en la hora 0 del dia, lo lleva a la ultima hora de ese dia para que contemple el dia completo
if CONVERT (time, @hasta) = '00:00:00' 
 set @hasta = @hasta + '23:59:59'
 
 
insert @clientes_mayor_puntaje
select cliente, sum(puntos) as puntaje
 from  DEL_NAVAL.puntos
 where fecha between @desde and @hasta
 group by cliente
 order by puntaje desc
 
 

 
 return
end
go



create function DEL_NAVAL.destinos_pasajes_cancelados
(@desde datetime,
 @hasta datetime)
returns @destinos_pasajes_cancelados table (
destino nvarchar(255),
pasajes_cancelados int
)
as
begin

--si esta en la hora 0 del dia, lo lleva a la ultima hora de ese dia para que contemple el dia completo
if CONVERT (time, @hasta) = '00:00:00' 
 set @hasta = @hasta + '23:59:59'


insert @destinos_pasajes_cancelados
select CI.nombre_ciudad as ciudad, count(CA.pasaje) as pasajes_cancelados
 from  DEL_NAVAL.viajes VI, DEL_NAVAL.recorridos RE, del_naval.pasajes PA,
      DEL_NAVAL.ciudades CI, del_naval.cancelaciones CA
 where VI.recorrido = RE.id_recorrido
 and   RE.destino = CI.id_ciudad
 and   CA.fecha_devolucion between @desde and @hasta
 and   PA.id_pasaje = CA.pasaje
 and   PA.viaje = VI.id_viaje
 group by CI.nombre_ciudad
 order by pasajes_cancelados desc
 
 

 
 return
end
go




create function DEL_NAVAL.micros_fuera_servicio
(@desde datetime,
 @hasta datetime)
returns @micros_fuera_servicio table (
micro int,
patente nvarchar(255),
dias_fuera_servicio int
)
as
begin

--si esta en la hora 0 del dia, lo lleva a la ultima hora de ese dia para que contemple el dia completo
if CONVERT (time, @hasta) = '00:00:00' 
 set @hasta = @hasta + '23:59:59'

-- Usa union para considerar las diferentes condiciones de frontera que pueden darse
-- entre los rangos ingresados por parametros y los de fecha de servicio en la tabla micro
-- seleccionando el minimo de las cotas superior y el maximo de las cotas inferiores:




insert  @micros_fuera_servicio


--[ ] representa rango de busqueda @desde a @hasta
-- () representa rango de servicio efectivo fecha_servicio_desde a fecha_servicio_desde

-- [ () ]

--como la fecha "hasta" se registro hasta el minuto 59, para que haga bien la cuenta de dias 
--le sumamos un minuto para realizar los datediff


select  micro, patente, sum(dias_fuera_servicio) as dias
from
(
select  SE.id_servicio as serv,  MI.id_micro as micro, MI.patente as patente, DATEDIFF (DAY,SE.fecha_desde, dateadd(minute, 1, SE.fecha_hasta)) as dias_fuera_servicio
 from  del_naval.micros MI, del_naval.servicios_micro SE
 where SE.fecha_desde >= @desde
 and SE.fecha_hasta<= @hasta
 and MI.id_micro = SE.micro
 group by  SE.id_servicio, MI.id_micro, MI.patente, SE.fecha_desde, SE.fecha_hasta
union 

--( [ ) ]   -- caso sin interseccion () []


 select SE.id_servicio as serv, MI.id_micro,  MI.patente, DATEDIFF (DAY,@desde, dateadd(minute, 1, SE.fecha_hasta)) as dias_fuera_servicio
 from  del_naval.micros MI, del_naval.servicios_micro SE
 where SE.fecha_desde < @desde
 and (SE.fecha_hasta <= @hasta and SE.fecha_hasta > @desde)--esto ultimo para que haya interseccion
 and MI.id_micro = SE.micro
 group by  SE.id_servicio, MI.id_micro, MI.patente, SE.fecha_hasta 
union 
-- [ ( ] )   -- caso sin interseccion [] () 

 select  SE.id_servicio as serv,MI.id_micro, MI.patente, DATEDIFF (DAY,SE.fecha_desde, dateadd(minute, 1, @hasta)) as dias_fuera_servicio
 from  del_naval.micros MI, del_naval.servicios_micro SE 
 where (SE.fecha_desde >= @desde and SE.fecha_desde <  @hasta) --esto ultimo para que haya interseccion
 and SE.fecha_hasta >= @hasta
 and MI.id_micro = SE.micro
 group by  SE.id_servicio, MI.id_micro, MI.patente, SE.fecha_desde
union 
-- ( [ ] ) 



 select  SE.id_servicio as serv, MI.id_micro, MI.patente, DATEDIFF(DAY,@desde, dateadd(minute, 1, @hasta)) as dias_fuera_servicio
 from  del_naval.micros MI, del_naval.servicios_micro SE
 where SE.fecha_desde < @desde
 and SE.fecha_hasta > @hasta
 and MI.id_micro = SE.micro
 group by  SE.id_servicio, id_micro, patente
 ) subselect
 group by  micro, patente
 order by dias desc

  
 return 
end
go           
            


create procedure del_naval.insertarViaje
(@recorrido int,
@micro int,
@salida datetime, 
@llegada_estimada datetime, 
@retorno int output
)
as
begin
--verifica disponibilidad del micro
if exists (select top 1 id_micro
 from DEL_NAVAL.micros
 where  id_micro = @micro
 -- pido que el micro a utilizar tenga un servicio que finalice 
 -- antes de la fecha en que lo necesito o que comienze despues 
 -- o q no tenga servicio
 and (isnull(fecha_servicio_hasta,'01/01/1900') < @salida  
      OR isnull(fecha_servicio_desde,'31/12/9999') > @llegada_estimada)
 -- y que no este en la lista de micros ocupados para esa fecha
 and not exists (select micro
 from DEL_NAVAL.viajes
 where micro = id_micro
 and ((fecha_salida >= @salida and fecha_estimada <= @llegada_estimada) or
      (@salida between fecha_salida and fecha_estimada)   or
      (@llegada_estimada between fecha_salida and fecha_estimada)) 
 and cancelado = 0 ))
 
 begin --begin del if
  --verifica que el micro y el recorrido tengan mismo tipo de servicio
  if exists (select top 1 * 
  from DEL_NAVAL.recorridos RE, DEL_NAVAL.micros MI
  where RE.id_recorrido = @recorrido 
  and MI.id_micro = @micro 
  and RE.tipo_servicio = MI.tipo_servicio)
    begin --beginif
      insert DEL_NAVAL.viajes
      values (@recorrido, @micro, @salida, NULL , @llegada_estimada, 0)
      set @retorno = 0
      return 
    end --endif
  else
   begin
   set @retorno = -2
   return  --micro no cubre tipo de servicio de ese recorrido
   end
 end --endif
 else
 begin
  set @retorno = -1
  return --micro no disponible       
 end     
      



end;
go



--El siguiente trigger se encarga de registrar las bajas por servicio de un micro
--en la tabla de historico de bajas por servicio
  
create trigger del_naval.actualizar_servicios_microUPD 
on del_naval.micros
after update
as 

   declare @micro int, @desdenuevo datetime, @hastanuevo datetime,
                       @desdeviejo datetime, @hastaviejo datetime
   
   set @micro = (select top 1 id_micro from inserted)
   set @desdenuevo = (select top 1 fecha_servicio_desde from inserted)
   set @hastanuevo = (select top 1 fecha_servicio_hasta from inserted)
   
   set @desdeviejo = (select top 1 fecha_servicio_desde from deleted)
   set @hastaviejo = (select top 1 fecha_servicio_hasta from deleted)
   
   --si el rango de fechas de servicio no es null...
  if (@desdenuevo is not null and @hastanuevo is not null)
  
  --se fija que si el rango viejo no era null y hay variacion, hace el insert en el registro 
  -- historico. O si estaba en null el registro anterior, tmb hace el insert  
   if  ((@desdeviejo is not null and @hastaviejo is not null)
      and (@desdenuevo <> @desdeviejo) and (@hastanuevo <> @hastaviejo))
       OR 
      (@desdeviejo is null and @hastaviejo is null)
   
       insert into del_naval.servicios_micro
        values(@micro,@desdenuevo,@hastanuevo)   
                
 go           
 
 

 
--El siguiente trigger se encarga de registrar las bajas por servicio de un micro
--en la tabla de historico de bajas por servicio
  
create trigger del_naval.actualizar_servicios_microINS 
on del_naval.micros
after insert
as 

   declare @micro int, @desdenuevo datetime, @hastanuevo datetime
          
   
   set @micro = (select top 1 id_micro from inserted)
   set @desdenuevo = (select top 1 fecha_servicio_desde from inserted)
   set @hastanuevo = (select top 1 fecha_servicio_hasta from inserted)

   --si el rango de fechas de servicio no es null...
  if (@desdenuevo is not null and @hastanuevo is not null) 

       insert into del_naval.servicios_micro
        values(@micro,@desdenuevo,@hastanuevo)   
                
 go                   
 
   
   


create procedure del_naval.registrarLlegada
(@micro int,
@origen int,
@destino int, 
@fecha_llegada datetime,
@retorno int output
)
as
begin
 declare @viaje int
 set @viaje = 
  (select top 1 VI.id_viaje
  from DEL_NAVAL.viajes VI, DEL_NAVAL.micros MI, DEL_NAVAL.recorridos RE
  where VI.recorrido = RE.id_recorrido
    and RE.tipo_servicio = MI.tipo_servicio
    and RE.origen = @origen
    and RE.destino = @destino
    and RE.cancelado = 0
    and VI.cancelado = 0
    and VI.fecha_llegada is null
    --valida que la fecha de llegada sea a lo sumo un dia mas o un dia menos que la estimada
    and convert(date,VI.fecha_estimada) between DATEADD(day,-1,@fecha_llegada)  and DATEADD(day,1,@fecha_llegada) 
    and MI.id_micro = VI.micro )
 
 if @viaje is not null --si los datos ingresados corresponden con la llegada de un micro...
  begin
   begin transaction
  

  
   --actualiza la fecha de llegada en el viaje
    update DEL_NAVAL.viajes
    set fecha_llegada = @fecha_llegada
    where id_viaje = @viaje
     --registrar puntos
   
     insert into del_naval.puntos
      select pasajero, cast (monto / 5 as int),  @fecha_llegada, id_pasaje, NULL, 0
       from DEL_NAVAL.pasajes 
       where viaje = @viaje
      union
      select remitente, cast (monto / 5 as int), @fecha_llegada, NULL, id_encomienda, 0
       from DEL_NAVAL.encomiendas
       where viaje = @viaje

    
   commit
   set @retorno = 0
   return  --todo OK  
  end 
 else
  begin
   set @retorno = -1
   return  --Verificar datos ingresados, inconsitencia en llegada   
  end
    
end;
go





create procedure del_naval.pasajeroPuedeViajar
(@pasajero int, 
 @viaje int,
 @retorno int output
 )
as
begin
  
  declare @desde datetime, @hasta datetime
  set @desde = (select fecha_salida from DEL_NAVAL.viajes where id_viaje = @viaje)
  set @hasta = (select fecha_estimada from DEL_NAVAL.viajes where id_viaje = @viaje)
  
  
--que no exista en la lista de pasajeros ocupados para la fecha del viaje que esta intentando hacer.
if not exists (select pasajero
 from DEL_NAVAL.viajes VI, DEL_NAVAL.pasajes PA
 where PA.pasajero = @pasajero
 and PA.viaje = Vi.id_viaje
 and ((fecha_salida >= @desde and fecha_estimada <= @hasta) or
      (@desde between fecha_salida and fecha_estimada)   or
      (@hasta between fecha_salida and fecha_estimada)) 
 and PA.cancelado = 0  
 and VI.cancelado = 0)
  begin
   set @retorno = 0 --todo ok, el pasajero esta libre y puede viajar
   return   
  end        
 Else
  begin
      set @retorno = -1 --pasajero ocupado
   return   
  end  
End;
go






-- -2 puntos insuficientes
-- -1 no hay stock
-- -3 error inesperado

create procedure del_naval.canjearPuntos
(@cliente int, 
 @producto int,
 @cantidad int,
 @fecha datetime,
 @retorno int output
 )
as
begin

--valida stock
if (select stock from DEL_NAVAL.productos where id_producto = @producto ) < @cantidad
 begin
  set @retorno = -1
  return   --no hay stock suficiente para la cantidad requerida
 end

--detecta cuantos puntos necesita  canjear
declare @puntos_a_canjear int
set @puntos_a_canjear = (select valor_puntos 
                           from DEL_NAVAL.productos 
                           where id_producto = @producto) * @cantidad   

--valida que el cliente tenga esos puntos 
if ((select SUM (puntos) - SUM(puntos_usados) 
    from DEL_NAVAL.consultarPuntos (@cliente,@fecha)    
    where id_canje is null) < @puntos_a_canjear)
 
  begin
  set @retorno = -2
  return  --puntos insuficientes   
  end
--si esta todo ok, consume los puntos y realiza la registracion del canje


 
 --actualizacion de puntos usados con cursores

declare @puntos_a_canjear_aux int --se va decrementando a medida que se van compensando 
                                   --con los puntos usados que se incrementan

declare @puntos_disponibles int --representa puntos sin asignar, o sin ser usados, para un mismo id_punto

--variables para el cursor
declare @id_punto int, @puntos int, @puntos_usados int


declare cPuntos cursor for
  select id_punto, puntos, puntos_usados
 from DEL_NAVAL.consultarPuntos (@cliente, @fecha) 
 where id_canje is null
 order by fecha_movimiento asc

begin transaction
                     
 open cPuntos
 fetch cPuntos into @id_punto, @puntos, @puntos_usados
 
 set @puntos_a_canjear_aux = @puntos_a_canjear 
 
 while (@@FETCH_STATUS = 0 or @puntos_a_canjear_aux > 0 )
 begin
 
 set @puntos_disponibles = @puntos - @puntos_usados
 if (@puntos_disponibles > 0 ) -- si hay puntos disponibles, los usa. sino pasa al siguiente registro
  begin
   if @puntos_disponibles <= @puntos_a_canjear_aux  --hay q usar todos los disponibles
    begin
      set @puntos_a_canjear_aux = @puntos_a_canjear_aux - @puntos_disponibles  
      --actualiza el estado del punto en la tabla
      update DEL_NAVAL.puntos
      set usados = @puntos_usados + @puntos_disponibles   --todos pasan a estar usados
      where id_punto = @id_punto       
    end
   else -- si hay mas disponible que los que necesito solo usar una parte de los disponibles
    begin
    
      --actualiza el estado del punto en la tabla
      update DEL_NAVAL.puntos
      set usados = @puntos_usados + @puntos_a_canjear_aux  --consumo todos los q necesito 
      where id_punto = @id_punto      
      
      set @puntos_a_canjear_aux = 0  --con esto sale del while
    end
  end
   
   fetch cPuntos into @id_punto, @puntos, @puntos_usados 
 end --fin del while
  
  if (@puntos_a_canjear_aux <> 0 )
    begin
     set @retorno = -3 --error inesperado 
    end 
    
   --si se pudieron consumir los puntos correctamente, realiza el registro del canje
   insert DEL_NAVAL.canjes
   values (@cliente, @producto, @cantidad, @fecha, @puntos_a_canjear)  
   --actualiza stock
   update DEL_NAVAL.productos 
   set stock = stock - @cantidad 
   where id_producto = @producto 
 
 commit
 close cPuntos
 deallocate cPuntos
  

set @retorno = 0 --todo ok              
return 
End;
go



create function del_naval.consultarPuntos
(@cliente int, 
 @fecha datetime --fecha a partir de la cual se calcula vencimiento 
)

returns @consultarPuntos TABLE
(
id_punto int, 
id_canje int,
cliente int,
puntos int, 
fecha_movimiento datetime, 
descripcion nvarchar(255),
puntos_usados int 

 )



  
 as
Begin


--si esta en la hora 0 del dia, lo lleva a la ultima hora de ese dia para que contemple el dia completo
if CONVERT (time, @fecha) = '00:00:00' 
 set @fecha = @fecha + '23:59:59'

 insert @consultarPuntos
 select PU.id_punto, NULL, PU.cliente, PU.puntos, PU.fecha, 'Puntos ya consumidos: ' + 
CONVERT(nvarchar,PU.usados) + ' // Antiguedad: ' + ' ' + CONVERT(nvarchar,DATEDIFF (DAY,PU.fecha,@fecha)) +
 ' dias.' as Descripcion  , PU.usados
 
  from DEL_NAVAL.puntos PU
  where PU.cliente = @cliente
  and DATEDIFF (DAY,PU.fecha,@fecha)<= 365  --los vencidos no los muestra
  and PU.fecha <= @fecha -- solo trae puntos vigentes hasta la fecha ingresada por parametro
  
   union 
   
  select NULL, CA.id_canje, CA.cliente, (CA.puntos_canjeados * -1) , CA.fecha_canje,  'Canje: ' + 
  CONVERT(nvarchar,cantidad) + ' x ' + 
  (select descripcion from DEL_NAVAL.productos where id_producto = producto) , NULL
  
  from DEL_NAVAL.canjes CA
 where  CA.cliente = @cliente
 and CA.fecha_canje <= @fecha 
   order by PU.fecha  asc 

 
return 
    
End;
go


 

create procedure del_naval.insertarCompra
(@comprador int,
@forma_pago nvarchar(255),
@fecha datetime,
@retorno int output
)
as
begin
 

declare  @ret table (id_voucher int)

     insert into del_naval.compras
     output inserted.id_voucher into @ret
     values (@comprador, @forma_pago, @fecha)
     
set @retorno = (select id_voucher from @ret)
    
  
  
   return  --devuelve el id de voucher creado

    
end;
go



create procedure del_naval.insertarDatosTarjeta
(@voucher int,
@tarjeta int, 
@numero nvarchar(255),
@cod_ser nvarchar(255),
@fecha datetime
)
as
begin
    
     insert into del_naval.pagos_tarjetas
     values (@voucher, @tarjeta, @numero, @cod_ser, @fecha)
  
   return 0 
    
end;
go



create procedure del_naval.insertarPasaje
(@voucher int,
@viaje int,
@pasajero int,
@butaca int,
@codigo_pasaje int output,
@monto int output
)
as
begin
 
declare @precio_base_pasaje numeric(18,2),
        @porcentaje numeric(18,2),
        @recorrido int,
        @tipo_servicio int
        

set @recorrido = (select recorrido from DEL_NAVAL.viajes where id_viaje = @viaje )        
set @precio_base_pasaje = (select precio_base_pasaje from DEL_NAVAL.recorridos where id_recorrido = @recorrido)      
set @tipo_servicio = (select tipo_servicio from DEL_NAVAL.recorridos where id_recorrido = @recorrido)       
set @porcentaje = (select porcentaje from DEL_NAVAL.tipos_servicio where id_servicio = @tipo_servicio)

--calcula el monto

if (@monto <> 0) or @monto is null --si el monto no viene en cero, lo calcula, sino lo deja como esta
-- viene en cero cuando estamos en presencia de un acompañante de discapacitado o de un discapacitado
     
      if (select jubilado_pensionado from DEL_NAVAL.clientes where id_dni = @pasajero) = 1
      --si es jubilado o pensionado calcula con 50% de descuento     
        set @monto = convert (int, (@precio_base_pasaje * ((@porcentaje / 100) + 1)) * 50   )
      else
        set @monto = convert (int, (@precio_base_pasaje * ((@porcentaje / 100) + 1)) * 100   )


--obtiene el codigo de pasaje siguiente
set  @codigo_pasaje = (select MAX(codigo_pasaje)+1 from del_naval.pasajes)



declare @montonumeric numeric(18,2)
set @montonumeric = (convert(numeric(18,2),@monto) /100)


insert DEL_NAVAL.pasajes 
values (@voucher, @viaje, @pasajero, @butaca, 0, @montonumeric ,@codigo_pasaje)  
 
  
  
   return  --devuelve los valores de monto y codigo de pasaje

    
end;
go



create procedure del_naval.insertarEncomienda
(@voucher int,
@viaje int,
@remitente int,
@peso int,
@codigo_encomienda int output,
@monto int output
)
as
begin
 
declare @precio_kg_encomienda numeric(18,2),
        @recorrido int
      
        

set @recorrido = (select recorrido from DEL_NAVAL.viajes where id_viaje = @viaje )        
set @precio_kg_encomienda = (select precio_kg_encomienda  from DEL_NAVAL.recorridos where id_recorrido = @recorrido)      


--calcula el monto
 set @monto = convert (int, (@precio_kg_encomienda  * @peso * 100) )


--obtiene el codigo de pasaje siguiente
set @codigo_encomienda = (select MAX(codigo_encomienda)+1 from del_naval.encomiendas)



declare @montonumeric numeric(18,2)
set @montonumeric = (convert(numeric(18,2),@monto) /100)


insert DEL_NAVAL.encomiendas  
values (@voucher, @viaje, @remitente, @peso, 0, @montonumeric ,@codigo_encomienda)  
 
  
  
   return  --devuelve los valores de monto y codigo de pasaje

    
end;
go
               