namespace Final_project_server.Models
{
    public class Item
    {
        int id;
        int closet_ID;
        int type_name_ID;
        int brand_name_ID;
        string name;
        bool sale_status;
        double price;
        string color;
        string size;
        string description;
        string shipping_method;

        List<Item> itemsList =new List<Item>();

        public int Id { get => id; set => id = value; }
        public int Closet_ID { get => closet_ID; set => closet_ID = value; }
        public int Type_name_ID { get => type_name_ID; set => type_name_ID = value; }
        public int Brand_name_ID { get => brand_name_ID; set => brand_name_ID = value; }
        public string Name { get => name; set => name = value; }
        public bool Sale_status { get => sale_status; set => sale_status = value; }
        public double Price { get => price; set => price = value; }
        public string Color { get => color; set => color = value; }
        public string Size { get => size; set => size = value; }
        public string Description { get => description; set => description = value; }
        public string Shipping_method { get => shipping_method; set => shipping_method = value; }

        public List<Item> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadItems();
        }

        public List<Item> ReadByCloset()
        {
            DBservices dbs = new DBservices();
            return dbs.ItemByCloset(this);
        }

        public bool Insert()
        {
            DBservices dbs = new DBservices();
            if (dbs.InsertItem(this) == 1)
            {
                closetsList.Add(this);
                return true;
            }
            return false;

        }
    }
}
