select c.cutomerid
      ,name
from customers c inner join orders o on c.customerId = o.customer.Id 
where o.product_name in ('Milk', 'SourCream')
group by c.customerid, c. name
having( sum(case when o.product_name = 'Milk' then 1 else 0 end) > 0
	and sum(case when o.product_name = 'SourCream' 
				  and datepart(y, o.purchasedate) = datepart(y, getdate()
				  and datepart(m, o.purchasedate) = datepart(m, getdate())
	    			then 1 else 0 end) = 0 
	  )