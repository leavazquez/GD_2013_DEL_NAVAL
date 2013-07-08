
use GD1C2013
go
--el siguiente procedimiento realiza las operaciones a nivel BD relacionadas con
--La cancelacion de un viaje, realizando ademas las cancelaciones correspondientes 
--de todos sus pasajes y encomiendas asociados, 
--utilizando para esto los procedimientos ya creados

if OBJECT_ID ('cancelarViaje','P') is not null
drop procedure cancelarViaje
go

create procedure cancelarViaje
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
  exec devolverPasaje @pasaje,@codigo_cancelacion,@fecha_cancelacion,@motivo,-1 
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
  exec devolverEncomienda @encomienda,@codigo_cancelacion,@fecha_cancelacion,@motivo,-1 
  fetch cEncomiendas into @encomienda
 end
 
 close cEncomiendas
 deallocate cEncomiendas
                  
   update DEL_NAVAL.viajes
   set cancelado = 1
   where id_viaje = @viaje               
                  
commit                  
Return 
End;
go