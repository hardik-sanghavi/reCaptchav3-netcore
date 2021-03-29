using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using reCaptchav3.Models;
using reCaptchav3.Repository.Interfaces;

namespace reCaptchav3.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICaptchaValidator _captchaValidator;

        public AccountController(ICaptchaValidator captchaValidator)
        {
            _captchaValidator = captchaValidator;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model, string token)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(token))
            {
                ModelState.AddModelError("captcha", "Captcha validation failed");
            }
            return View();
        }
    }
}
