if OBJECT_ID ('infoCambioMicro','P') is not null
drop procedure infoCambioMicro 
go

-- si no hay viajes asociados a ese micro en esa fecha devuelve -1
-- si hay viajes pero no encuentra micro de reemplazo, devuelve -2
-- si hay viajes y encuentra micro, devuelve el id de micro
create procedure infoCambioMicro
(@micro int, 
 @desde datetime,
 @hasta datetime,
 @retorno int output)

 as
 begin
 --en el caso de que venga NULL por parametro se trata de una baja
 set @hasta = ISNULL(@hasta, '31/12/9999')
 
if (select COUNT (*)
 from DEL_NAVAL.viajes
 where micro = @micro
 and fecha_salida >= @desde
 and fecha_estimada <= @hasta
 and cancelado = 0 ) = 0 
begin 
 -- si no hay viajes afectados da la baja 
 -- o el ingreso a servicio segun corresponda y retorna -1
  exec bajaOserviceMicro @micro, @desde,@hasta
 
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
set @fecha_servicio_desde = (select isnull(fecha_servicio_desde,'31/12/9999') from DEL_NAVAL.micros where id_micro = @micro)
set @fecha_servicio_hasta = (select isnull(fecha_servicio_hasta,'01/01/1900') from DEL_NAVAL.micros where id_micro = @micro)

-- retorna el primer micro que cumple con las condiciones de reemplazo 
-- o null en caso de que no haya ninguno que cumpla
       
            
 set @retorno = (select top 1 id_micro
 from DEL_NAVAL.micros
 where tipo_servicio = @tipo_servicio
 and kilos_bodega >= @kilos_bodega
 and cantidad_asientos >= @cantidad_asientos
 and marca = @marca
 and baja_fin_vida_util = 0
 and id_micro <> @micro
 -- pido que el micro reemplazante tenga un servicio que finalice 
 -- antes de la fecha en que lo necesito o que comienze despues 
 and (isnull(fecha_servicio_hasta,'01/01/1900') < @fecha_servicio_desde  
      OR isnull(fecha_servicio_desde,'31/12/9999') > @fecha_servicio_hasta))
 
set @retorno = isnull(@retorno, -2)

return
 
 
 end;
 go
 

