if OBJECT_ID ('cancelarRecorrido','P') is not null
 drop procedure cancelarRecorrido
go

create procedure cancelarRecorrido
(@recorrido int,
@codigo_cancelacion nvarchar(255),
@fecha_cancelacion datetime,
@motivo nvarchar(255)
)

as
begin

--utilizamos un cursor para ir cancelando uno a uno los viajes
--pertenecientes a este recorrido que estamos cancelando.

begin transaction

--cancelacion viajes
declare @viaje int

declare cViajes cursor for
  select id_viaje 
   from DEL_NAVAL.viajes
   where recorrido=@recorrido
                     
 open cViajes 
 fetch cViajes into @viaje
 
 while (@@FETCH_STATUS = 0)
 begin
  exec cancelarViaje @viaje,@codigo_cancelacion,@fecha_cancelacion,@motivo 
  fetch cViajes into @viaje
 end
 
 close cViajes
 deallocate cViajes
 
                  
   update DEL_NAVAL.recorridos 
   set cancelado = 1
   where id_recorrido = @recorrido               
                  
commit                  
Return 
End;
go


