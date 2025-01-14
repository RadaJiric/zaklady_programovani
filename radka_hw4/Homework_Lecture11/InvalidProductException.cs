using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Lecture11
{


    public class InvalidProductException : Exception
    {
        public InvalidProductException(string message) : base(message) { }
    }

}
