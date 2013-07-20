if OBJECT_ID ('del_naval.insertarCompra','P') is not null
drop procedure del_naval.insertarCompra

if OBJECT_ID ('del_naval.insertarPasaje','P') is not null
drop procedure del_naval.insertarPasaje



if OBJECT_ID ('del_naval.insertarEncomienda','P') is not null
drop procedure del_naval.insertarEncomienda


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





