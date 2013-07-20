if OBJECT_ID ('del_naval.registrarLlegada','P') is not null
drop procedure del_naval.registrarLlegada
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


