using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class HotelSystemElementException : Exception
    {
        public HotelSystemElementException(string message) : base(message) { }
    }
}
