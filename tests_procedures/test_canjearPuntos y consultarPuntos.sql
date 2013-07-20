insert DEL_NAVAL.compras
output inserted.id_voucher
values (32238185,'TARJETA','27/01/2014')

SELECT *
FROM DEL_NAVAL.canjes


SELECT *
FROM DEL_NAVAL.compras

SELECT *
FROM DEL_NAVAL.pasajes 

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


set dateformat dmy
go
declare @outp int
exec del_naval.canjearPuntos 32238185,9, 1, '27/01/2014', @outp output
select @outp
-- -2 puntos insuficientes
-- -1 no hay stock
-- -3 error inesperado

select *
from DEL_NAVAL.productos 

/*--puntos ganados
set dateformat dmy
go
select id_punto, cliente, puntos_obtenidos as Puntos, fecha_movimiento, 'Utilizados: ' + 
CONVERT(nvarchar,puntos_usados) + ' // Antiguedad: ' + ' ' + CONVERT(nvarchar,antiguedad_dias) + ' dias.' as Descripcion
 from DEL_NAVAL.consultarPuntos (32238185,'27/01/2014') 
 where id_canje is null
union
select id_canje, cliente,  puntos_canjeados * -1, fecha_movimiento,   'Canje: ' + CONVERT(nvarchar,cantidad) + ' x '
 + (select descripcion from DEL_NAVAL.productos where id_producto = producto)
 from DEL_NAVAL.consultarPuntos (32238185,'27/01/2014') 
 where id_punto is null 
order by fecha_movimiento asc  */

select * 
from DEL_NAVAL.puntos

update DEL_NAVAL.puntos 
set usados = 0
where id_punto = 93358

--calcular puntos listos para usar por cliente en una fecha
set dateformat dmy
go
select SUM (puntos) - SUM(puntos_usados) from DEL_NAVAL.consultarPuntos (32238185,'27/01/2014')
where id_canje is null