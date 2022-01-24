using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class Client : HotelSystemElement
    {
        public string Imię { get; private set; }
        public string Nazwisko { get; private set; }
        public string Email { get; private set; }
        public string Pesel { get; private set; }
        public string Telefon { get; private set; }
        public string NumerRejestracyjny { get; private set; }
        public DateTime DataUrodzenia 
        {
            get 
            {
                return GetBirthDateAndGenderFromPesel(Pesel).DataUrodzenia;
            }
        }
        public bool Płeć 
        {
            get
            {
                return GetBirthDateAndGenderFromPesel(Pesel).Płeć;
            }
        }
        public bool CzyPosiadaPojazd 
        {
            get 
            {
                if (NumerRejestracyjny.Equals(string.Empty))
                {
                    return false;
                }
                else return true;
            }
        }

        private void Initialize(string imie, string nazwisko, string email, string pesel, string telefon) 
        {
            this.Empty = false;
            if (imie.Equals(string.Empty)) 
            {
                throw new HotelSystemElementException("Pole imię nie może być puste");
            }
            this.Imię = imie;
            if (nazwisko.Equals(string.Empty)) 
            {
                throw new HotelSystemElementException("Pole nazwisko nie może być puste");
            }
            this.Nazwisko = nazwisko;
            if (!ValidateEmail(email)) 
            {
                throw new HotelSystemElementException("Podano nieprawidłowy adres e-mail");
            }
            this.Email = email;
            if (!ValidatePesel(pesel)) 
            {
                throw new HotelSystemElementException("Podano nieprawidłowy numer pesel");
            }
            this.Pesel = pesel;
            if (!ValidateTelefon(telefon)) 
            {
                throw new HotelSystemElementException("Podano niewłaściwy numer telefonu");
            }
            this.Telefon = telefon;
        }
        public Client(string imie, string nazwisko, string email, string pesel, string telefon) 
        {
            Initialize(imie, nazwisko, email, pesel, telefon);
            this.NumerRejestracyjny = string.Empty;
        }
        public Client(string imie, string nazwisko, string email, string pesel, string telefon, string numerRejestracyjny)
        {
            Initialize(imie, nazwisko, email, pesel, telefon);
            if (!ValidateNumerRejestracyjny(numerRejestracyjny)) 
            {
                throw new HotelSystemElementException("Podano niewłaściwy numer rejestracyjny pojazdu");
            }
            this.NumerRejestracyjny = numerRejestracyjny;
        }
        private static bool ValidateEmail(string email) 
        {
            return true;
        }
        private static bool ValidatePesel(string pesel) 
        {
            return true;
        }
        private static bool ValidateTelefon(string telefon) 
        {
            return true;
        }
        private static bool ValidateNumerRejestracyjny(string numerRejestracyjny) 
        {
            return true;
        }
        private static (DateTime DataUrodzenia, bool Płeć) GetBirthDateAndGenderFromPesel(string pesel) 
        {
            return (DateTime.Now, false);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Klient hotelu: " + id.ToString());
            sb.AppendLine("Imię: " + this.Imię);
            sb.AppendLine("Nazwisko: " + this.Nazwisko);
            sb.AppendLine("Pesel: " + this.Pesel);
            sb.AppendLine("E-mail: " + this.Email);
            sb.AppendLine("Telefon: " + this.Telefon);
            sb.AppendLine("Płeć: " + (this.Płeć ? "Mężczyzna" : "Kobieta"));
            sb.AppendLine("Data urodzenia: " + this.DataUrodzenia.ToString().Split(' ')[0]);
            if (this.CzyPosiadaPojazd) 
            {
                sb.AppendLine("Numer rejestracyjny pojazdu: " + this.NumerRejestracyjny);
            }
            return sb.ToString();
        }
    }
}
