using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practical_lesson_No._28.Interfaces
{
    interface SqlMethods<T>
    {
        List<T> GetAll();
        void Update(params object[] args);
        void Insert(params object[] args);
        void Delete();
    }
}
