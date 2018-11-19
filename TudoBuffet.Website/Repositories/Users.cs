using System;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Repositories
{
    public class Users : IUsers
    {
        private readonly MainDbContext mainDbContext;

        public Users(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public void Insert(User user)
        {
            mainDbContext.Add(user);
            mainDbContext.SaveChanges();
        }
    }
}
