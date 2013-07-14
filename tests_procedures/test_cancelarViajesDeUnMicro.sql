use GD1C2013
go

set dateformat dmy
go

select *
 from DEL_NAVAL.viajes
 where micro = 7
 and fecha_salida >= '27/01/2013'
 and fecha_estimada <= '12/07/9999'
 and cancelado = 0 
 
 

exec cancelarViajesDeUnMicro 7,'27/01/2013','12/07/9999', 'LiberaMicro_codigo','11/07/2012','LiberaMicro_motivo'
 

