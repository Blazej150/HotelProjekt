using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class HotelSystemElement
    {
        public bool InRepository 
        {
            get; protected set;
        }
        public bool Empty { get; protected set; }
        public Guid Id { get; protected set; }
        public HotelSystemElement() 
        {
            Id = Guid.NewGuid();
            InRepository = false;
        }
        public void AddToRepository() 
        {
            this.InRepository = true;
        }
        public static HotelSystemElement CreateEmpty() 
        {
            HotelSystemElement hotelSystemElement = new HotelSystemElement();
            hotelSystemElement.Empty = true;
            hotelSystemElement.InRepository = false;
            return hotelSystemElement;
        }
    }
}
