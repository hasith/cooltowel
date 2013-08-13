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
        public int Id { get; set; }
        public Guid GUID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public string Description { get; set; }
        public string Name { get; set; }
    }
}
