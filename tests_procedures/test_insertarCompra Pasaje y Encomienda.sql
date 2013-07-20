
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
exec del_naval.insertarPasaje 506084, 10547, 32238185, 1073,@codigo_pasaje output, @outp output
select @outp
select @codigo_pasaje

 
select * from del_naval.ButacasDisponiblesXviaje (10547)

select *
from DEL_NAVAL.pasajes 
where codigo_pasaje = (select MAX(codigo_pasaje) from del_naval.pasajes)