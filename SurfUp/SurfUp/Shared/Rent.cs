using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfUp.Shared
{
    
    public class Rent
    {
        [Key]
        public int RentId { get; set; }
        public int BoardId{ get; set; }

        public DateTime StartRent { get; set; }

        public DateTime EndRent { get; set; }
    }
}
