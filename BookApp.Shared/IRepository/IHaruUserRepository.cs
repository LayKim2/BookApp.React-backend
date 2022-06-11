using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared
{
    public interface IHaruUserRepository : IHaruUserCrudRepository<HaruUser>
    {
        Task<HaruUser> GetHaruUser(string username); // get detail
        //public async Task<HaruUser> GetHaruUser(string username)
    }
}
