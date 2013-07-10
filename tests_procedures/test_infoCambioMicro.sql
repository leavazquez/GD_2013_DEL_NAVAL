select *
from DEL_NAVAL.micros

set dateformat dmy
go

 select *
 from DEL_NAVAL.viajes
 where micro = 3
 and fecha_salida >= '01/05/2012'
 and fecha_estimada <= '15/05/2012'
 and cancelado = 0
 

  set dateformat dmy
go

declare @retorno int
exec infoCambioMicro 1,'01/05/2012',NULL, @retorno output
select @retorno 
 
 exec bajaOserviceMicro 4,'01/05/2012', NULL