﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BallHogs.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace BallHogs.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string player)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://www.balldontlie.io");

            var response = await client.GetAsync($"/api/v1/players?search={player}");

            var body = await response.Content.ReadAsStringAsync();

            var content = JsonConvert.DeserializeObject<ApiModel>(body);

            return View("SearchResult", content);
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
