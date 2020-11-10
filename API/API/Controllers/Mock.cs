﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("/Redis")]
    public class RedisController : ControllerBase
    {
        private IEasyCachingProvider cachingProvider;
        private IEasyCachingProviderFactory cachingProviderFactory;

        public RedisController(IEasyCachingProviderFactory cachingProviderFactory)
        {
            this.cachingProviderFactory = cachingProviderFactory;
            this.cachingProvider = this.cachingProviderFactory.GetCachingProvider("redis1");
        }

        [HttpGet("Set")]
        public DefaultResponse SetItemInQueue()
        {
            this.cachingProvider.Set("TestKey123", "Here is my value", TimeSpan.FromDays(100));

            var response = new DefaultResponse { success = true, message = "This route was called." };

            return response;
        }

        [HttpGet("Get")]
        public IActionResult GetItemInQueue()
        {
            var item = this.cachingProvider.Get<string>("TestKey123");

            return Ok(item);
        }
    }
}