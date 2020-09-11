using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceChatBotApi.Models
{
    public class ServiceResponse
    {
        public string Status { get; set; }        
        public object Response { get; set; }
        public string ErrorMessage { get; set; }        
    }
}