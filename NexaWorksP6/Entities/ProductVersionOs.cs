using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NexaWorksP6.Entities
{
    public class ProductVersionOs
    {
        public int ProductId { get; set; }
        public int VersionId { get; set; }
        public int OperatingSystemId { get; set; }

        public Product Product { get; set; } = null!;
        public VersionEntity Version { get; set; } = null!;
        public OperatingSystemEntity OperatingSystem { get; set; } = null!;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
