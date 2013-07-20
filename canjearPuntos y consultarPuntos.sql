

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
if (select stock from DEL_NAVAL.productos where id_producto = @producto ) < @cantidad
 return -1 --no hay stock suficiente para la cantidad requerida

--detecta cuantos puntos necesita  canjear
declare @puntos_a_canjear
set @puntos_a_canjear = (select valor_puntos 
                           from DEL_NAVAL.productos 
                           where id_producto = @producto) * @cantidad   

--valida que el cliente tenga esos puntos 
if (select SUM (puntos) - SUM(usados) 
    from DEL_NAVAL.consultarPuntos (@cliente,@fecha) < @puntos_a_canjear
  return -2 --puntos insuficientes   
  
--si esta todo ok, consume los puntos y realiza la registracion del canje

begin transaction
 
  


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
id_punto int, 
id_canje int,
cliente int,
puntos_obtenidos int, 
puntos_canjeados int,
fecha datetime, 
usados int, 
vejez int,
producto int, 
cantidad int

 )



  
 as
Begin
 insert @consultarPuntos
 select PU.id_punto, NULL, PU.cliente, PU.puntos, NULL, PU.fecha, PU.usados, DATEDIFF (DAY,PU.fecha,@fecha) as vejez, NULL,  NULL
  from DEL_NAVAL.puntos PU
  where PU.cliente = @cliente
  and DATEDIFF (DAY,PU.fecha,@fecha)<= 365  --los vencidos no los muestra
  and PU.fecha < @fecha -- solo trae puntos vigentes hasta la fecha ingresada por parametro
  
   union 
  select NULL, CA.id_canje, CA.cliente, NULL, CA.puntos_canjeados, CA.fecha_canje, NULL,NULL, CA.producto, CA.cantidad
  from DEL_NAVAL.canjes CA
 where  CA.cliente = @cliente
 and CA.fecha_canje < @fecha 
   order by vejez desc 

 
return 
    
End;
go
