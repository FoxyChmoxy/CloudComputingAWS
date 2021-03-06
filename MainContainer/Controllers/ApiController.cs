﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess.JsonModels;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainContainer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public ApiController(ILogger<ApiController> logger,
            IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public OkObjectResult Get()
        {
            return Ok(new { response = 200 });
        }

        #region USER

        [HttpGet("user")]
        public async Task<OkObjectResult> GetUserList()
        {
            var response = await GetResponse("RDS", "user");
            return Ok(response);
        }

        [HttpGet("user/email/{email}")]
        public async Task<OkObjectResult> GetUserByEmail(string email)
        {
            var response = await GetResponse("RDS", $"user/email/{email}");
            return Ok(response);
        }

        [HttpGet("user/{id}")]
        public async Task<OkObjectResult> GetUserById(string id)
        {
            var response = await GetResponse("RDS", $"user/{id}");
            return Ok(response);
        }

        [HttpPost("user/edit")]
        public async Task<OkObjectResult> EditUser(UserJsonModel user)
        {
            var response = await PostResponse("RDS", "user/edit", user);
            return Ok(response);
        }

        #endregion

        #region PRODUCT

        [HttpGet("product")]
        public async Task<OkObjectResult> GetProductList()
        {
            var response = await GetResponse("RDS", "product");
            return Ok(response);
        }

        [HttpGet("product/{id}")]
        public async Task<OkObjectResult> GetProductById(int id)
        {
            var response = await GetResponse("RDS", "product", id);
            return Ok(response);
        }

        [HttpPut("product/{id}")]
        public async Task<OkObjectResult> EditProduct(int id, Product product)
        {
            var response = await PutResponse("RDS", $"product/{id}", product);
            return Ok(response);
        }

        [HttpPost("product")]
        public async Task<OkObjectResult> CreateProduct(Product product)
        {
            var response = await PostResponse("RDS", "product", product);
            return Ok(response);
        }

        [HttpDelete("product/{id}")]
        public async Task<OkObjectResult> DeleteProduct(int id)
        {
            var response = await DeleteResponse("RDS", $"product/{id}");
            return Ok(response);
        }

        #endregion

        #region ORDER

        [HttpGet("order")]
        public async Task<OkObjectResult> GetOrderList()
        {
            var response = await GetResponse("MDB", "order");
            return Ok(response);
        }

        [HttpGet("order/{id}")]
        public async Task<OkObjectResult> GetOrderById(string id)
        {
            var response = await GetResponse("MDB", "order", id);
            return Ok(response);
        }

        [HttpPut("order/{id}")]
        public async Task<OkObjectResult> EditOrder(string id, OrderJsonModel order)
        {
            var response = await PutResponse("MDB", $"order/{id}", order);
            return Ok(response);
        }

        [HttpPost("order")]
        public async Task<OkObjectResult> CreateOrder(OrderJsonModel order)
        {
            var response = await PostResponse("MDB", "order", order);
            return Ok(response);
        }

        [HttpDelete("order/{id}")]
        public async Task<OkObjectResult> DeleteOrder(string id)
        {
            var response = await DeleteResponse("MDB", $"order/{id}");
            return Ok(response);
        }

        #endregion

        #region POST

        [HttpGet("post")]
        public async Task<OkObjectResult> GetPostList()
        {
            var response = await GetResponse("MDB", "post");
            return Ok(response);
        }

        [HttpGet("post/{id}")]
        public async Task<OkObjectResult> GetPostById(string id)
        {
            var response = await GetResponse("MDB", "post", id);
            return Ok(response);
        }

        [HttpPut("post/{id}")]
        public async Task<OkObjectResult> EditPost(string id, PostJsonModel post)
        {
            var response = await PutResponse("MDB", $"post/{id}", post);
            return Ok(response);
        }

        [HttpPost("post")]
        public async Task<OkObjectResult> CreatePost(PostJsonModel post)
        {
            var response = await PostResponse("MDB", "post", post);
            return Ok(response);
        }

        [HttpDelete("post/{id}")]
        public async Task<OkObjectResult> DeletePost(string id)
        {
            var response = await DeleteResponse("MDB", $"post/{id}");
            return Ok(response);
        }

        #endregion

        #region RESPONSE METHODS

        private async Task<string> GetResponse(string httpClient, string action,
            params object[] param)
        {
            if (param.Length > 0)
                foreach (var par in param)
                    action += "/" + par;

            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.GetAsync(action);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> PostResponse(string httpClient, string action, object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.PostAsync(action, data);
            return response.Content.ReadAsStringAsync().Result;
        }

        private async Task<string> PutResponse(string httpClient, string action, object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.PutAsync(action, data);
            return response.Content.ReadAsStringAsync().Result;
        }

        private async Task<string> DeleteResponse(string httpClient, string action)
        {
            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.DeleteAsync(action);
            return response.Content.ReadAsStringAsync().Result;
        }

        #endregion
    }
}