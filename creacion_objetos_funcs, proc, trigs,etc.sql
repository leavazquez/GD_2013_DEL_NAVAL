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
 if (select intentos from inserted) >= 3
 begin
  update del_naval.usuarios
   set activo = 0
   where id_usuario = (select id_usuario from inserted)
 end  
 
 
 
 