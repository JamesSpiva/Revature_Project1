using System;
using System.Collections.Generic;
using System.Text;
using PizzaBox.Library.DataClasses;

namespace PizzaBox.Library.Repositories
{
    public interface IUserRepository
    {
        bool Create(User user);
        User Read(int id);
        User Read(string username);
        //void Update();
        //void Delete();
    }
}
