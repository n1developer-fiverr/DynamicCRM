﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Windows.Input;

namespace DCrm.Helpers
{
    public class Crm
    {

        private static string username = "appuser@krplastic.com";
        private static string password = "P@$$W0rd!";
        private static string clientId = "b8d6f914-882a-40a9-a035-4e9ef4e3caa2";
        private static string tenantId = "5ff5e38b-6561-4f85-adf9-1c7d92ba2848";
        private static string baseUrl = "https://krplastics.api.crm.dynamics.com";
        private string webApiUrl;
        private HttpClient client;

        public Crm()
        {
            webApiUrl = $"{baseUrl}/api/data/v9.2/";

        }

        private async Task<string> GetToken()
        {
            HttpClient client = new HttpClient();
            string tokenEndpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/token";
            var body = $"resource={baseUrl}&client_id={clientId}&grant_type=password&username={username}&password={password}";
            var stringContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.PostAsync(tokenEndpoint, stringContent);

            Console.WriteLine(response.StatusCode);
            JObject jobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            var token = jobject["access_token"].Value<string>();

            return token;
        }

        public async Task Setup()
        {

            var token = await GetToken();
            Console.WriteLine(token);
            Console.WriteLine("\n\n\n\n\n\n\n");
            Console.WriteLine(token);
            var header = new AuthenticationHeaderValue("Bearer", token);

            client = new HttpClient();
            client.BaseAddress = new Uri(webApiUrl);
            client.DefaultRequestHeaders.Authorization = header;
        }

        public async Task<bool> Update(Account account)
        {
            //new_columnbreak

            var toCall = $"accounts({account.Id})";

            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("PATCH"), toCall);
            message.Content = new StringContent($"{{\"new_columnbreak\": \"{account.ColumnBreak}\"}}", Encoding.UTF8,
                                    "application/json");

            var response = await client.SendAsync(message);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Account>> GetAccounts(int limit = 5)
        {
            var r = await client.GetAsync($"accounts?$top={limit}");
            var result = await r.Content.ReadAsStringAsync();

            JObject body = JObject.Parse(result);
            var values = body["value"].ToObject<JArray>();

            List<Account> accounts = new List<Account>();

            foreach (var v in values)
            {
                accounts.Add(new Account()
                {
                    Name = (string)v["name"],
                    Id = (string)v["accountid"],
                    ColumnBreak = (string)v["new_columnbreak"],
                    Address = GetAddresss((string)v["address1_line1"], (string)v["address1_city"], (string)v["address1_country"])

                });
            }

            return accounts;
        }

        private static string GetAddresss(string l1, string city, string country)
        {
            string a = "";

            if (l1 != null)
                a += " " + l1;

            if (l1 != null)
                a += " " + city;

            if (l1 != null)
                a += " " + country;

            return a;
        }
    }

    public class Account
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public string Id { get; set; }
        public string ColumnBreak { get; set; }
        public bool AllowUpdate { get; set; } = true;
    }
}