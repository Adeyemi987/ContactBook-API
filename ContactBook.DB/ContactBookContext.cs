using ContactBook.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.DB
{
    public class ContactBookContext : IdentityDbContext<User>
    {
        public ContactBookContext(DbContextOptions<ContactBookContext> options): base(options)
        {
             
        }

       
    }
}
 