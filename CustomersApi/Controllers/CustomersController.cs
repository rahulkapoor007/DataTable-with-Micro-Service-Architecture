using Customers.Common.Exceptions;
using Customers.Services.Abstraction.Contracts;
using Customers.Services.Abstraction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;

        }
        [HttpPost("GetData")]
        public async Task<IActionResult> GetData()
        {
            try
            {
                string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                CustomerRequestModel data = JsonConvert.DeserializeObject<CustomerRequestModel>(requestBody);
                return new OkObjectResult(await _customerService.GetData(data));
            }
            catch (Exception ex)
            {
                throw new ApplicationErrorException(ex);
            }
        }
        [HttpPost("GetFiltersData")]
        public async Task<IActionResult> GetFilters()
        {
            try
            {
                string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                FilterRequestModel data = JsonConvert.DeserializeObject<FilterRequestModel>(requestBody);
                return new OkObjectResult(await _customerService.GetDataSet(data));
            }
            catch (Exception ex)
            {
                throw new ApplicationErrorException(ex);
            }
        }
    }
}
