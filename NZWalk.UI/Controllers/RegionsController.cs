using Microsoft.AspNetCore.Mvc;
using NZWalk.UI.Models;
using NZWalk.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalk.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try    // With the help of ctrl + k + s to surround it with try catch block
            {

                //var handler = new HttpClientHandler();
                //handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                // Get all regions from Web API
                var client = _httpClientFactory.CreateClient();    // This basically creates a new HttpClient instance for us to use which we can use to consume the web API that we've.

                //var client = new HttpClient(handler);   // This basically creates a new HttpClient instance for us to use which we can use to consume the web API that we've.

                // GetAsync returns a Task<HttpResponseMessage> 
                var httpResponseMessage = await client.GetAsync("https://localhost:7284/api/regions");    // We got this base URL from the Web API project that we created.
                                                                                                          // This base url would ideally be coming from your appsetttings.json file from the UI project. Because that will differ from your different environments. So your test environment will have a different base url than your production environment or your development environment. They'll be different.
                                                                                                          // So your base API URL should ideally be coming from appsettings.json or somewhere similar where you keep the configurations for your API. But for now, we are just hardcoding it here for the sake of simplicity.
                httpResponseMessage.EnsureSuccessStatusCode();  // This will throw an exception if the status code is not successful. So if the status code is not 200 OK, it will throw an exception. So we can handle that exception in our code and show a friendly error message to the user.
                // In ASP.NET Core, the framework provides us with an HTTPClientClass which comes from System.Net.Http namespace. It provides us a way to consume web api using the get post or other different type of calls.

                // Extracting the body of the response 

                //var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
                
                // Giving it to the ViewBag
                //ViewBag.Response = responseBody;
            }
            catch (Exception)
            {
                throw;
            }


            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()       // Here we're creating the httpRequestMessage object which is of type HttpRequestMessage. This is the object that we're going to send to the Web API. So this is the request that we're going to send to the Web API.
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7284/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json") // application/json is the request content type.
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();  // This will throw an exception if the status code is not successful. So if the status code is not 200 OK, it will throw an exception. So we can handle that exception in our code and show a friendly error message to the user.

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response != null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }


        //ViewBag.Id = id;
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7284/api/regions/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage() 
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7284/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json") // application/json is the request content type.
            };
            var httpResponseMessage =  await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7081/api/regions/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception)
            {

                throw;
            }
            return View("Edit");
        }

    }
}

