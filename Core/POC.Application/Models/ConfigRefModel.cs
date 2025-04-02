using POC.Domain.Entities;
using POC.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace POC.Application.Models
{
    public class ConfigRef_Search : BaseSearchEntity
    {
        public string Ref_Type { get; set; }
        public string Ref_Param { get; set; }
    }

    public class ConfigRef_Request : BaseEntity
    {
        public string Ref_Type { get; set; }
        public string Ref_Param { get; set; }
        public string Ref_Value1 { get; set; }
        public string Ref_Value2 { get; set; }
        public string Ref_Value3 { get; set; }
        public string Ref_Value4 { get; set; }
        public string Ref_Value5 { get; set; }
        public string Ref_Value6 { get; set; }
        public string Ref_Value7 { get; set; }
        public string Ref_Value8 { get; set; }
        public string Ref_Value9 { get; set; }
        public string Ref_Value10 { get; set; }
    }

    public class ConfigRef_Response : BaseResponseEntity
    {
        public string Ref_Type { get; set; }
        public string Ref_Param { get; set; }
        public string Ref_Value1 { get; set; }
        public string Ref_Value2 { get; set; }
        public string Ref_Value3 { get; set; }
        public string Ref_Value4 { get; set; }
        public string Ref_Value5 { get; set; }
        public string Ref_Value6 { get; set; }
        public string Ref_Value7 { get; set; }
        public string Ref_Value8 { get; set; }
        public string Ref_Value9 { get; set; }
        public string Ref_Value10 { get; set; }
    }
}
