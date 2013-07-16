use GD1C2013
go

if OBJECT_ID ('DEL_NAVAL.destinos_comprados','TF') is not null
 drop function DEL_NAVAL.destinos_comprados 

if OBJECT_ID ('DEL_NAVAL.destinos_micros_vacios','TF') is not null
 drop function DEL_NAVAL.destinos_micros_vacios
 
if OBJECT_ID ('DEL_NAVAL.destinos_pasajes_cancelados','TF') is not null
 drop function DEL_NAVAL.destinos_pasajes_cancelados

if OBJECT_ID ('DEL_NAVAL.micros_fuera_servicio','TF') is not null
 drop function DEL_NAVAL.micros_fuera_servicio
 
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




create function DEL_NAVAL.destinos_pasajes_cancelados
(@desde datetime,
 @hasta datetime)
returns @destinos_pasajes_cancelados table (
destino nvarchar(255),
pasajes_cancelados int
)
as
begin


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

-- Usa union para considerar las diferentes condiciones de frontera que pueden darse
-- entre los rangos ingresados por parametros y los de fecha de servicio en la tabla micro
-- seleccionando el minimo de las cotas superior y el maximo de las cotas inferiores:
/*
min fecha_servicio_hasta   @hasta

max fecha_servicio_desde    @desde*/

insert  @micros_fuera_servicio


select  id_micro, patente, sum(day(fecha_servicio_hasta - fecha_servicio_desde)) as dias_fuera_servicio
 from  del_naval.micros 
 where fecha_servicio_desde >= @desde
 and fecha_servicio_hasta <= @hasta
 group by id_micro, patente
union
 select id_micro, patente, sum(day(fecha_servicio_hasta - @desde)) as dias_fuera_servicio
 from  del_naval.micros 
 where fecha_servicio_desde < @desde
 and fecha_servicio_hasta <= @hasta
 group by id_micro, patente
union
 select id_micro, patente, sum(day(@hasta - fecha_servicio_desde)) as dias_fuera_servicio
 from  del_naval.micros 
 where fecha_servicio_desde > @desde
 and fecha_servicio_hasta > @hasta
 group by id_micro, patente
union
 select id_micro, patente, sum(day(@hasta - @desde)) as dias_fuera_servicio
 from  del_naval.micros 
 where fecha_servicio_desde < @desde
 and fecha_servicio_hasta > @hasta
 group by id_micro, patente
 order by dias_fuera_servicio desc

  
 return 
end
go