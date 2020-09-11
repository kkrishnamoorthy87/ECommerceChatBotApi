using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceChatBotApi.Models
{
    public class BrandDetails
    {
        public string LayoutType { get; set; }

        public List<HeroCard> Attachments { get; set; }
        public BrandDetails()
        {
            LayoutType = string.Empty;
            Attachments = new List<HeroCard>();
        }
    }
}