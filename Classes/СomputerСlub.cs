using Practical_lesson_No._28.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_work_28.Classes
{
    public class СomputerСlub : SqlMethods<СomputerСlub>
    {
        public int ID;
        public string Name;
        public string Address;
        public DateTime Time;

        public СomputerСlub()
        {

        }

        public СomputerСlub(int ID, string Name, string Address, DateTime Time)
        {
            this.ID = ID;
            this.Name = Name;
            this.Address = Address;
            this.Time = Time;
        }

        public void Update(params object[] args)
        {

        }

        public void Insert(params object[] args)
        {

        }

        public void Delete()
        {

        }

        public List<СomputerСlub> GetAll()
        {
            return new List<СomputerСlub>();
        }
    }
}
