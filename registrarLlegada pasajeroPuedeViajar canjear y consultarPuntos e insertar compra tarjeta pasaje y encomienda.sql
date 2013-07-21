
use GD1C2013
go


if OBJECT_ID ('del_naval.registrarLlegada','P') is not null
drop procedure del_naval.registrarLlegada

if OBJECT_ID ('del_naval.pasajeroPuedeViajar','P') is not null
drop procedure del_naval.pasajeroPuedeViajar

if OBJECT_ID ('del_naval.canjearPuntos','P') is not null
drop procedure del_naval.canjearPuntos

if OBJECT_ID ('del_naval.consultarPuntos','TF') is not null
drop function del_naval.consultarPuntos 

if OBJECT_ID ('del_naval.insertarCompra','P') is not null
drop procedure del_naval.insertarCompra

if OBJECT_ID ('del_naval.insertarDatosTarjeta','P') is not null
drop procedure del_naval.insertarDatosTarjeta

if OBJECT_ID ('del_naval.insertarPasaje','P') is not null
drop procedure del_naval.insertarPasaje

if OBJECT_ID ('del_naval.insertarEncomienda','P') is not null
drop procedure del_naval.insertarEncomienda


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






-- -2 puntos insuficientes
-- -1 no hay stock
-- -3 error inesperado

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


 

create procedure del_naval.insertarCompra
(@comprador int,
@forma_pago nvarchar(255),
@fecha datetime,
@retorno int output
)
as
begin
 

declare  @ret table (id_voucher int)

     insert into del_naval.compras
     output inserted.id_voucher into @ret
     values (@comprador, @forma_pago, @fecha)
     
set @retorno = (select id_voucher from @ret)
    
  
  
   return  --devuelve el id de voucher creado

    
end;
go



create procedure del_naval.insertarDatosTarjeta
(@voucher int,
@tarjeta int, 
@numero nvarchar(255),
@cod_ser nvarchar(255),
@fecha datetime
)
as
begin
    
     insert into del_naval.pagos_tarjetas
     values (@voucher, @tarjeta, @numero, @cod_ser, @fecha)
  
   return 0 
    
end;
go



create procedure del_naval.insertarPasaje
(@voucher int,
@viaje int,
@pasajero int,
@butaca int,
@codigo_pasaje int output,
@monto int output
)
as
begin
 
declare @precio_base_pasaje numeric(18,2),
        @porcentaje numeric(18,2),
        @recorrido int,
        @tipo_servicio int
        

set @recorrido = (select recorrido from DEL_NAVAL.viajes where id_viaje = @viaje )        
set @precio_base_pasaje = (select precio_base_pasaje from DEL_NAVAL.recorridos where id_recorrido = @recorrido)      
set @tipo_servicio = (select tipo_servicio from DEL_NAVAL.recorridos where id_recorrido = @recorrido)       
set @porcentaje = (select porcentaje from DEL_NAVAL.tipos_servicio where id_servicio = @tipo_servicio)

--calcula el monto

if (@monto <> 0) or @monto is null --si el monto no viene en cero, lo calcula, sino lo deja como esta
-- viene en cero cuando estamos en presencia de un acompañante de discapacitado o de un discapacitado
     
      if (select jubilado_pensionado from DEL_NAVAL.clientes where id_dni = @pasajero) = 1
      --si es jubilado o pensionado calcula con 50% de descuento     
        set @monto = convert (int, (@precio_base_pasaje * ((@porcentaje / 100) + 1)) * 50   )
      else
        set @monto = convert (int, (@precio_base_pasaje * ((@porcentaje / 100) + 1)) * 100   )


--obtiene el codigo de pasaje siguiente
set  @codigo_pasaje = (select MAX(codigo_pasaje)+1 from del_naval.pasajes)



declare @montonumeric numeric(18,2)
set @montonumeric = (convert(numeric(18,2),@monto) /100)


insert DEL_NAVAL.pasajes 
values (@voucher, @viaje, @pasajero, @butaca, 0, @montonumeric ,@codigo_pasaje)  
 
  
  
   return  --devuelve los valores de monto y codigo de pasaje

    
end;
go



create procedure del_naval.insertarEncomienda
(@voucher int,
@viaje int,
@remitente int,
@peso int,
@codigo_encomienda int output,
@monto int output
)
as
begin
 
declare @precio_kg_encomienda numeric(18,2),
        @recorrido int
      
        

set @recorrido = (select recorrido from DEL_NAVAL.viajes where id_viaje = @viaje )        
set @precio_kg_encomienda = (select precio_kg_encomienda  from DEL_NAVAL.recorridos where id_recorrido = @recorrido)      


--calcula el monto
 set @monto = convert (int, (@precio_kg_encomienda  * @peso * 100) )


--obtiene el codigo de pasaje siguiente
set @codigo_encomienda = (select MAX(codigo_encomienda)+1 from del_naval.encomiendas)



declare @montonumeric numeric(18,2)
set @montonumeric = (convert(numeric(18,2),@monto) /100)


insert DEL_NAVAL.encomiendas  
values (@voucher, @viaje, @remitente, @peso, 0, @montonumeric ,@codigo_encomienda)  
 
  
  
   return  --devuelve los valores de monto y codigo de pasaje

    
end;
go
