using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMvc.ViewModel;

namespace WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private static string WebApiUrl = "http://localhost:28278/";
        // GET: Home
        public async Task<ActionResult> Index()
        {
            var tokenBased = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebApiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = await client.GetAsync("Account/validlogin?userName=admin&userPassword=admin");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;

                    tokenBased = JsonConvert.DeserializeObject<string>(resultMessage);

                    //dynamic value = JsonConvert.DeserializeObject(resultMessage);
                    //tokenBased = value.Message;

                    Session["TokenNumber"] = tokenBased;
                    Session["UserName"] = "admin";
                }
            }
            //return Content(tokenBased);
            return View();
        }

        public async Task<ActionResult> GetEmployee()
        {
            string returnMessage = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebApiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["TokenNumber"].ToString() + ":" + Session["UserName"]);
                var responseMessage = await client.GetAsync("Account/GetEmployee");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;

                    returnMessage = JsonConvert.DeserializeObject<string>(resultMessage);
                }
            }
            return Content(returnMessage);
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel objContactViewModel)
        {

            return Json(new { success = true, message = "Your request has been successfully added,." }, JsonRequestBehavior.AllowGet);
        }
    }
}