using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Review20180708.Models
{   
	public  class ClientRepository : EFRepository<Client>, IClientRepository
	{
        public override IQueryable<Client> All()
        {
            return base.All()
                .Where(c => c.IsDelete == false)
                .OrderByDescending(c => c.ClientId)
                .Take(100);
        }

        public override void Delete(Client entity)
        {
            entity.IsDelete = true;
        }
    }

	public  interface IClientRepository : IRepository<Client>
	{

	}
}