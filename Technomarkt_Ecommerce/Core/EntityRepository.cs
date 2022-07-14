using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class EntityRepository : IEntity<Guid>
    {

        public EntityRepository()
        {
            Status = Status.Active;
            CreatedDate = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            CreatedComputerName = Environment.MachineName;
            CreatedIP = "192.168.31.146";//Use a method to get ip later!
            CreatedAdUsername = "Admin";
            CreatedBy = "Admin";
        }

        public Guid ID { get; set; }
        public Status Status { get; set; }


        public string CreatedDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }
        public string CreatedAdUsername { get; set; }
        public string CreatedBy { get; set; }


        public string UpdatedDate { get; set; }
        public string UpdatedComputerName { get; set; }
        public string UpdatedIP { get; set; }
        public string UpdatedAdUsername { get; set; }
        public string UpdatedBy { get; set; }


    }
}
