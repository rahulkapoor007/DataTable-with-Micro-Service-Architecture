using DataTableDemo.Enums;
using DataTableDemo.Generic;
using DataTableDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTableDemo.Controllers
{
    public class CustomersController : Controller
    {
        public readonly IConfiguration _configuration;
        public static string MicroServiceUrl;
        public CustomersController(IConfiguration configuration)
        {
            _configuration = configuration;
            MicroServiceUrl = _configuration["Data:MicroServiceEndPoint"];
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GetCustomerDataAsync([FromBody] Root dtParameters) 
        {
            //var a= JsonConvert.DeserializeObject<DTParameters>(dtParameters);
            var filteredResultsCount = 0;
            var totalResultsCount = 0;
            var orderCriteria = string.Empty;
            var orderDirection = string.Empty;
            CustomerResponseModel customerResponseModel = new CustomerResponseModel();
            CustomerDataRequestModel dataModel = new CustomerDataRequestModel();
            try
            {
                int PageIndex = (Convert.ToInt32(dtParameters.Start) / Convert.ToInt32(dtParameters.Length)) + 1;
                int PageSize = Convert.ToInt32(dtParameters.Length);

                foreach (var col in dtParameters.Columns)
                {
                    SearchFields fields = null;
                    string searchValue = col.Search.Value;
                    string data = col.Data;
                    if (!string.IsNullOrEmpty(searchValue))
                        fields = GetSearchFields(searchValue, data);
                    if (fields != null)
                        dataModel.SearchFields.Add(fields);
                }
                if (dtParameters.Order != null)
                {
                    orderDirection = dtParameters.Order[0].Dir.ToString().ToLower();
                    orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                }
                dataModel.SortingField.FieldName = orderCriteria;
                dataModel.SortingField.Direction = orderDirection;
                dataModel.PageNumber = PageIndex;
                dataModel.PageSize = PageSize;
                dataModel.GeneralSearchField = dtParameters.Search.Value;

                var responseBody =await HttpRequest.GetRequestAsync(MicroServiceUrl, dataModel, "GetData");
                if (!string.IsNullOrEmpty(responseBody))
                {
                    customerResponseModel = JsonConvert.DeserializeObject<CustomerResponseModel>(responseBody);

                    if (customerResponseModel != null)
                    {
                        totalResultsCount = Convert.ToInt32(customerResponseModel.TotalCount);
                        filteredResultsCount = Convert.ToInt32(customerResponseModel.ResultCount);
                    }
                    else
                    {
                        totalResultsCount = 0; filteredResultsCount = 0;
                        customerResponseModel = new CustomerResponseModel();
                    }
                }
            }
            catch (Exception e)
            {

            }


            return Json(new
            {
                draw = dtParameters.Draw,
                recordsFiltered = totalResultsCount,//Total List Number To return
                recordsTotal = filteredResultsCount,//Actual Count
                data = customerResponseModel.DataList ?? new List<CustomersModel>()//Data list
            });
        }
        private static SearchFields GetSearchFields(string searchValue, string data)
        {
            SearchFields fields = new SearchFields();

            switch (data?.ToLower())
            {
                case "id":
                    fields.FieldName = "id";
                    fields.FieldValue = new List<string>() { searchValue };
                    fields.OperatorType = Operator.LIKE;
                    break;
                case "name":
                    fields.FieldName = "name";
                    fields.FieldValue = searchValue.Split('|').ToList();
                    fields.OperatorType = Operator.IN;
                    break;
                case "address":
                    fields.FieldName = "address";
                    fields.FieldValue = searchValue.Split('|').ToList();
                    fields.OperatorType = Operator.IN;
                    break;
                case "phonenumber":
                    fields.FieldName = "phoneNumber";
                    fields.FieldValue = new List<string>() { searchValue };
                    fields.OperatorType = Operator.LIKE;
                    break;
                case "email":
                    fields.FieldName = "email";
                    fields.FieldValue = new List<string>() { searchValue };
                    fields.OperatorType = Operator.LIKE;
                    break;
                }
            return fields;
        }
        [HttpPost]
        public async Task<IActionResult> GetFilters([FromBody] Root dtParameters)
        {
            string responseBody = string.Empty;
            CustomerRef filtersData = new CustomerRef();

            CustomerFilterRequestModel dataModel = new CustomerFilterRequestModel();
            foreach (var col in dtParameters.Columns)
            {
                SearchFields fields = null;
                string searchValue = col.Search.Value;
                string data = col.Data;
                if (!string.IsNullOrEmpty(col.Search.Value.ToString()) && !string.Equals(dtParameters.RequestedFilterColumn, col.Data, StringComparison.OrdinalIgnoreCase))
                {
                    fields = GetSearchFields(searchValue, data);
                }
                if (fields != null)
                    dataModel.SearchFields.Add(fields);
            }
            dataModel.FilterColumn = dtParameters.RequestedFilterColumn.ToString();
            dataModel.SpName = $"usp_distinct{dtParameters.RequestedFilterColumn}";

            responseBody = await HttpRequest.GetRequestAsync(MicroServiceUrl, dataModel, "GetFiltersData");

            if (!string.IsNullOrEmpty(responseBody))
            {
                filtersData = JsonConvert.DeserializeObject<CustomerRef>(responseBody);
            }
            else
            {
                filtersData = null;
            }
            return Json(new
            {
                name = filtersData.name,
                address = filtersData.address
            });
        }
    }
    public class CustomerRef
    {
        public List<Name> name { get; set; }
        public List<Address> address { get; set; }
    }
    public class Name
    {
        public string FilterColumn { get; set; }
    }
    public class Address
    {
        public string FilterColumn { get; set; }
    }
}
