using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JobType : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual Company? Company { get; set; }
        public virtual IList<Job> Jobs { get; set; } = new List<Job>();
    }
}
