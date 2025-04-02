using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace POC.Domain.Entities
{
    public class BasePaninationEntity
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        [JsonIgnore]
        public int Total { get; set; }
    }
}
