using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Practical_lesson_No._28.Classes;

namespace Practice_work_28.Classes
{
    public class СomputerСlub
    {
        public int ClubID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string OpeningHours { get; set; }

        public СomputerСlub(int clubID, string name, string address, string openingHours)
        {
            ClubID = clubID;
            Name = name;
            Address = address;
            OpeningHours = openingHours;
        }

        public void Update(int clubID, string name, string address, string openingHours)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE ComputerClub SET Name = @name, Address = @address, OpeningHours = @openingHours WHERE ClubID = @clubID";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@openingHours", openingHours);
                cmd.Parameters.AddWithValue("@clubID", clubID);
                cmd.ExecuteNonQuery();
            }
        }

        static public void Insert(string name, string address, string openingHours)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO ComputerClub (Name, Address, OpeningHours) VALUES (@name, @address, @openingHours)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@openingHours", openingHours);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int clubID)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM ComputerClub WHERE ClubID = @clubID";
                cmd.Parameters.AddWithValue("@clubID", clubID);
                cmd.ExecuteNonQuery();
            }
        }

        static public List<СomputerСlub> GetAll()
        {
            List<СomputerСlub> clubs = new List<СomputerСlub>();
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM ComputerClub";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clubs.Add(new СomputerСlub(reader.GetInt32("ClubID"), reader.GetString("Name"), reader.GetString("Address"), reader.GetString("OpeningHours")));
                    }
                }
            }
            return clubs;
        }
    }
}
