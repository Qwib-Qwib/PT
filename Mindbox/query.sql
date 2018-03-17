select c.customerId
      ,c.name
from customers c inner join orders o on c.customerId = o.customerId 
where o.product_name in ('Milk', 'SourCream')
group by c.customerId, c.name
having( sum(case when o.product_name = 'Milk' then 1 else 0 end) > 0
	and sum(case when o.product_name = 'SourCream' 
				  and datepart(year, o.purchasedate) = datepart(year, getdate())
				  and datepart(month, o.purchasedate) = datepart(month, getdate()) then 1 else 0 end) = 0 
	  )