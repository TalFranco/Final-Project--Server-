using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Final_project_server.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Hosting;

    public class DBservices
    {
        public SqlDataAdapter da;
        public DataTable dt;


        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
    // This method read all the users from the table
    //--------------------------------------------------------------------------------- 
    public List<User> ReadUsers()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateReadUsersCommandSP("spReadUsers", con);             // create the command


        List<User> usersList = new List<User>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                User user = new User();

                user.Id = Convert.ToInt32(dataReader["ID"]);
                user.Email = dataReader["Email"].ToString();
                user.Phone_number = dataReader["Phone_number"].ToString();
                user.Full_name = dataReader["Full_name"].ToString();
                user.Password = dataReader["Password"].ToString();
                user.Address = dataReader["Address"].ToString();
                user.Is_admin = Convert.ToBoolean(dataReader["IsAdmin"]);
                user.Closet_id = Convert.ToInt32(dataReader["Closet_id"]);
                user.User_image = dataReader["User_image"].ToString();

                usersList.Add(user);
            }

            return usersList;
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    //---------------------------------------------------------------------------------
    // Create the ReadUseres SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateReadUsersCommandSP(string spName, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        return cmd;
    }


    // This method inserts a user to the users table 
    //--------------------------------------------------------------------------------------------------
    public int InsertUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateInsertUserCommandSP("spInsertUser", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the InsertUser SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateInsertUserCommandSP(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@Phone_number", user.Phone_number);
        cmd.Parameters.AddWithValue("@Full_name", user.Full_name);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        cmd.Parameters.AddWithValue("@Address", user.Address);
        cmd.Parameters.AddWithValue("@IsAdmin", user.Is_admin);
        cmd.Parameters.AddWithValue("@Closet_ID", user.Closet_id);
        cmd.Parameters.AddWithValue("@User_image", user.User_image);
        return cmd;
    }


    // This method read all the closets from the table
    //--------------------------------------------------------------------------------- 
    public List<Closet> ReadClosets()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateReadClosetsCommandSP("spReadCloset", con);             // create the command


        List<Closet> closetList = new List<Closet>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Closet closet = new Closet();

                closet.Id = Convert.ToInt32(dataReader["ID"]);
                closet.Description = dataReader["Description"].ToString();

                closetList.Add(closet);

            }

            return closetList;
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    //---------------------------------------------------------------------------------
    // Create the ReadCloset SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateReadClosetsCommandSP(string spName, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        return cmd;
    }

    // This method makes the login order
    //---------------------------------------------------------------------------------
    public User Login(User User)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateLoginUserCommandSP("spLoginUser", con, User);// create the command
        User U = new User();
        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                U.Id = Convert.ToInt32(dataReader["ID"]);
                U.Email = dataReader["Email"].ToString();
                U.Phone_number = dataReader["Phone_number"].ToString();
                U.Full_name = dataReader["Full_name"].ToString();
                U.Password = dataReader["Password"].ToString();
                U.Address = dataReader["Address"].ToString();
                U.Is_admin = Convert.ToBoolean(dataReader["IsAdmin"]);
                U.Closet_id = Convert.ToInt32(dataReader["Closet_id"]);
                U.User_image = dataReader["User_image"].ToString();
            }
            return U;

        }
        catch (Exception ex)
        {

            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    // Create the Login SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateLoginUserCommandSP(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure


        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@Password", user.Password);


        return cmd;
    }


    // This method inserts a closet to the closets table 
    //--------------------------------------------------------------------------------------------------
    public int InsertCloset(Closet closet)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateInsertClosetCommandSP("spInsertCloset", con, closet);             // create the command

        try
        {
            //int numEffected = cmd.ExecuteNonQuery(); // execute the command
            int id= Convert.ToInt32(cmd.ExecuteScalar());
            return id;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the InsertCloset SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateInsertClosetCommandSP(String spName, SqlConnection con, Closet closet)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@Description", closet.Description);
     
        return cmd;
    }

    // This method read all the items from the table
    //--------------------------------------------------------------------------------- 
    public List<Item> ReadItems()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateReadItemsCommandSP("spReadItem", con);             // create the command


        List<Item> itemsList = new List<Item>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Item item  = new Item();

                item.Id = Convert.ToInt32(dataReader["ID"]);
                item.Closet_ID = Convert.ToInt32(dataReader["Closet_ID"]);
                item.Type_name_ID = Convert.ToInt32(dataReader["Type_name_ID"]);
                item.Brand_name_ID = Convert.ToInt32(dataReader["Brand_name_ID"]);
                item.Name = dataReader["Name"].ToString();
                item.Sale_status = Convert.ToBoolean(dataReader["Sale_status"]);
                item.Price = Convert.ToInt32(dataReader["Price"]);
                item.Color = dataReader["Color"].ToString();
                item.Size = dataReader["Size"].ToString();
                item.Description = dataReader["Description"].ToString();
                item.Shipping_method = dataReader["Shipping_method"].ToString();

                itemsList.Add(item);


                }

            return itemsList;
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the ReadItems SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateReadItemsCommandSP(string spName, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        return cmd;
    }

    // This method inserts an item to the items table 
    //--------------------------------------------------------------------------------------------------
    public int InsertItem(Item item)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateInsertItemCommandSP("spInsertItem", con, item);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the InsertItem SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateInsertItemCommandSP(String spName, SqlConnection con, Item item)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@Closet_ID", item.Closet_ID);
        cmd.Parameters.AddWithValue("@Type_name_ID", item.Type_name_ID);
        cmd.Parameters.AddWithValue("@Brand_name_ID", item.Brand_name_ID);
        cmd.Parameters.AddWithValue("@item.Name", item.Name);
        cmd.Parameters.AddWithValue("@Sale_status", item.Sale_status);
        cmd.Parameters.AddWithValue("@Price", item.Price);
        cmd.Parameters.AddWithValue("@Color", item.Color);
        cmd.Parameters.AddWithValue("@Size", item.Size);
        cmd.Parameters.AddWithValue("@Description", item.Description);
        cmd.Parameters.AddWithValue("@Shipping_method", item.Shipping_method);

        return cmd;
    }

    // This method read item by its closet
    //---------------------------------------------------------------------------------
    public List<Item> ItemByCloset(Item item)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateItemByClosetCommandSP("spItemByCloset", con, item);// create the command

        List<Item> itemsList = new List<Item>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Item item = new Item();

                item.Id = Convert.ToInt32(dataReader["ID"]);
                item.Closet_ID = Convert.ToInt32(dataReader["Closet_ID"]);
                item.Type_name_ID = Convert.ToInt32(dataReader["Type_name_ID"]);
                item.Brand_name_ID = Convert.ToInt32(dataReader["Brand_name_ID"]);
                item.Name = dataReader["Name"].ToString();
                item.Sale_status = Convert.ToBoolean(dataReader["Sale_status"]);
                item.Price = Convert.ToInt32(dataReader["Price"]);
                item.Color = dataReader["Color"].ToString();
                item.Size = dataReader["Size"].ToString();
                item.Description = dataReader["Description"].ToString();
                item.Shipping_method = dataReader["Shipping_method"].ToString();

                itemsList.Add(item);
            }
            return itemsList;

        }
        catch (Exception ex)
        {

            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    // Create the Login SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateItemByClosetCommandSP(String spName, SqlConnection con, Item item)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure


        cmd.Parameters.AddWithValue("@Closet_ID", item.Closet_ID);


        return cmd;
    }



}


