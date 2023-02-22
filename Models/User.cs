
using System.Net;

namespace Final_project_server.Models
{
    public class User
    {
        int id;
        string email;
        string phone_number;
        string full_name;
        string password;
        string address;
        bool is_admin;
        int closet_id;
        string user_image;

        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string Phone_number { get => phone_number; set => phone_number = value; }
        public string Full_name { get => full_name; set => full_name = value; }
        public string Password { get => password; set => password = value; }
        public string Address { get => address; set => address = value; }
        public bool Is_admin { get => is_admin; set => is_admin = value; }
        public int Closet_id { get => closet_id; set => closet_id = value; }
        public string User_image { get => user_image; set => user_image = value; }

        static List<User> usersList = new List<User>();


        public List<User> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadUsers();
        }

        public bool Insert()
        {
            DBservices dbs = new DBservices();
            if (dbs.InsertUser(this) == 1)
            {
                usersList.Add(this);
                return true;
            }
            return false;
        }


        public User Login()
        {
            DBservices dbs = new DBservices();
            return dbs.Login(this);
        }
        //public bool Update()
        //{
        //    DBservices dbs = new DBservices();
        //    if (dbs.UpadteUser(this) == 1)
        //    {
        //        return true;
        //    }
        //    return false;
        //}






    }
}
