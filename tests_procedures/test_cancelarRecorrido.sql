select id_recorrido, COUNT (*) as cant
from del_naval.recorridos re, DEL_NAVAL.viajes vi
where vi.recorrido = re.id_recorrido
   and vi.cancelado = 0
   and vi.fecha_salida >= '12/07/2012'
group by id_recorrido
order by cant desc

select *
from del_naval.recorridos re, DEL_NAVAL.viajes vi
where vi.recorrido = re.id_recorrido
  and vi.cancelado = 0
   and vi.fecha_salida >= '12/07/2012'
and re.id_recorrido = 8


exec cancelarRecorrido 8,'RANDOM',"12/07/2012",'cancelarRecorrido'
