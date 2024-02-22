using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Practical_lesson_No._28.Classes;

namespace Practical_lesson_No._28.Classes
{
    public class ComputerClub
    {
        public int ClubID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string OpeningHours { get; set; }

        public ComputerClub(int clubID, string name, string address, string openingHours)
        {
            ClubID = clubID;
            Name = name;
            Address = address;
            OpeningHours = openingHours;
        }

        public bool Update(string name, string address, string openingHours)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@name", name},
                {"@address", address},
                {"@openingHours", openingHours},
                {"@clubID", this.ClubID}
            };
            return Connection.ExecuteNonQuery("UPDATE ComputerClub SET Name = @name, Address = @address, OpeningHours = @openingHours WHERE ClubID = @clubID", parameters);
        }

        static public bool Insert(string name, string address, string openingHours)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@name", name},
                {"@address", address},
                {"@openingHours", openingHours}
            };
            return Connection.ExecuteNonQuery("INSERT INTO ComputerClub (Name, Address, OpeningHours) VALUES (@name, @address, @openingHours)", parameters);
        }

        public bool Delete()
        {
            var parameters = new Dictionary<string, object>
            {
                {"@clubID", ClubID}
            };
            return Connection.ExecuteNonQuery("DELETE FROM ComputerClub WHERE ClubID = @clubID", parameters);
        }

        static public List<ComputerClub> GetAll()
        {
            List<ComputerClub> clubs = new List<ComputerClub>();
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
                        clubs.Add(new ComputerClub(reader.GetInt32("ClubID"), reader.GetString("Name"), reader.GetString("Address"), reader.GetString("OpeningHours")));
                    }
                }
            }
            return clubs;
        }
    }
}
