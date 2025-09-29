using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NexaWorksP6.Entities
{

        public class VersionEntity
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public int ProductId { get; set; }
            public  Product Product { get; set; } = null!;
            public ICollection<ProductVersionOs> ProductVersionOs { get; set; } = new List<ProductVersionOs>();

        }

}
