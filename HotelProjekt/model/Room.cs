using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class Room : HotelSystemElement
    {
        private int nrPokoju;
        public int NrPokoju
        {
            get 
            {
                return nrPokoju;
            }
            set 
            {
                if (value <= 0) throw new HotelSystemElementException("Numer pokoju musi być większy od 0");
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                nrPokoju = value;
            }
        }
        private double cenaZaGodzinę;
        public double CenaZaGodzinę
        {
            get 
            {
                return cenaZaGodzinę;
            }
            set 
            {
                if (value <= 0) throw new HotelSystemElementException("Cena pokoju za godzinę musi być większa od 0");
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                cenaZaGodzinę = value;
            }
        }
        public Room(int nrPokoju, double cenaZaGodzinę) : base()
        {
            this.Empty = false;
            this.NrPokoju = nrPokoju;
            this.CenaZaGodzinę = cenaZaGodzinę;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Pokój: " + Id.ToString());
            sb.AppendLine("Numer: " + this.NrPokoju);
            sb.AppendLine("Cena za godzinę: " + this.CenaZaGodzinę);
            return sb.ToString();
        }
    }
}
