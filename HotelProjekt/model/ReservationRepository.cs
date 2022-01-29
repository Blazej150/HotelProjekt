using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class ReservationRepository : Repository
    {
        public ReservationRepository() : base() { }

        protected override bool AvoidDuplicates(HotelSystemElement element)
        {
            Reservation reservation = element as Reservation;
            foreach (HotelSystemElement e in this.elements)
            {
                Reservation res_rep = e as Reservation;
                if (res_rep.DataRezerwacji.Equals(res_rep.DataRezerwacji) || res_rep.DataWymeldowania.Equals(res_rep.DataWymeldowania))
                {
                    return true;
                }
            }
            return false;
        }

        public override Guid Dodaj(HotelSystemElement element)
        {
            if (element is Reservation)
            {
                if (AvoidDuplicates(element)) throw new HotelSystemRepositoryException("Ten element jest duplikatem i nie może zostać dodany do repozytorium");
                Guid id = element.Id;
                element.AddToRepository();
                this.elements.Add(element);
                return id;
            }
            else
            {
                throw new HotelSystemRepositoryException("Ten element nie jest klasą Client");
            }
        }

        public override string FullInfo(string title = null)
        {
            return base.FullInfo("Repozytorium rezerwacji hotelu");
        }
    }
}
