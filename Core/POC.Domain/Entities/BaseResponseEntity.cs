using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace POC.Domain.Entities
{
    public class BaseResponseEntity : BaseEntity
    {
        //public string CreatorName { get; set; }
        //public long CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        //public string ModifierName { get; set; }
        //public long ModifiedBy { get; set; }

        [DefaultValue(null)]
        public DateTime? ModifiedDate { get; set; }
    }
}
