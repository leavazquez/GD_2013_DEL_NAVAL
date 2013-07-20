

if OBJECT_ID ('del_naval.canjearPuntos','P') is not null
drop procedure del_naval.canjearPuntos

if OBJECT_ID ('del_naval.consultarPuntos','TF') is not null
drop function del_naval.consultarPuntos 

go



create procedure del_naval.canjearPuntos
(@cliente int, 
 @producto int,
 @cantidad int,
 @fecha datetime
 )
as
begin

--valida stock
if (select stock from DEL_NAVAL.productos where id_producto = @producto ) <= 0
 return -1 --no hay stock

--validar puntos 
 



commit                  
return 
End;
go



create function del_naval.consultarPuntos
(@cliente int, 
 @fecha datetime --fecha a partir de la cual se calcula vencimiento 
)

returns @consultarPuntos TABLE
(
cliente int,
puntos int, 
fecha datetime, 
usados int, 
vejez int,
producto int, 
cantidad int,
fecha_canje datetime, 
puntos_canjeados int
 )



  
 as
Begin
 insert @consultarPuntos
 select PU.cliente, PU.puntos, PU.fecha, PU.usados, DATEDIFF (DAY,PU.fecha,@fecha), NULL,  NULL,  NULL,  NULL
  from DEL_NAVAL.puntos PU
  where PU.cliente = @cliente
  and PU.fecha < @fecha -- solo trae puntos vigentes hasta la fecha ingresada por parametro
  order by vejez desc
 union 
  select CA.cliente, NULL, NULL, NULL, NULL, CA.producto, CA.cantidad, CA.fecha_canje, CA.puntos_canjeados
  from DEL_NAVAL.canjes CA
 where  CA.cliente = @cliente
 and CA.fecha_canje < @fecha 
 
 
return 
    
End;
go
