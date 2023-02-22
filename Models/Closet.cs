namespace Final_project_server.Models
{
    public class Closet
    {
        int id;
        string description;

        public int Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }

        static List<Closet> closetsList = new List<Closet>();

        public List<Closet> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadClosets();
        }

        public bool Insert()
        {
                DBservices dbs = new DBservices();
                if (dbs.InsertCloset(this) > 0)
                {
                closetsList.Add(this);
                    return true;
                }
                return false;

        }


    }
}
