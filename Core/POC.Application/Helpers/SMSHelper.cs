using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Data;
using POC.Application.Models;
using Newtonsoft.Json;
using POC.Application.Interfaces;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Text.Json.Nodes;

namespace POC.Helpers
{
    public interface ISMSHelper
    {
        string SMSSend_SteviaDigital(string MobileNumber, string Message);
    }

    public class SMSHelper : ISMSHelper
    {
        public string SMSSend_SteviaDigital(string MobileNumber, string Message)
        {
            throw new NotImplementedException();
        }
    }
}