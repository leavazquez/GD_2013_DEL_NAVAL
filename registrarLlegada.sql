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
    --en cuanto a fechas solo valida que la fecha estimada y la de llegada esten en el mismo dia.
    and convert(date,VI.fecha_estimada) = convert(date,@fecha_llegada)
    and MI.id_micro = VI.micro )
 
 if @viaje is not null --si los datos ingresados corresponden con la llegada de un micro...
  begin
   begin transaction
   --registrar puntos
  
   --actualiza la fecha de llegada en el viaje
    update DEL_NAVAL.viajes
    set fecha_llegada = @fecha_llegada
    where id_viaje = @viaje
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


