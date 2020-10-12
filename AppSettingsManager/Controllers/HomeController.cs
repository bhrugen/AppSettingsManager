﻿using AppSettingsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppSettingsManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private TwilioSettings _twilioSettings;
        private readonly IOptions<TwilioSettings> _twilioOptions;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, IOptions<TwilioSettings> twilioOptions,
            TwilioSettings twilioSettings)
        {
            _logger = logger;
            _config = config;
            _twilioOptions = twilioOptions;
            //_twilioSettings = new TwilioSettings();
            //config.GetSection("Twilio").Bind(_twilioSettings);
            _twilioSettings = twilioSettings;
        }

        public IActionResult Index()
        {
            ViewBag.SendGridKey = _config.GetValue<string>("SendGridKey");
            //ViewBag.TwilioAuthToken = _config.GetSection("Twilio").GetValue<string>("AuthToken");
            //ViewBag.TwilioAccountSid = _config.GetValue<string>("Twilio:AccountSid");
            //ViewBag.TwilioPhoneNumber = _twilioSettings.PhoneNumber;

            //IOptions
            //ViewBag.TwilioAuthToken = _twilioOptions.Value.AuthToken;
            //ViewBag.TwilioAccountSid = _twilioOptions.Value.AccountSid;
            //ViewBag.TwilioPhoneNumber = _twilioOptions.Value.PhoneNumber;
            
            ViewBag.TwilioAuthToken = _twilioSettings.AuthToken;
            ViewBag.TwilioAccountSid = _twilioSettings.AccountSid;
            ViewBag.TwilioPhoneNumber = _twilioSettings.PhoneNumber;


            //ViewBag.ThirdLevelSettingValue = _config.GetValue<string>("FirstLevelSetting:SecondLevelSetting:BottomLevelSetting");
            //ViewBag.ThirdLevelSettingValue = _config.GetSection("FirstLevelSetting").GetSection("SecondLevelSetting")
            //                           .GetValue<string>("BottomLevelSetting");
            ViewBag.ThirdLevelSettingValue = _config.GetSection("FirstLevelSetting").GetSection("SecondLevelSetting")
                                           .GetSection("BottomLevelSetting").Value;
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
    }
}
