using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NexaWorksP6.Entities
{

        public class OperatingSystemEntity
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public ICollection<ProductVersionOs> Compatibilities { get; set; } = new List<ProductVersionOs>();
            public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        }

}
