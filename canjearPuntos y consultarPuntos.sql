use GD1C2013
go

if OBJECT_ID ('del_naval.canjearPuntos','P') is not null
drop procedure del_naval.canjearPuntos

if OBJECT_ID ('del_naval.consultarPuntos','TF') is not null
drop function del_naval.consultarPuntos 

go



create procedure del_naval.canjearPuntos
(@cliente int, 
 @producto int,
 @cantidad int,
 @fecha datetime,
 @retorno int output
 )
as
begin

--valida stock
if (select stock from DEL_NAVAL.productos where id_producto = @producto ) < @cantidad
 begin
  set @retorno = -1
  return   --no hay stock suficiente para la cantidad requerida
 end

--detecta cuantos puntos necesita  canjear
declare @puntos_a_canjear int
set @puntos_a_canjear = (select valor_puntos 
                           from DEL_NAVAL.productos 
                           where id_producto = @producto) * @cantidad   

--valida que el cliente tenga esos puntos 
if ((select SUM (puntos) - SUM(puntos_usados) 
    from DEL_NAVAL.consultarPuntos (@cliente,@fecha)    
    where id_canje is null) < @puntos_a_canjear)
 
  begin
  set @retorno = -2
  return  --puntos insuficientes   
  end
--si esta todo ok, consume los puntos y realiza la registracion del canje


 
 --actualizacion de puntos usados con cursores

declare @puntos_a_canjear_aux int --se va decrementando a medida que se van compensando 
                                   --con los puntos usados que se incrementan

declare @puntos_disponibles int --representa puntos sin asignar, o sin ser usados, para un mismo id_punto

--variables para el cursor
declare @id_punto int, @puntos int, @puntos_usados int


declare cPuntos cursor for
  select id_punto, puntos, puntos_usados
 from DEL_NAVAL.consultarPuntos (@cliente, @fecha) 
 where id_canje is null
 order by fecha_movimiento asc

begin transaction
                     
 open cPuntos
 fetch cPuntos into @id_punto, @puntos, @puntos_usados
 
 set @puntos_a_canjear_aux = @puntos_a_canjear 
 
 while (@@FETCH_STATUS = 0 or @puntos_a_canjear_aux > 0 )
 begin
 
 set @puntos_disponibles = @puntos - @puntos_usados
 if (@puntos_disponibles > 0 ) -- si hay puntos disponibles, los usa. sino pasa al siguiente registro
  begin
   if @puntos_disponibles <= @puntos_a_canjear_aux  --hay q usar todos los disponibles
    begin
      set @puntos_a_canjear_aux = @puntos_a_canjear_aux - @puntos_disponibles  
      --actualiza el estado del punto en la tabla
      update DEL_NAVAL.puntos
      set usados = @puntos_usados + @puntos_disponibles   --todos pasan a estar usados
      where id_punto = @id_punto       
    end
   else -- si hay mas disponible que los que necesito solo usar una parte de los disponibles
    begin
    
      --actualiza el estado del punto en la tabla
      update DEL_NAVAL.puntos
      set usados = @puntos_usados + @puntos_a_canjear_aux  --consumo todos los q necesito 
      where id_punto = @id_punto      
      
      set @puntos_a_canjear_aux = 0  --con esto sale del while
    end
  end
   
   fetch cPuntos into @id_punto, @puntos, @puntos_usados 
 end --fin del while
  
  if (@puntos_a_canjear_aux <> 0 )
    begin
     set @retorno = -3 --error inesperado 
    end 
    
   --si se pudieron consumir los puntos correctamente, realiza el registro del canje
   insert DEL_NAVAL.canjes
   values (@cliente, @producto, @cantidad, @fecha, @puntos_a_canjear)  
   --actualiza stock
   update DEL_NAVAL.productos 
   set stock = stock - @cantidad 
   where id_producto = @producto 
 
 commit
 close cPuntos
 deallocate cPuntos
  

set @retorno = 0 --todo ok              
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
puntos int, 
fecha_movimiento datetime, 
descripcion nvarchar(255),
puntos_usados int 

 )



  
 as
Begin
 insert @consultarPuntos
 select PU.id_punto, NULL, PU.cliente, PU.puntos, PU.fecha, 'Puntos ya consumidos: ' + 
CONVERT(nvarchar,PU.usados) + ' // Antiguedad: ' + ' ' + CONVERT(nvarchar,DATEDIFF (DAY,PU.fecha,@fecha)) +
 ' dias.' as Descripcion  , PU.usados
 
  from DEL_NAVAL.puntos PU
  where PU.cliente = @cliente
  and DATEDIFF (DAY,PU.fecha,@fecha)<= 365  --los vencidos no los muestra
  and PU.fecha <= @fecha -- solo trae puntos vigentes hasta la fecha ingresada por parametro
  
   union 
   
  select NULL, CA.id_canje, CA.cliente, (CA.puntos_canjeados * -1) , CA.fecha_canje,  'Canje: ' + 
  CONVERT(nvarchar,cantidad) + ' x ' + 
  (select descripcion from DEL_NAVAL.productos where id_producto = producto) , NULL
  
  from DEL_NAVAL.canjes CA
 where  CA.cliente = @cliente
 and CA.fecha_canje <= @fecha 
   order by PU.fecha  asc 

 
return 
    
End;
go
