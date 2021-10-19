using Customers.Common.Enums;
using Customers.Common.Extension;
using Customers.Repositories.Abstraction.Contracts;
using Customers.Repositories.Abstraction.Models;
using Customers.Repositories.Context.Entities;
using Customers.Services.Abstraction.Contracts;
using Customers.Services.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Services.Implementation.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<CustomersModel> _customerRepository;
        public CustomerService(IRepository<CustomersModel> customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public async Task<CustomerResponseModel> GetData(CustomerRequestModel data)
        {
            string Where_GeneralCriteria = data.GeneralSearchField;
            string Where_InternalCriteria = string.Empty;
            if (data.SearchFields != null)
                Where_InternalCriteria = this.GetWhereCriteria(data.SearchFields);
            bool? Return_All_Rows_ysn = false;
            string Sort_Expression = data.SortingField.FieldName;
            string Sort_Direction = data.SortingField.Direction;
            bool? Paginate_Clients_ysn = true;
            int? Page_Index_int = data.PageNumber;
            int? Page_Size_int = data.PageSize;

            CustomerResponseModel model = new CustomerResponseModel();
            var response = await _customerRepository.GetData(Where_GeneralCriteria, Where_InternalCriteria,
            Return_All_Rows_ysn, Sort_Expression, Sort_Direction, Paginate_Clients_ysn,
            Page_Index_int, Page_Size_int);

            if (response!=null && response.Count > 0)
            {
                model.customerList = this.AssignToEntity(response);
                model.PageIndex = data.PageNumber;
                model.PageSize = data.PageSize;
                model.ResultCount = model.customerList.Count();
                model.TotalCount = response.FirstOrDefault().count;
            }
            else
            {
                model.customerList = null;
                model.PageIndex = data.PageNumber;
                model.PageSize = data.PageSize;
                model.ResultCount = 0;
                model.TotalCount = 0;
            }

            return model;
        }
        public async Task<DataSet> GetDataSet(FilterRequestModel data)
        {
            var filterModel = this.TransformToModel(data);
            return await this._customerRepository.GetDataSet(filterModel);
        }
        private FilterModel TransformToModel(FilterRequestModel data)
        {
            FilterModel requestModel = new FilterModel();
            requestModel.WhereFilters = data.SearchFields != null ? this.GetWhereCriteria(data.SearchFields) : "";
            requestModel.SpName = data.SpName;
            requestModel.FilterColumn = data.FilterColumn;
            return requestModel;
        }
        private string GetWhereCriteria(List<SearchFields> model)
        {
            string criteria = string.Empty;
            foreach (SearchFields fields in model)
            {
                if (string.IsNullOrEmpty(fields.FieldName) || fields.FieldValue.Count == 0) throw new Exception("Invalid search field.");

                switch (fields.OperatorType)
                {
                    case Operator.EQUAL:
                        criteria += $"{fields.FieldName} {fields.OperatorType.GetEnumDescription()} '{fields.FieldValue.FirstOrDefault()}'";
                        break;
                    case Operator.IN:
                        List<string> values = new List<string>();
                        foreach (var field in fields.FieldValue)
                        {
                            values.Add($"'{field?.ToLower()}'");
                        }
                        criteria += $"{fields.FieldName} {fields.OperatorType.GetEnumDescription()} ({string.Join(",", values)})";
                        break;
                    case Operator.LT:
                        criteria += $"{fields.FieldName} {fields.OperatorType.GetEnumDescription()} '{fields.FieldValue.FirstOrDefault()}'";
                        break;
                    case Operator.GT:
                        criteria += $"{fields.FieldName} {fields.OperatorType.GetEnumDescription()} '{fields.FieldValue.FirstOrDefault()}'";
                        break;
                    case Operator.LTE:
                        criteria += $"{fields.FieldName} {fields.OperatorType.GetEnumDescription()} '{fields.FieldValue.FirstOrDefault()}'";
                        break;
                    case Operator.GTE:
                        criteria += $"{fields.FieldName} {fields.OperatorType.GetEnumDescription()} '{fields.FieldValue.FirstOrDefault()}'";
                        break;
                    case Operator.LIKE:
                        criteria += $"{fields.FieldName} {fields.OperatorType.GetEnumDescription()} '%{fields.FieldValue.FirstOrDefault()}%'";
                        break;
                }
            }
            return criteria;
        }
        private IEnumerable<CustomerSendingModel> AssignToEntity(List<CustomersModel> models)
        {
            IEnumerable<CustomerSendingModel> entity = models.Select(x => new CustomerSendingModel() 
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email
            });
            return entity;
        }
    }
}
