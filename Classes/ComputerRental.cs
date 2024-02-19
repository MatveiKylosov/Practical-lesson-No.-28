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

        public void Update(DateTime dateAndTime, string clientFullName, int clubID)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE ComputerRental SET DateAndTime = @dateAndTime, ClientFullName = @clientFullName, ClubID = @clubID WHERE RentalID = @rentalID";
                cmd.Parameters.AddWithValue("@dateAndTime", dateAndTime);
                cmd.Parameters.AddWithValue("@clientFullName", clientFullName);
                cmd.Parameters.AddWithValue("@clubID", clubID);
                cmd.Parameters.AddWithValue("@rentalID", this.RentalID);
                cmd.ExecuteNonQuery();
            }
        }

        static public void Insert(DateTime dateAndTime, string clientFullName, int clubID)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO ComputerRental (DateAndTime, ClientFullName, ClubID) VALUES (@dateAndTime, @clientFullName, @clubID)";
                cmd.Parameters.AddWithValue("@dateAndTime", dateAndTime);
                cmd.Parameters.AddWithValue("@clientFullName", clientFullName);
                cmd.Parameters.AddWithValue("@clubID", clubID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete()
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM ComputerRental WHERE RentalID = @rentalID";
                cmd.Parameters.AddWithValue("@rentalID", this.RentalID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
