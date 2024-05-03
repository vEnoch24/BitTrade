using BitTrade.Dto;
using BitTrade.Model;
using BitTrade.Respository;
using BitTrade.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayStack.Net;
using Radzen.Blazor.Rendering;
using Vonage.Request;
using Vonage;
using BitTrade.RequestPayload;
using Microsoft.JSInterop;

namespace BitTrade.DataAccess
{
    public class TransactionService : ITransactionService
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<TransactionModel> _genericRepository;
        private readonly IEmailService _mailService;
        private readonly NavigationManager _navigationManager;
        private readonly IJSRuntime _jSRuntime;
        private readonly string token;

        private PayStackApi Paystack { get; set; }


        public TransactionService(IConfiguration configuration, IGenericRepository<TransactionModel> genericRepository, NavigationManager navigationManager, IEmailService mailService, IJSRuntime jSRuntime)
        {
            _configuration = configuration;
            _genericRepository = genericRepository;
            _mailService = mailService;
            token = _configuration["Payment:PaystackSK"];
            _navigationManager = navigationManager;
            Paystack = new PayStackApi(token);
            _jSRuntime = jSRuntime;
        }

        public async Task Payment(TransactionModel payment)
        {
            TransactionInitializeRequest request = new()
            {
                AmountInKobo = (int)payment.Amount * 100,
                Email = payment.Email,
                Reference = Generate().ToString(),
                Currency = "NGN",
                CallbackUrl = "https://localhost:44364//payment-callback",
            };

            TransactionInitializeResponse response = Paystack.Transactions.Initialize(request);

            if (response.Status)
            {
                var transaction = new TransactionModel()
                {
                    Amount = payment.Amount,
                    Email = payment.Email,
                    TrxRef = request.Reference,
                    Name = payment.Name,
                    BitcoinAddress = payment.BitcoinAddress,
                    BitcoinAmount = payment.BitcoinAmount,
                };

                await _genericRepository.Create(transaction);
                //await _genericRepository.Save();
                _navigationManager.NavigateTo(response.Data.AuthorizationUrl);
            }

        }

        public async Task Verify(string reference)
        {
            TransactionVerifyResponse response = Paystack.Transactions.Verify(reference);
            if (response.Data.Status == "success")
            {
                var transaction = await _genericRepository.GetAllAsQueryable().Where(x => x.TrxRef == reference).FirstOrDefaultAsync();
                if (transaction != null)
                {
                    transaction.Status = true;
                    await _genericRepository.Update(transaction);
                    //await _genericRepository.Save();
                    await SendMail(transaction);
                    //await SendSms();  
                    _navigationManager.NavigateTo("https://localhost:44364/fetchData");
                }      
            }
        }

        public async Task<List<TransactionModel>> GetAllTransactions()
        {
            //var transactions = await _genericRepository.GetAllAsQueryable().Where(x => x.Status == true).ToListAsync();
            var transactions = await _genericRepository.GetAllAsQueryable().OrderByDescending(d => d.CreatedAt).ToListAsync();

            return transactions;
        }

        public async Task DeleteTransactions()
        {
            await _genericRepository.GetAllAsQueryable().Where(x => x.Status == false).ExecuteDeleteAsync();
        }

        public async Task DeleteTransaction(string id)
        {
            var transaction = await _genericRepository.GetById(id);
            if(transaction == null)
            {
                await _jSRuntime.InvokeVoidAsync("console.log", "transaction not found");
            }
            else
            {
                await _genericRepository.Delete(transaction);
                await _genericRepository.Save();
            }        
        }


        public async Task SendMail(TransactionModel transaction)
        {
            var emailBody = "<!DOCTYPE html>" +
                "<html lang=\"en\">" +
                "<head>" +
                "</head>" +
                "<body> " +
                " <div style=\"background-color: #f4f4f4; padding: 20px;\">" +
                "<p style=\"font-size: 18px;\"><b>Client, <span style=\"color: #007bff;\"> " + transaction.Name + "</span>!</b></p>" +
            "<p style=\"font-size: 16px;\">With BitCoin Address <span style=\"color: #007bff;\">" + transaction.BitcoinAddress + "</span> and " +
            "Email <span style=\"color: #007bff;\">" + transaction.Email + "</span>, has ordered <span style=\"color: #007bff;\">" + transaction.BitcoinAmount + " Bitcoin</span>!</p>" +
            " </body>" +
            "</html>";

            List<string> emails = new List<string>();

            emails.Add("richardatjohn@email.com");
            emails.Add("ogunrindeenoch1@email.com");

            var emailDto = new EmailDto("ogunrindeenoch1@gmail.com", "BitCoin Order", emailBody);
            await _mailService.SendEmailAsync(emailDto);    
        }

        public static int Generate()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            return rand.Next(10000000, 999999999);
        }

        public Task SendSms( SmsMessageRequest model)
        {
            var credentials = Credentials.FromApiKeyAndSecret(
                "939ddbb6",
                "zXfP4KGzaP195jzD"
                );

            var VonageClient = new VonageClient(credentials);

            var response = VonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
            {
                To = model.To,
                From = "BitTrade",
                Text = model.Message
            });

            return Task.FromResult(response);
        }
    }
}
