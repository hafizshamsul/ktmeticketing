using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Assignment1_MuhammadHafiz.Models;

namespace Assignment1_MuhammadHafiz.Processes
{
    public class DbAccess
    {
        public IEnumerable<User> GetDbUsers()
        {
            IList<User> dbUser = new List<User>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreContext"].ConnectionString);
            string sql = @"SELECT * FROM Users";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dbUser.Add(new User()
                {
                    //serId = reader.GetInt32(1),
                    Name = reader.GetString(1),
                    ICpass = reader.GetString(2),
                    Email = reader.GetString(3),
                    Password = reader.GetString(4),
                    Role = reader.GetString(5),
                    DateRegistered = reader.GetDateTime(6)
                });
            }
            conn.Close();
            return dbUser;
        }

        public IEnumerable<Ticket> GetDbTickets()
        {
            IList<Ticket> dbTicket = new List<Ticket>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreContext"].ConnectionString);
            string sql = @"SELECT * FROM Tickets JOIN Users ON Users.UserId = Tickets.UserId";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dbTicket.Add(new Ticket()
                {
                    
                    //Users. = reader.GetInt32(1),
                    StartPoint = reader.GetString(2),
                    EndPoint = reader.GetString(3),
                    Category = reader.GetString(4),
                    WayType = reader.GetString(5),
                    Quantity = reader.GetInt32(6),
                    InitialPrice = reader.GetDouble(7),
                    BaseDiscountRate = reader.GetDouble(8),
                    CatDiscountRate = reader.GetDouble(9),
                    TotalPrice = reader.GetDouble(10),
                    DateOfPurchase = reader.GetDateTime(11)
            });
            }
            conn.Close();
            return dbTicket;
        }
    }
}