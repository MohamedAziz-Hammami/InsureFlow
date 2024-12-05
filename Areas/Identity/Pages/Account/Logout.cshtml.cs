// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AstreeWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AstreeWebApp.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AstreeUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<AstreeUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // Sign out the user
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            TempData["SuccessMessage"] = "You have successfully logged out.";
            // Optionally clear session if you're using it (usually unnecessary for cookie authentication)
            HttpContext.Session.Clear();

            // Redirect to the return URL or the home page
            if (returnUrl != null)
            {
                // Ensure returnUrl is a local URL to prevent open redirect attacks
                if (Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    _logger.LogWarning("Invalid return URL: {ReturnUrl}", returnUrl);
                }
            }

            return RedirectToPage("/Index");
        }
    }
}
