using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public abstract class Repository
    {
        protected List<HotelSystemElement> elements;

        public Repository() 
        {
            elements = new List<HotelSystemElement>();

        }
        protected abstract bool AvoidDuplicates(HotelSystemElement element);
        public abstract Guid Dodaj(HotelSystemElement element);
        public void Usuń(Guid id)
        {
            HotelSystemElement element = ZnajdźElement(id);
            if (element.Empty != true)
            {
                element.RemoveFromRepository();
                this.elements.Remove(element);
            }
            else
            {
                throw new HotelSystemRepositoryException("Taki element nie istnieje w danym repozytorium");
            }
        }
        public HotelSystemElement ZnajdźElement(Guid id)
        {
            foreach (HotelSystemElement element in this.elements)
            {
                if (element.Id.Equals(id))
                {
                    return element;
                }
            }
            return HotelSystemElement.CreateEmpty();
        }
        public virtual string FullInfo(string title) 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-----------"+title+"------------");
            foreach (HotelSystemElement element in this.elements)
            {
                sb.AppendLine(element.ToString());
            }
            sb.AppendLine("---------------------------------------------------");
            return sb.ToString();
        }
    }
}
