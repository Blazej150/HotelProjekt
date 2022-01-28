using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class HotelSystemRepositoryException : Exception
    {
        public HotelSystemRepositoryException(string message) : base(message) { }
    }
}
