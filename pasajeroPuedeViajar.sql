
if OBJECT_ID ('del_naval.pasajeroPuedeViajar','P') is not null
drop procedure del_naval.pasajeroPuedeViajar

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

