SELECT *
FROM DEL_NAVAL.canjes

SELECT *
FROM DEL_NAVAL.productos

SELECT *
FROM DEL_NAVAL.puntos

select *
from del_naval.clientes

set dateformat dmy
go

--tabla completa
set dateformat dmy
go
select * from DEL_NAVAL.consultarPuntos (32238185,'27/01/2014')

--puntos ganados
set dateformat dmy
go
select cliente, puntos, fecha, usados, vejez 
 from DEL_NAVAL.consultarPuntos (32238185,'27/01/2014') 
 where producto is null

--puntos canjeados
set dateformat dmy
go
select producto, cantidad, fecha_canje, puntos_canjeados 
 from DEL_NAVAL.consultarPuntos (32238185,'27/01/2014') 
 where puntos is null 


--calcular puntos listos para usar por cliente en una fecha
set dateformat dmy
go
select SUM (puntos) - SUM(usados) from DEL_NAVAL.consultarPuntos (32238185,'27/01/2014')