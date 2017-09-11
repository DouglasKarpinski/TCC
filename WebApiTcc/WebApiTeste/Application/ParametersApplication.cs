﻿using Newtonsoft.Json;
using WebApiTeste.Models;

namespace WebApiTeste.Application
{
    public static class ParametersApplication
    {
        private static Parameters _parameters;

        public static Parameters Parameters => _parameters ?? Get();

        private static Parameters Get()
        {
            //var url = $"{(Debugger.IsAttached ? "http://localhost" : ConfigurationManager.AppSettings["Url"])}:{ConfigurationManager.AppSettings["Port"]}/";
            var url = "http://localhost";
            var response = new WebApiRequests.WebApiRequest(url).AddResource("parameters").Get();

            _parameters = JsonConvert.DeserializeObject<Parameters>(response.Content.ReadAsStringAsync().Result);
            return _parameters;
        }
    }
}