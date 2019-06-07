using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaBox.Context.DataModel;
using PizzaBox.Library.DataClasses;
using PizzaBox.Library.Repositories;

namespace PizzaBox.Context.Repositories
{
    public class UserRepository : IUserRepository
    {
        public bool Create(User user)
        {
            if (Read(user.Username) != null) return false;
            //else
            Database.GetContext().Account.Add(Mapper.Map(user));
            Database.SaveContext();
            return true;
        }

        public User Read(int id)
        {
            return Mapper.Map(Database.GetContext().Account.Find(id));
        }

        public User Read(string username)
        {
            return Mapper.Map(Database.GetContext().Account.Where(x => x.Username == username).FirstOrDefault());
        }
    }
}
