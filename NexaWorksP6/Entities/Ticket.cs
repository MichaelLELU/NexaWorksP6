using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexaWorksP6.Entities
{

    public class Ticket
    {
        public int Id { get; set; }


        public int ProductId { get; set; }
        public int VersionId { get; set; }
        public int OperatingSystemId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }

        public int StatusId { get; set; }
        public string Problem { get; set; } = null!;
        public string? Resolution { get; set; }

        public Product Product { get; set; } = null!;
        public VersionEntity Version { get; set; } = null!;
        public OperatingSystemEntity OperatingSystem { get; set; } = null!;
        public Status Status { get; set; } = null!;
        public ProductVersionOs Pvo { get; set; } = null!;
    }

}
