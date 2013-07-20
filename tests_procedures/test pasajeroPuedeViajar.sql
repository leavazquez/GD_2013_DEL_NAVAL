
use GD1C2013

set dateformat dmy
go

declare @outp int
exec del_naval.pasajeroPuedeViajar 32238185, 8539, @outp output
select @outp
-- -1 si no puede viajar
-- 0 si esta disponible

--5150 de 8a 20

select *
from DEL_NAVAL.viajes VI, DEL_NAVAL.pasajes PA
where VI.id_viaje = PA.viaje
and convert(date,VI.fecha_salida) = convert(date,'08/11/2012')