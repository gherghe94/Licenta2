using Licenta.DataLayer.SqlDb.Implementation;
using Licenta.DataLayer.SqlDb.Interfaces;
using Licenta.Domain.Models;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Implementation
{
    public class RoomService : Service<Room, IRoomRepository>, IRoomService
    {
        public RoomService()
        {
            base.repository = new RoomRepository();
        }

        public bool IsUnique(Room room)
        {
            List<Room> allRooms = GetAll();
            allRooms.RemoveAll(q=>q.Id == room.Id);
            return !allRooms.Any(r => r.Name == room.Name);
        }
    }
}
