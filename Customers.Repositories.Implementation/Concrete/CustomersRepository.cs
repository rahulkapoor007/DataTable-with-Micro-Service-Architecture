using Customers.Common.Exceptions;
using Customers.Repositories.Abstraction.Contracts;
using Customers.Repositories.Abstraction.Models;
using Customers.Repositories.Context.Context;
using Customers.Repositories.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Repositories.Implementation.Concrete
{
    public class CustomersRepository : IRepository<CustomersModel>
    {
        private readonly CustomersContext _customerContext;
        private IConfiguration _configuration;
        public CustomersRepository(CustomersContext customerContext, IConfiguration configuration)
        {
            this._customerContext = customerContext ?? throw new ArgumentNullException(nameof(CustomersContext));
            this._configuration = configuration;
        }
        public async Task<List<CustomersModel>> GetData
            (string Where_GeneralCriteria_Delim_mem = "", string Where_InternalCriteria_Delim_mem = "",
            bool? Return_All_Rows_ysn = false, string Sort_Expression = "", string Sort_Direction = "",
            bool? Paginate_Clients_ysn = true, int? Page_Index_int = 1, int? Page_Size_int = 10)
        {
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("@Where_GeneralCriteria_Delim_mem", Where_GeneralCriteria_Delim_mem ?? ""));
                sqlParameters.Add(new SqlParameter("@Where_InternalCriteria_Delim_mem", Where_InternalCriteria_Delim_mem ?? ""));
                sqlParameters.Add(new SqlParameter("@Return_All_Rows_ysn", Return_All_Rows_ysn));
                sqlParameters.Add(new SqlParameter("@Sort_Expression", Sort_Expression));
                sqlParameters.Add(new SqlParameter("@Sort_Direction", Sort_Direction));
                sqlParameters.Add(new SqlParameter("@Paginate_Themes_ysn", Paginate_Clients_ysn));
                sqlParameters.Add(new SqlParameter("@Page_Index_int", Page_Index_int));
                sqlParameters.Add(new SqlParameter("@Page_Size_int", Page_Size_int));

                var data = _customerContext.Customers.FromSqlRaw("get_CustomerData @Where_GeneralCriteria_Delim_mem,@Where_InternalCriteria_Delim_mem,@Return_All_Rows_ysn,@Sort_Expression,@Sort_Direction,@Paginate_Themes_ysn,@Page_Index_int,@Page_Size_int", sqlParameters.ToArray()).ToList();
                return data;

            }
            catch (Exception ex)
            {
                throw new ApplicationErrorException(ex);
            }
        }
        public async Task<DataSet> GetDataSet(FilterModel filterRequestModel)
        {
            string connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            }
            DataSet dataset = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(filterRequestModel.SpName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;
                        //cmd.Parameters.AddWithValue("@pipeduns", filterRequestModel.PipelineDuns);
                        cmd.Parameters.AddWithValue("@Where_Criteria", filterRequestModel.WhereFilters);
                        //cmd.Parameters.AddWithValue("@filterColumn", filterColumn);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            // Fill the DataSet using default values for DataTable names, etc
                            da.TableMappings.Add("Table", filterRequestModel.FilterColumn);
                            da.Fill(dataset);

                            conn.Close();

                            return dataset;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationErrorException(ex);
            }
        }
    }
}
