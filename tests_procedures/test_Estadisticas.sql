use GD1C2013
go
set dateformat dmy
go

select top 5 * from DEL_NAVAL.destinos_comprados ('01/01/2013', '30/06/2013')

select top 5 * from DEL_NAVAL.destinos_micros_vacios ('01/07/2012', '31/12/2012')

select top 5 * from DEL_NAVAL.clientes_mayor_puntaje ('01/07/2012', '02/07/2012')

select top 5 * from DEL_NAVAL.destinos_pasajes_cancelados ('01/07/2012', '31/12/2013')

select top 5 * from DEL_NAVAL.micros_fuera_servicio('01/07/2012', '30/07/2012') 



select *
from DEL_NAVAL.compras
where comprador = 89165804
and fecha_compra between '01/07/2012' and '02/07/2012'

select *
from del_naval.pasajes 
where id_pasaje = 120297 OR
      ID_PASAJE = 183746
     
 

select *
from DEL_NAVAL.puntos
where cliente = 89165804
and fecha between '01/07/2012' and '02/07/2012'

select *
from del_naval.encomiendas 
where id_encomienda = 70996

update DEL_NAVAL.micros
set fecha_servicio_desde = '01/07/2012',
    fecha_servicio_hasta = '09/07/2012'
where cantidad_asientos = 40  

select * 
from del_naval.micros

select * 
from del_naval.cancelaciones







select COUNT( *) from DEL_NAVAL.ButacasDisponiblesXviaje (4070)