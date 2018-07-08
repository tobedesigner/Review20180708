using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Review20180708.Models
{   
	public  class OrderLineRepository : EFRepository<OrderLine>, IOrderLineRepository
	{

	}

	public  interface IOrderLineRepository : IRepository<OrderLine>
	{

	}
}