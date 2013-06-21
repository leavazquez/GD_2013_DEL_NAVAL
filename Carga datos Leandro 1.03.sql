
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
select distinct recorrido_codigo, ORI, DES, id_servicio, sum(recorrido_precio_basepasaje), sum(Recorrido_Precio_BaseKG)
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
select distinct id_recorrido, id_micro, fechasalida, fechallegada, Fecha_LLegada_Estimada 
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

insert into DEL_NAVAL.butacas_disponibles
select vi.id_viaje, bu.id_butaca, 1
from DEL_NAVAL.viajes vi, DEL_NAVAL.pasajes pa, DEL_NAVAL.micros mi, DEL_NAVAL.butacas bu
where vi.micro = mi.id_micro
and pa.butaca = bu.id_butaca
and pa.viaje = vi.id_viaje
and bu.micro = mi.id_micro


alter table del_naval.compras 
drop column aux_migracion;

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