select id_recorrido, COUNT (*) as cant
from del_naval.recorridos re, DEL_NAVAL.viajes vi
where vi.recorrido = re.id_recorrido
group by id_recorrido
order by cant desc

select *
from del_naval.recorridos re, DEL_NAVAL.viajes vi
where vi.recorrido = re.id_recorrido
and re.id_recorrido = 4


exec cancelarRecorrido 4,'RANDOM',"07/25/2011",'cancelarRecorrido'
