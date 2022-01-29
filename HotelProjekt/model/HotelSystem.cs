using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjekt.model
{
    public class HotelSystem
    {
        private Repository clientRepo, roomRepo, reservationRepo;
        public HotelSystem() 
        {
            clientRepo = new ClientRepository();
            roomRepo = new RoomRepository();
            reservationRepo = new ReservationRepository();
        }
        public Guid AddClient(Client client) 
        {
            return clientRepo.Dodaj(client);
        }
        public void RemoveClient(Guid id) 
        {
            clientRepo.Usuń(id);
        }
        public Client findClient(Guid id) 
        {
            return (Client)clientRepo.ZnajdźElement(id);
        }

        public Guid AddRoom(Room room)
        {
            return roomRepo.Dodaj(room);
        }
        public void RemoveRoom(Guid id)
        {
            roomRepo.Usuń(id);
        }
        public Room findRoom(Guid id)
        {
            return (Room)roomRepo.ZnajdźElement(id);
        }

        public Guid AddReservation(Reservation reservation)
        {
            return reservationRepo.Dodaj(reservation);
        }
        public void RemoveReservation(Guid id)
        {
            reservationRepo.Usuń(id);
        }
        public Reservation findReservation(Guid id)
        {
            return (Reservation)reservationRepo.ZnajdźElement(id);
        }


    }
}
