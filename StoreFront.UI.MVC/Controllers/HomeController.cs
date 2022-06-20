using Microsoft.AspNetCore.Mvc;
using StoreFront.UI.MVC.Models;
using System.Diagnostics;
using MimeKit;
using MailKit.Net.Smtp;


namespace StoreFront.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        ////Email Credentials
        //private readonly IConfiguration _config;
        //public HomeController(IConfiguration config, ILogger<HomeController> logger)
        //{
        //    _config = config;
        //    _logger = logger;
        //}

        //public IActionResult Contact()
        //{

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Contact(ContactViewModel cvm)
        //{
        //    if (!ModelState.IsValid)
        //    {

        //        return View(cvm);
        //    }

        //    string message = $"You have received a new email from your store front's contact form!<br />" +
        //        $"Sender: {cvm.Name}<br />Email: {cvm.Email}<br />Subject: {cvm.Subject}<br />" +
        //        $"Message: {cvm.Message}";

        //    var mm = new MimeMessage();

        //    mm.From.Add(new MailboxAddress("User", _config.GetValue<string>("Credentials:Email:User")));

        //    mm.To.Add(new MailboxAddress("Personal", _config.GetValue<string>("Credentials:Email:Recipient")));

        //    mm.Subject = cvm.Subject;

        //    mm.Body = new TextPart("HTML") { Text = message };

        //    mm.Priority = MessagePriority.Urgent;

        //    mm.ReplyTo.Add(new MailboxAddress("Sender", cvm.Email));

        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect(_config.GetValue<string>("Credentials:Email:Client"));

        //        client.Authenticate(

        //            _config.GetValue<string>("Credentials:Email:User"),
        //            _config.GetValue<string>("Credentials:Email:Password")

        //            );
        //        try
        //        {
        //            client.Send(mm);
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.ErrorMessage = $"There was an error processing your request. Please try" +
        //                $"again later.<br />Error Message: {ex.StackTrace}";

        //            return View(cvm);
        //        }
        //    }
        //    return View("EmailConfirmation", cvm);
        //}


    }
}