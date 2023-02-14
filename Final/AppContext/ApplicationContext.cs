using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Final.BusinessObjects;

namespace Final.Context
{

    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Years> T_Years { get; set; }



    }
}
