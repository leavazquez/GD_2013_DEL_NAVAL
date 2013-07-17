
use GD1C2013

set dateformat dmy
go

declare @outp int
exec del_naval.insertarViaje 3, 25, '27/03/2013','30/03/2013', @outp output
select @outp
-- -1 micro no disponibkle
-- -2 tipo de servicio no compatible
--  0 viaje creado

select *
from DEL_NAVAL.recorridos RE, DEL_NAVAL.micros MI
where RE.id_recorrido = 3
and MI.id_micro = 25

select top 5 *
from DEL_NAVAL.viajes
where fecha_estimada = '30/03/2013'