using Practical_lesson_No._28.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_work_28.Classes
{
    class ComputerRental : SqlMethods<ComputerRental>
    {

        public List<ComputerRental> GetAll()
        {
            return new List<ComputerRental>();
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
    }
}
