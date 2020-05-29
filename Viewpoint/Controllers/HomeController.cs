using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Viewpoint.Controllers
{
    public class HomeController : Controller
    {
        private AppSettings _mySettings { get; set; }

        public HomeController(IOptions<AppSettings> settings)
        {
            _mySettings = settings.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string SourceCode, string applicationNumber, 
                                               string sourceBatchID, string pageCount, 
                                               string ssn, string contractType)
        {
            var fin = new Models.Fin()
            {
                mth_pay_amt = "479.50",
                mth_st_dt = "2002-04-21",
                term = "60",
                total = "39489.52"
            };

            var vehicles = new Models.Vehicles()
            {
                Vehicle = new List<Models.Vehicle>()
                {
                    new Models.Vehicle(){Make="Chevy", Model="Cruize",  Year=2020},
                    new Models.Vehicle(){Make="Chevy", Model="Malibu",  Year=2020}
                }
            };

            var loans = new Models.Loans()
            {
                LoanInfo = new List<Models.LoanInfo>()
                {
                    new Models.LoanInfo(){flatAmount = "100.00", loan_type= "Approved", percentAmount= "5.0", program= "Chevy"},
                    new Models.LoanInfo(){flatAmount = "0.00", loan_type= "Suggested", percentAmount= "1.25", program= "Chevy"},
                    new Models.LoanInfo(){flatAmount = "0.00", loan_type= "Subvented", percentAmount= ".05", program= "Chevy"}
                }
            };

            var input = new Models.Inbound()
            {
                
                sourceCode = SourceCode,
                applicationNumber = applicationNumber,
                sourceBatchID = sourceBatchID,
                ssn = ssn,
                pageCount = pageCount,
                contractType = contractType,
                metaDataFilePath = sourceBatchID.Trim() + ".xml",                               
                Case = new Models.Case() { Vehicles = vehicles, Loans = loans, fin = fin }
            };

            string inputxml = input.ToXML();

            try
            {
               using var req = new HttpClient();
                req.DefaultRequestHeaders.Add("User-Agent", "Timer Function");
                req.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("text/plain"));

                HttpResponseMessage response = await req.PostAsync(_mySettings.AzureFunctionURL, new StringContent(inputxml));
                response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();
                if (data != null)
                {
                    ViewData["result"] = data;
                }
            }
            catch(Exception ex)
            {
                ViewData["result"] = ex.Message;
            }
            return View("Response");
        }
    }
}
