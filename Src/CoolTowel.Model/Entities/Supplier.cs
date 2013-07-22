using CoolTowel.Data.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Model
{
    public class Supplier: IIdentifier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid GUID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; } 
    }
}
