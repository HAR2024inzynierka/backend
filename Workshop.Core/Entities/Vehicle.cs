using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Entities
{
    [Table("Pojazdy")]
    public class Vehicle
    {
        public int Id { get; set; }
        [Column("Marka")]
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        [Column("NrRejestracujny")]
        public string RegistrationNumber { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
