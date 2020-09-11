using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ECommerceChatBotApi.Models;
using Microsoft.Bot.Schema;

namespace ECommerceChatBotApi.Controllers
{
    [RoutePrefix("api/ecommercechatbot")]
    public class ECommerceChatBotController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("getwelcomemessage")]
        public IHttpActionResult GetWelcomeMessage()
        {
            ServiceResponse serviceResponse = new ServiceResponse();

            HeroCard heroCard = new HeroCard();

            heroCard.Title = "Welcome to ECommerce Chat Bot";
            heroCard.Text = "Hiii ! How can i assist you";
            heroCard.Buttons = new List<CardAction>
            {
                new CardAction{ Type= "imBack", Title= "Top rated fridge brands", Value= "Top rated fridge brands" },
                new CardAction{ Type= "imBack", Title= "Benefits of smart fridge", Value= "Benefits of smart fridge" },
                new CardAction{ Type= "imBack", Title= "Reason to purchase fridge", Value= "Reason to purchase fridge" },
                new CardAction{ Type= "imBack", Title= "Open bank account", Value= "Open bank account" }
            };

            serviceResponse.Status = "Success";
            serviceResponse.Response = heroCard;

            return Ok(serviceResponse);
        }

        [Route("getbranddetails")]
        public IHttpActionResult GetBrandDetails(string productName)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            BrandDetails brandDetails = new BrandDetails();

            switch (productName.Trim().ToLower())
            {
                case "fridge":
                case "refrigerator":
                    brandDetails.LayoutType = "carousel";
                    brandDetails.Attachments = GetFridgeBrandDetails();
                    break;
                default:
                    break;
            }
            serviceResponse.Status = "Success";
            serviceResponse.Response = brandDetails;

            return Ok(serviceResponse);
        }

        public List<HeroCard> GetFridgeBrandDetails()
        {
            List<HeroCard> heroCard = new List<HeroCard>() {
                new HeroCard(){
                    Buttons = new List<CardAction> {
                        new CardAction { Type="imBack", Title="Godrej",Text ="",Value="Godrej", Image="" }
                    },
                    Images = new List<CardImage>(){
                        new CardImage(){ Url = "https://images.financialexpress.com/2019/06/godrej-1.jpg", Alt="Godrej"}
                    }
                },

                new HeroCard(){
                    Buttons = new List<CardAction> {
                        new CardAction { Type="imBack", Title="Whirlpool",Text ="",Value="Whirlpool", Image="" }
                    },
                    Images = new List<CardImage>(){
                        new CardImage(){ Url = "https://png.pngitem.com/pimgs/s/47-473442_whirlpool-logo-hd-png-download.png", Alt="Whirlpool"}
                    }
                },

                new HeroCard(){
                    Buttons = new List<CardAction> {
                        new CardAction { Type="imBack", Title="Panasonic",Text ="",Value="Panasonic", Image="" }
                    },
                    Images = new List<CardImage>(){
                        new CardImage(){ Url = "https://png.pngitem.com/pimgs/s/481-4819396_panasonic-logo-png-download-logo-panasonic-png-transparent.png", Alt="Panasonic"}
                    }
                },

                new HeroCard(){
                    Buttons = new List<CardAction> {
                        new CardAction { Type="imBack", Title="LG",Text ="",Value="LG", Image="" }
                    },
                    Images = new List<CardImage>(){
                        new CardImage(){ Url = "https://png.pngitem.com/pimgs/s/99-997913_lg-logo-png-lg-logo-png-2017-transparent.png", Alt="LG"}
                    }
                },
                new HeroCard(){
                    Buttons = new List<CardAction> {
                        new CardAction { Type="imBack", Title="Samsung",Text ="",Value="Samsung", Image="" }
                    },
                    Images = new List<CardImage>(){
                        new CardImage(){ Url = "https://png.pngitem.com/pimgs/s/2-24638_samsung-logo-png-transparent-vector-samsung-logo-png.png", Alt="Samsung"}
                    }
                }
            };

            return heroCard;
        }

        [Route("getspecificbranddetails")]
        public IHttpActionResult GetSpecificBrandDetails(string brandName)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            HeroCard heroCard = new HeroCard();

            switch (brandName.Trim().ToLower())
            {
                case "godrej":
                    heroCard = GetIndividualBrandDetails(brandName, "Features\n  1. Cool Shower Technology.\n  2. Music, FM and Mobile Charging Refrigerator.\n  3. Turbo Chill Mode for Faster Cooling.", "https://mondaldistribution.com/wp-content/uploads/2020/04/EPRO205TAI5.2-Purple-Black-190-Ltr.png");
                    break;
                case "whirlpool":
                    heroCard = GetIndividualBrandDetails(brandName, "Features\n  1. Best Top Freezer refrigerator.\n  2. Best Apartment Size French Door Refrigerator.\n  3. Best 4-Door French Door Refrigerator.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSZmWw8l5XuNtdm6MNyKFdQ4o0R2pAYbLuR3CPYstejDfQwNAI&s");
                    break;
                case "panasonic":
                    heroCard = GetIndividualBrandDetails(brandName, "Features\n  1. Speed Chilling.\n  2. Five-Door Configurations.\n  3. Giant Touch Screens With Digital Smarts.", "https://www.world-import.com/images/P/Panasonic-NR-D513.png");
                    break;
                case "lg":
                    heroCard = GetIndividualBrandDetails(brandName, "Features\n  1. Smart Inverter Compressor.\n  2. 4 Star : For Energy savings up to 45%.\n  3. Toughened Glass Shelves.", "https://mondaldistribution.com/wp-content/uploads/2020/06/302rspn.png");
                    break;
                case "samsung":
                    heroCard = GetIndividualBrandDetails(brandName, "Features\n  1. Digital Inverter Compressor.\n  2. Keep food cool for atleast 8 hours when power is switched off.\n  3. Easy Slide out shelf to make you to access your food items conveniently.", "https://mondaldistribution.com/wp-content/uploads/2020/04/SAMSUNG-RR22T383XCU-HL.CAMELLIA-BLUE.png");
                    break;
                default:
                    heroCard = new HeroCard() { Title = "The product you are looking " + brandName + " is not avialable. Can you please try again with different product." };
                    break;
            }

            serviceResponse.Status = "Success";
            serviceResponse.Response = heroCard;

            return Ok(serviceResponse);
        }

        public HeroCard GetIndividualBrandDetails(string brandName, string brandFeatures, string brandImagePath)
        {
            HeroCard heroCard = new HeroCard();
            heroCard.Title = brandName.ToUpper();
            heroCard.Text = brandFeatures;
            heroCard.Images = new List<CardImage>
            {
                new CardImage(){ Url=brandImagePath, Alt=brandName }
            };

            return heroCard;
        }

        [Route("getspecificproductfeatures")]
        public IHttpActionResult GetSpecificProductFeatures(string productName)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            HeroCard heroCard = new HeroCard();

            switch (productName.Trim().ToLower())
            {
                case "fridge":
                case "refrigerator":
                    heroCard = GetIndividualProductFeatures("Features of smart fridge"
                                                            , "1. Coordinate schedules for every member of the family." +
                                                               "\n 2. Look up recipes and have your fridge read the steps while you cook." +
                                                               "\n 3. Create grocery lists that sync to your smartphone in real-time." +
                                                               "\n 4. Set expiration dates and receive notifications to use food while it’s fresh." +
                                                               "\n 5. Upload photos for display." +
                                                               "\n 6. Create individual profiles for each family member to send them persona notes and to-do lists." +
                                                               "\n 7. Use a whiteboard option to leave messages for your family." +
                                                               "\n 8. Customize temperature by drawer or compartment." +
                                                               "\n 9. Turn the ice maker on or off from your smartphone."
                                                             , new List<string> { "Top rated fridge brands", "Reason to purchase fridge" }
                                                             );
                    break;
                default:                    
                    break;
            }

            serviceResponse.Status = "Success";
            serviceResponse.Response = heroCard;

            return Ok(serviceResponse);
        }

        public HeroCard GetIndividualProductFeatures(string title, string text, List<string> suggestedActions)
        {
            HeroCard heroCard = new HeroCard();

            heroCard.Title = title;
            heroCard.Text = text;
            List<CardAction> cardActions = new List<CardAction>();

            for (int i = 0; i < suggestedActions.Count; i++)
            {
                cardActions.Add(new CardAction { Type = "imBack", Title = suggestedActions[i], Value = suggestedActions[i] });
            }

            heroCard.Buttons = cardActions;

            return heroCard;
        }

        [Route("getspecificproductreasons")]
        public IHttpActionResult GetSpecificProductReasons(string productName)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            HeroCard heroCard = new HeroCard();

            switch (productName.Trim().ToLower())
            {
                case "fridge":
                case "refrigerator":
                    heroCard = GetIndividualProductReasons("Reason should purchase fridge"
                                                            , "Fridge helps to slows bacterial growth. Bacteria exist everywhere in nature. They are in the soil, air, water, and the foods we eat." +
                                                              "\n When they have nutrients (food), moisture, and favorable temperatures, they grow rapidly, increasing in numbers to the point where some types of bacteria can cause illness."
                                                            , new List<string> { "Top rated fridge brands", "Benefits of smart fridge" }
                                                            );
                    break;
                default:                    
                    break;
            }

            serviceResponse.Status = "Success";
            serviceResponse.Response = heroCard;

            return Ok(serviceResponse);
        }

        public HeroCard GetIndividualProductReasons(string title, string text, List<string> suggestedActions)
        {
            HeroCard heroCard = new HeroCard();

            heroCard.Title = title;
            heroCard.Text = text;
            List<CardAction> cardActions = new List<CardAction>();

            for (int i = 0; i < suggestedActions.Count; i++)
            {
                cardActions.Add(new CardAction { Type = "imBack", Title = suggestedActions[i], Value = suggestedActions[i] });
            }

            heroCard.Buttons = cardActions;

            return heroCard;
        }




    }
}