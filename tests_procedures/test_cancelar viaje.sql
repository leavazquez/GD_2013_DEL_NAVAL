USE GD1C2013
go

select * 
from del_naval.pasajes PA, del_naval.encomiendas EN, del_naval.viajes VI
where PA.viaje = VI.id_viaje
and   EN.viaje = VI.id_viaje
and   VI.id_viaje = 12

exec del_naval.cancelarViaje 92,'RANDOM',"07/25/2012",'cancelarViaje'