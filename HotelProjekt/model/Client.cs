using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class Client : HotelSystemElement
    {
        private string imię;
        public string Imię {
            get 
            {
                return imię;
            }
            set 
            {
                if (value.Equals(string.Empty)) throw new HotelSystemElementException("Pole imię nie może być puste");
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                imię = value;
            } 
        }
        private string nazwisko;
        public string Nazwisko 
        {
            get 
            {
                return nazwisko;
            }
            set 
            {
                if (value.Equals(string.Empty)) throw new HotelSystemElementException("Pole nazwisko nie może być puste");
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                nazwisko = value;
            }
        }
        private string email;
        public string Email 
        {
            get 
            {
                return email;
            }
            set 
            {
                if(!ValidateEmail(value)) throw new HotelSystemElementException("Podano nieprawidłowy adres e-mail");
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                email = value;
            }
        }
        private string pesel;
        public string Pesel 
        {
            get 
            {
                return pesel;
            }
            set 
            {
                if (!ValidatePesel(value)) throw new HotelSystemElementException("Podano nieprawidłowy numer pesel");
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                pesel = value;
            }
        }
        private string telefon;
        public string Telefon
        {
            get 
            {
                return telefon;
            }
            set 
            {
                if (!ValidateTelefon(value)) throw new HotelSystemElementException("Podano niewłaściwy numer telefonu");
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                telefon = value;
            }
        }
        private string numerKontaBankowego;
        public string NumerKontaBankowego 
        {
            get 
            {
                return numerKontaBankowego;
            }
            set 
            {
                if (!ValidateNrKonta(value)) throw new HotelSystemElementException("Podano niewłaściwy numer konta bankowego");
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                numerKontaBankowego = value;
            }
        }
        private string numerRejestracyjny;
        public string NumerRejestracyjny 
        {
            get 
            {
                return numerRejestracyjny;
            }
            set 
            {
                if (value.Equals(string.Empty)) 
                {
                    numerRejestracyjny = string.Empty;
                    return;
                }
                if (!ValidateNumerRejestracyjny(value)) throw new HotelSystemElementException("Podano niewłaściwy numer rejestracyjny pojazdu");
                if (this.InRepository) throw new HotelSystemElementException("Nie można zmieniać pól instancji ponieważ została ona dodana do repozytorium");
                numerRejestracyjny = value;
            }
        }
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

        private void Initialize(string imie, string nazwisko, string email, string pesel, string telefon, string numerKontaBankowego) 
        {
            this.Empty = false;
            this.Imię = imie;
            this.Nazwisko = nazwisko;
            this.Email = email;
            this.Pesel = pesel;
            this.Telefon = telefon;
            this.NumerKontaBankowego = numerKontaBankowego;
        }
        public Client(string imie, string nazwisko, string email, string pesel, string telefon, string numerKontaBankowego) 
        {
            Initialize(imie, nazwisko, email, pesel, telefon, numerKontaBankowego);
            this.NumerRejestracyjny = string.Empty;
        }
        public Client(string imie, string nazwisko, string email, string pesel, string telefon, string numerKontaBankowego, string numerRejestracyjny)
        {
            Initialize(imie, nazwisko, email, pesel, telefon, numerKontaBankowego);
            this.NumerRejestracyjny = numerRejestracyjny;
        }
        private static bool ValidateEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException) 
            {
                return false;
            }
        }
        private static bool ValidateNrKonta(string nrKonta)
        {
            nrKonta = nrKonta.Replace(" ", String.Empty);
            if (nrKonta.Length != 26 && nrKonta.Length != 32)
            {
                return false;
            }

            const int countryCode = 2521;
            string checkSum = nrKonta.Substring(0, 2);
            string accountNumber = nrKonta.Substring(2);
            string reversedDigits = accountNumber + countryCode + checkSum;
            return ModString(reversedDigits, 97) == 1;
        }

        private static int ModString(string x, int y)
        {
            if (string.IsNullOrEmpty(x))
            {
                return 0;
            }
            string firstDigit = x.Substring(0, x.Length - 1);
            int lastDigit = int.Parse(x.Substring(x.Length - 1));
            return (ModString(firstDigit, y) * 10 + lastDigit) % y;
        }
        private static bool ValidatePesel(string pesel) 
        {
            if (!(pesel.All(char.IsDigit) && pesel.Length == 11)) return false;
            int sum = 0;
            int weight = 1;
            for (int i = 0; i < pesel.Length - 1; i++) 
            {
                sum += int.Parse(pesel[i].ToString()) * weight;
                switch (weight) 
                {
                    case 1:
                        weight = 3;
                        break;
                    case 3:
                        weight = 7;
                        break;
                    case 7:
                        weight = 9;
                        break;
                    case 9:
                        weight = 1;
                        break;
                }
            }
            if (int.Parse(pesel[10].ToString()) == 10 - (sum % 10)) return true;
            return false;
        }
        private static bool ValidateTelefon(string telefon) 
        {
            if (telefon.All(char.IsDigit)) return true;
            return false;
        }
        private static bool ValidateNumerRejestracyjny(string numerRejestracyjny) 
        {
            Match match = Regex.Match(numerRejestracyjny, @"^[A-Z]{2,3} [A-Z0-9]{4,5}$");

            if (match.Success) 
            {
                return true;
            }
            return false;
        }
        private static (DateTime DataUrodzenia, bool Płeć) GetBirthDateAndGenderFromPesel(string pesel) 
        {
            int day, month, year;
            month = int.Parse(pesel.Substring(2, 2));

            int age = month / 20;
            string yearPrefix = string.Empty;

            switch (age) 
            {
                case 0:
                    yearPrefix = "19";
                    break;
                case 1:
                    yearPrefix = "20";
                    break;
                case 2:
                    yearPrefix = "21";
                    break;
                case 3:
                    yearPrefix = "22";
                    break;
                case 4:
                    yearPrefix = "18";
                    break;
            }

            year = int.Parse(yearPrefix + pesel.Substring(0, 2));
            month %= 20;
            day = int.Parse(pesel.Substring(4, 2));

            DateTime birthDate = DateTime.Parse(year + "/" + month + "/" + day);
            bool gender = int.Parse(pesel[9].ToString()) % 2 != 0 ? true : false;

            return (birthDate, gender);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Klient hotelu: " + Id.ToString());
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
