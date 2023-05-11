using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitiy.Concrete.Dtos
{
    public class ShippingDetail
    {


        //[Required]
        public string FirstName { get; set; }
        //[Required]
        public string LastName { get; set; }
        //[Required]
        public string ShippingAddress { get; set; }
        //[Required]
        public string City { get; set; }
        //[Required]
        public string Email { get; set; }

        public int ShippingId { get; set; }
    }
}
