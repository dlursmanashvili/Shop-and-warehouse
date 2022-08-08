using System;
using Store.Models;
using Microsoft.Extensions.Configuration;

namespace Store.Repositories
{
    public class ContactInfoRepository : RepositoryBase<ContactInfo>
    {
        public ContactInfoRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}