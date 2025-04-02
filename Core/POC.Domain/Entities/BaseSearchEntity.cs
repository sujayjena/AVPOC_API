using POC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Persistence.Repositories
{
    public class BaseSearchEntity : BasePaninationEntity
    {
        [DefaultValue("")]
        public string? SearchText { get; set; }

        [DefaultValue(null)]
        public bool? IsActive { get; set; }
    }
}
