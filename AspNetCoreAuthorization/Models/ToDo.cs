using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthorization.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
