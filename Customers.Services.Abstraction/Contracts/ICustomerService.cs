using Customers.Services.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Services.Abstraction.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerResponseModel> GetData(CustomerRequestModel data);
        Task<DataSet> GetDataSet(FilterRequestModel data);
    }
}
