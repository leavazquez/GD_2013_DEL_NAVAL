if OBJECT_ID ('bajaOserviceMicro','P') is not null
drop procedure bajaOserviceMicro 
go

create procedure bajaOserviceMicro
(@micro int,
 @desde datetime,
 @hasta datetime)
as
begin
 if @desde is not null and @hasta is null
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