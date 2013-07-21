
use GD1C2013

set dateformat dmy
go

declare @outp int
exec del_naval.insertarCompra 32238185, 10547, '27/01/2014', @outp output
select @outp


select *
from del_naval.tipos_servicio

declare @codigo_pasaje int
set  @codigo_pasaje = (select MAX(codigo_pasaje)+1 from del_naval.pasajes)
select @codigo_pasaje


declare @codigo_pasaje int
declare @outp int
--set @outp = 1
exec del_naval.insertarPasaje 506084, 10547, 32238185, 1073,@codigo_pasaje output, @outp output
select @outp
select @codigo_pasaje

update DEL_NAVAL.clientes 
set jubilado_pensionado = 1
where id_dni = 32238185

 
select * from del_naval.ButacasDisponiblesXviaje (10547)

select *
from DEL_NAVAL.pasajes 
where codigo_pasaje = (select MAX(codigo_pasaje) from del_naval.pasajes)




declare @codigo_encomienda int
declare @outp int
exec del_naval.insertarEncomienda 506084, 10547, 32238185, 10,@codigo_encomienda  output, @outp output
select @outp
select @codigo_encomienda 


select DEL_NAVAL.PesoLibreEncomiendasXviaje (10547)

update DEL_NAVAL.encomiendas 
set cancelado = 1
where viaje = 10547
 
 
 select *
 from DEL_NAVAL.viajes
 where id_viaje = 10547
 
 

set dateformat dmy
go

exec del_naval.insertarDatosTarjeta 506084, 1, '3223 8185', '103' , '27/03/2015'
-- voucher, id_tarjeta, numero, codigo seguridad, vencimiento

select *
from DEL_NAVAL.tarjetas

select *
from DEL_NAVAL.pagos_tarjetas