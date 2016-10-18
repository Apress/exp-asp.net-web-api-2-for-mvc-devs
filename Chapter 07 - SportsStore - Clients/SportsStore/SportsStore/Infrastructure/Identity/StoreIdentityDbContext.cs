using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace SportsStore.Infrastructure.Identity {

    public class StoreIdentityDbContext : IdentityDbContext<StoreUser> {

        public StoreIdentityDbContext()
            : base("SportsStoreIdentityDb") {
            Database.SetInitializer<StoreIdentityDbContext>(new
                StoreIdentityDbInitializer());
        }

        public static StoreIdentityDbContext Create() {
            return new StoreIdentityDbContext();
        }
    }
}
