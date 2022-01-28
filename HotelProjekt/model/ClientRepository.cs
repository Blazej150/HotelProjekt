using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class ClientRepository : Repository
    {
        public ClientRepository() : base() { }

        protected override bool AvoidDuplicates(HotelSystemElement element) 
        {
            Client client = element as Client;
            foreach (HotelSystemElement e in this.elements) 
            {
                Client c = e as Client;
                if (c.Pesel.Equals(client.Pesel) || c.Email.Equals(client.Email) || c.Telefon.Equals(client.Telefon)) 
                {
                    return true;
                }
            }
            return false;
        }

        public override Guid Dodaj(HotelSystemElement element)
        {
            if (element is Client)
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
            return base.FullInfo("Repozytorium klientów hotelu");
        }
    }
}
