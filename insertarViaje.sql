if OBJECT_ID ('del_naval.insertarViaje','P') is not null
drop procedure del_naval.insertarViaje
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
 and fecha_salida >= @salida
 and fecha_estimada <= @llegada_estimada
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