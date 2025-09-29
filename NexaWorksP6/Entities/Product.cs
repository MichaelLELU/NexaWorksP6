using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexaWorksP6.Entities
{

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;

        public ICollection<VersionEntity> Versions { get; set; } = new List<VersionEntity>();
        public ICollection<ProductVersionOs> ProductVersionOs { get; set; } = new List<ProductVersionOs>();
    }
}
