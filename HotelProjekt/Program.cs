using HotelProjekt.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client1 = new Client("Jacek","Pawłowski","jacek123@gmail.com","00242602118","768199288", "89 2490 0005 0000 4530 6240 7892");
            Client client2 = new Client("Marek", "Kondrad", "marek123@gmail.com", "00242602118", "768199288", "89 2490 0005 0000 4530 6240 7892");

            ClientRepository repo = new ClientRepository();
            repo.Dodaj(client1);
            repo.Dodaj(client2);

            Console.ReadKey();
        }
    }
}
