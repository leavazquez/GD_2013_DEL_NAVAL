
declare @viaje int
set @viaje = 100
while @viaje < 1000
begin

select COUNT (*), @viaje
 from DEL_NAVAL.ButacasDisponiblesXviaje (@viaje)

set @viaje = @viaje + 1

end


select  *
from DEL_NAVAL.viajes
where (select count (*) from DEL_NAVAL.ButacasDisponiblesXviaje (id_viaje)) < 5


select dbo.PesoLibreEncomiendasXviaje (12)

select * from DEL_NAVAL.pasajes
where viaje = 12

select * from DEL_NAVAL.viajes where id_viaje = 847

select * from DEL_NAVAL.micros where id_micro = 26

update DEL_NAVAL.micros 
set cantidad_asientos = 40
where id_micro = 26

select * from DEL_NAVAL.butacas
where micro = 26

insert DEL_NAVAL.butacas_ocupadas
 select 847, id_butaca 
 from DEL_NAVAL.butacas
where id_butaca not in (select butaca from DEL_NAVAL.butacas_ocupadas
                        where viaje = 847)
      and micro = 26
      
      
      
    select *
 from DEL_NAVAL.ButacasDisponiblesXviaje (847)

