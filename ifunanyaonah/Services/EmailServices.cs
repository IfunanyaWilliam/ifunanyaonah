using ifunanyaonah.Models;
using ifunanyaonah.Services;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Portfolio.Services
{
    public class EmailService
    {
        private readonly MailJetConfig _options;

        public EmailService(IOptions<MailJetConfig> options)
        {
            _options = options.Value;
        }

        public async Task SendMail(EmailViewModel model)
        {
            MailjetClient client = new MailjetClient(_options.ApiKey, _options.ApiSecret);
            MailjetRequest request = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }
                .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", "williamifunanya@gmail.com"},
                  {"Name", "Ifunanya Onah"}
                  }},
                 {"To", new JArray {
                  new JObject {
                   {"Email", "williamifunanya@gmail.com"},
                   {"Name", "User"}
                   }
                  }},
                 {"Subject", model.Subject},
                 {"TextPart", "New Request From Portfolio"},
                 {"HTMLPart", $"<h3>From : {model.Name} <br> Details: {model.phoneNumber}. <br> {model.Email} <br>  Message : {model.Message} </h3>"}
                 }
                });
            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }
    }
}
