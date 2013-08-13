using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Data.Core
{
    public interface IIdentifier
    {
        int Id { get; set; }

        Guid GUID { get; set; }

        byte[] RowVersion { get; set; }
    }
}
