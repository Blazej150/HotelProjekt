using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class HotelSystemElement
    {
        public bool Empty { get; protected set; }
        protected Guid id;
        public HotelSystemElement() 
        {
            id = Guid.NewGuid();
        }
        public static HotelSystemElement CreateEmpty() 
        {
            HotelSystemElement hotelSystemElement = new HotelSystemElement();
            hotelSystemElement.Empty = true;
            return hotelSystemElement;
        }
    }
}
