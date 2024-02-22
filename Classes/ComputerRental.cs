using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Practical_lesson_No._28.Classes;

namespace Practical_lesson_No._28.Classes
{
    public class ComputerRental
    {
        public int RentalID { get; set; }
        public DateTime DateAndTime { get; set; }
        public string ClientFullName { get; set; }
        public int ClubID { get; set; }

        public ComputerRental(int rentalID, DateTime dateAndTime, string clientFullName, int clubID)
        {
            RentalID = rentalID;
            DateAndTime = dateAndTime;
            ClientFullName = clientFullName;
            ClubID = clubID;
        }
        static public List<ComputerRental> GetAll()
        {
            List<ComputerRental> rentals = new List<ComputerRental>();
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM ComputerRental";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rentals.Add(new ComputerRental(reader.GetInt32("RentalID"), reader.GetDateTime("DateAndTime"), reader.GetString("ClientFullName"), reader.GetInt32("ClubID")));
                    }
                }
            }
            return rentals;
        }

        public bool Update(DateTime dateAndTime, string clientFullName, int clubID)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@dateAndTime", dateAndTime},
                {"@clientFullName", clientFullName},
                {"@clubID", clubID},
                {"@rentalID", this.RentalID}
            };
            return Connection.ExecuteNonQuery("UPDATE ComputerRental SET DateAndTime = @dateAndTime, ClientFullName = @clientFullName, ClubID = @clubID WHERE RentalID = @rentalID", parameters);
        }

        static public bool Insert(DateTime dateAndTime, string clientFullName, int clubID)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@dateAndTime", dateAndTime},
                {"@clientFullName", clientFullName},
                {"@clubID", clubID}
            };
            return Connection.ExecuteNonQuery("INSERT INTO ComputerRental (DateAndTime, ClientFullName, ClubID) VALUES (@dateAndTime, @clientFullName, @clubID)", parameters);
        }

        public bool Delete()
        {
            var parameters = new Dictionary<string, object>
            {
                {"@rentalID", this.RentalID}
            };
            return Connection.ExecuteNonQuery("DELETE FROM ComputerRental WHERE RentalID = @rentalID", parameters);
        }
    }
}
