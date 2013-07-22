use GD1C2013
go

set dateformat dmy
go

select *
 from DEL_NAVAL.viajes
 where micro = 7
 and fecha_salida >= '27/01/2013'
 and fecha_estimada <= '30/01/2013'
 and cancelado = 1
 
 set dateformat dmy
go


exec del_naval.cancelarViajesDeUnMicro 7,'21/01/2013','22/01/2013', 'LiberaMicro_codigo','11/07/2012','lalala'
 

select *
from DEL_NAVAL.cancelaciones CA, DEL_NAVAL.pasajes PA
where ca.pasaje = PA.id_pasaje

 

select *
from DEL_NAVAL.cancelaciones CA, DEL_NAVAL.encomiendas EN
where ca.encomienda = EN.id_encomienda 