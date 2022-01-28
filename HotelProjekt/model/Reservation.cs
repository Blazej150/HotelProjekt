using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class Reservation : HotelSystemElement
    {
        private const double DISCOUNT_MULTIPLIER = 0.75;
        private const double OPŁATA_ZA_PARKING = 1.0;
        private DateTime dataRezerwacji;
        public DateTime DataRezerwacji 
        {
            get 
            {
                return dataRezerwacji;
            }
            set 
            {
                if (DateTime.Compare(value, DateTime.Now) < 0) throw new HotelSystemElementException("Data rezerwacji nie może być datą przeszłą");
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                dataRezerwacji = value;
            }
        }
        private DateTime dataWymeldowania;
        public DateTime DataWymeldowania 
        {
            get 
            {
                return dataWymeldowania;
            }
            set 
            {
                if (DateTime.Compare(value, this.DataRezerwacji) < 0) throw new HotelSystemElementException("Data wymeldownaia nie może być wcześniejsza od daty rezerwacji");
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                dataWymeldowania = value;
            }
        }
        private Client klient;
        public Client Klient 
        {
            get 
            {
                return klient;
            }
            set 
            {
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                if (value == null) 
                {
                    throw new ArgumentNullException();
                }
                klient = value;
            }
        }
        private Room pokój;
        public Room Pokój 
        {
            get 
            {
                return pokój;
            }
            set 
            {
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                pokój = value;
            }
        }
        public double CenaZaRezerwację
        {
            get 
            {
                double multiplier = KodPromocyjny.Equals(string.Empty) ? 1.0 : DISCOUNT_MULTIPLIER;
                double cenaZaGodzinę = this.Pokój.CenaZaGodzinę;
                double cenaZaParking = this.Klient.CzyPosiadaPojazd ? OPŁATA_ZA_PARKING : 0;
                return ((cenaZaGodzinę+cenaZaParking) * SumaGodzin) * multiplier;
            }
        }
        public int SumaGodzin 
        {
            get 
            {
                TimeSpan ts = DataWymeldowania - DataRezerwacji;
                return (int)Math.Ceiling(ts.TotalHours);
            }
        }
        private string kodPromocyjny;
        public string KodPromocyjny 
        {
            get 
            {
                return kodPromocyjny;
            }
            set 
            {
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                foreach (AvailableDiscountCodes code in Enum.GetValues(typeof(AvailableDiscountCodes)))
                {
                    if (value.Equals(code.ToString())) 
                    {
                        kodPromocyjny = code.ToString();
                        return;
                    }
                }
                throw new HotelSystemElementException("Podano nieprawidłowy kod promocyjny");
            }
        }
        private void Initialize(DateTime dataRezerwacji, DateTime dataWymeldowania)
        {
            this.Empty = false;
            this.DataRezerwacji = dataRezerwacji;
            this.DataWymeldowania = dataWymeldowania;
        }
        public Reservation(DateTime dataRezerwacji, DateTime dataWymeldowania) : base() 
        {
            Initialize(dataRezerwacji, dataWymeldowania);
        }

        public Reservation(DateTime dataRezerwacji, DateTime dataWymeldownaia, string kodPromocyjny) : base() 
        {
            Initialize(dataRezerwacji, dataWymeldowania);
            this.KodPromocyjny = kodPromocyjny;
        }
    }
}
