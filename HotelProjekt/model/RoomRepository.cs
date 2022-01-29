using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class RoomRepository : Repository
    {
        public RoomRepository() : base() { }

        protected override bool AvoidDuplicates(HotelSystemElement element)
        {
            Room room = element as Room;
            foreach (HotelSystemElement e in this.elements)
            {
                Room r = e as Room;
                if (r.NrPokoju.Equals(room.NrPokoju))
                {
                    return true;
                }
            }
            return false;
        }

        public override Guid Dodaj(HotelSystemElement element)
        {
            if (element is Room)
            {
                if (AvoidDuplicates(element)) throw new HotelSystemRepositoryException("Ten element jest duplikatem i nie może zostać dodany do repozytorium");
                Guid id = element.Id;
                element.AddToRepository();
                this.elements.Add(element);
                return id;
            }
            else
            {
                throw new HotelSystemRepositoryException("Ten element nie jest klasą Room");
            }
        }

        public override string FullInfo(string title = null)
        {
            return base.FullInfo("Repozytorium pokojów hotelu");
        }
    }
}
