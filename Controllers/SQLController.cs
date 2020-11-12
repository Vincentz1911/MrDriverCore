using MrDriverCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace MrDriverCore.Controllers
{
    public class SQLController
    {
        string connectionString = @"Data Source=62.171.142.200;Initial Catalog=MrDriverDB;User Id=mrdriver;Password=Pegasus76!;";

        public List<Location> GetLocationList(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * from [Location] WHERE userid = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = id;

            DataTable dataTable = SQL2Datatable(cmd);
            if (dataTable.Rows.Count > 0) return Datatable2List<Location>(dataTable);
            else return null;
        }

        internal List<User> GetAllDrivers()
        {
            SqlCommand cmd = new SqlCommand("SELECT * from [User]");
            DataTable dataTable = SQL2Datatable(cmd);
            if (dataTable.Rows.Count > 0) return Datatable2List<User>(dataTable);
            else return null;
        }

        internal User GetDriver(string driver)
        {
            SqlCommand cmd = new SqlCommand("SELECT * from [User] WHERE username = @driver");
            cmd.Parameters.Add("@driver", SqlDbType.NVarChar).Value = driver;
            DataTable dataTable = SQL2Datatable(cmd);
            if (dataTable.Rows.Count > 0) return Datatable2List<User>(dataTable)[0];
            else return null;
        }

        internal int UpdateDriver(User user)
        {
            SqlCommand cmd = new SqlCommand("UPDATE [User] SET username = @username, password = @password WHERE id = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = user.Id;
            cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = user.Username;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = user.Password;
            return ExecuteSQL(cmd);
        }

        internal void DeleteLocation(int id)
        {
            throw new NotImplementedException();
        }

        //internal DriverModel GetDriverById(int id)
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT * from [driver] WHERE id = @id");
        //    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
        //    DataTable dataTable = SQL2Datatable(cmd);
        //    if (dataTable.Rows.Count > 0) return Datatable2List<DriverModel>(dataTable)[0];
        //    else return null;
        //}
        internal User CreateDriver()
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO [User] (username, password) " +
                "OUTPUT INSERTED.id VALUES ('Driver', '1234')");
            int driverID = ExecuteSQLGetID(cmd);
            User driver = new User(driverID, "Driver", "1234");
            return driver;
        }

        internal int CreateLocation(Location location)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO [Location] " +
                "(userid, name, street, city, latitude, longitude) " +
                "OUTPUT INSERTED.id " +
                "VALUES (@userid, @name, @street, @city, @latitude, @longitude)");

            cmd.Parameters.Add("@userid", SqlDbType.Int).Value = location.UserId;
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = location.Name;
            cmd.Parameters.Add("@street", SqlDbType.NVarChar, 50).Value = location.Street;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value = location.City;
            cmd.Parameters.Add("@latitude", SqlDbType.Float).Value = location.Latitude;
            cmd.Parameters.Add("@longitude", SqlDbType.Float).Value = location.Longitude;
            //cmd.Parameters.Add("@saved", SqlDbType.Bit).Value = location.Saved;
            return ExecuteSQLGetID(cmd); //executes sqlcommand and returns id of created resident
        }

        internal int ExecuteSQL(SqlCommand cmd)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                cmd.Connection.Open();
                return cmd.ExecuteNonQuery(); //Returns rows changed
            }
        }

        internal int ExecuteSQLGetID(SqlCommand cmd)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                cmd.Connection.Open();
                var v = cmd.ExecuteScalar();
                return (int)v; //Returns OUTPUT id
            }
        }

        //Retrieves data from SQL as a datatable
        internal DataTable SQL2Datatable(SqlCommand cmd)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                adapter.Dispose();

                return dataTable;
            }
        }

        //Converts the datatable to list based on the item types
        internal List<T> Datatable2List<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                list.Add(item);
            }
            return list;
        }

        //Sets value from datatable row based on the column name
        private T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    //If property name = rows column name and is not DBNull, then add value
                    if (pro.Name.ToLower() == column.ColumnName.ToLower() && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else continue;
                }
            }
            return obj;
        }
    }
}

