using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace EasyRent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User() { Username="test", Email="test" , Password= "test", PhoneNumber="test", Role="ADMIN"};

            DBDataContext db = new DBDataContext();
            db.
        }
    }
}
