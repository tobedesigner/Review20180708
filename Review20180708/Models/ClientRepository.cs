using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Review20180708.Models
{   
	public  class ClientRepository : EFRepository<Client>, IClientRepository
	{

	}

	public  interface IClientRepository : IRepository<Client>
	{

	}
}