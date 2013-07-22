select *
from DEL_NAVAL.micros
where id_micro = 1

set dateformat dmy
go


 select *
 from DEL_NAVAL.viajes
 where micro = 15
  and fecha_salida >= '21/01/2013'
 and fecha_estimada <= '23/01/2013'

 
select * 
 from del_naval.pasajes
 where viaje = 3663
 
select * from del_naval.ButacasDisponiblesXviaje (10327)

  set dateformat dmy
go

declare @retorno int
exec del_naval.intentarBajarMicro 15,'21/01/2013','22/01/2013', @retorno output
select @retorno 
 
 exec bajaOserviceMicro 4,'01/05/2012', NULL