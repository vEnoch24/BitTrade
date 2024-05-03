using BitTrade.Data;
using BitTrade.Dto;
using BitTrade.Helpers;
using BitTrade.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Security.Cryptography;
using Vonage.Users;
using Vonage.Voice.EventWebhooks;

namespace BitTrade.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly NavigationManager _navigationManager;
        private readonly BitTradeDbContext _dbContext;
        private readonly TokenService _tokenService;

        public AuthServices(BitTradeDbContext dbContext, TokenService tokenService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, NavigationManager navigationManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _navigationManager = navigationManager;
            _tokenService = tokenService;
        }



        public async Task Login(InputModel model)
        {

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                RedirectToHome();
            }
            //RedirectToHome();
        }

        private void RedirectToHome()
        {
            _navigationManager.NavigateTo("/");
        }


        public async Task Register(InputModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                _navigationManager.NavigateTo("/");
            }

        }

        public void Dispose()
        {
            _userManager.Dispose();
        }

    }
}
