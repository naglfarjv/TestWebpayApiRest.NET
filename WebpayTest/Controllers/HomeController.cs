using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transbank.Webpay.WebpayPlus;


namespace WebpayTest.Controllers
{
    public class HomeController : Controller
    {



        public ActionResult Index()
        {
            var amount = new Random().Next(350, 200000);
            var buyOrder = new Random().Next(100000, 999999999).ToString();
            var sessionId = "sessionId";

            


            String httpHost = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();

            string returnUrl = "http://"+httpHost+"/home/Return";

            //string returnUrl = "http://localhost:50853/home/Return";

            var response = Transaction.Create(buyOrder, sessionId, amount, returnUrl);
            
            ViewBag.Amount = amount;
            ViewBag.BuyOrder = buyOrder;
            ViewBag.ReponseURL = response.Url;
            ViewBag.ResponseToken = response.Token;
            ViewBag.SessionId = sessionId;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Return()
        {

            string token = Request.Form["token_ws"];
            var result = Transaction.Commit(token);

            ViewBag.Token = token;
            ViewBag.Result = result;
            ViewBag.SaveToken = token;


            return View();
        }

        
    }
}