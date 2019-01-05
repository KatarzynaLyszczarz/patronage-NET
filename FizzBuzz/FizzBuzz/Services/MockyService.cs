using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace FizzBuzz
{
    public class MockyService : IMockyService
    {

        async Task<string> IMockyService.GetContent()
        {
            
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://www.mocky.io/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                    HttpResponseMessage response = await client.GetAsync("v2/5c127054330000e133998f85");
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();

            }
                
        }
        
    }
}
