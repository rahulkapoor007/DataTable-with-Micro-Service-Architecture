using Customers.Repositories.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Repositories.Abstraction.Contracts
{
    public interface IRepository<T>
    {
        //Task<OverallOperationResponse<T>> GetIemsAsync
        //    (string tspCode, List<SearchModel> searchItems,
        //    KeyValuePair<string, string> sortingFieldAndOrder,
        //    KeyValuePair<int, int> pageSizeAndNumber);

        //Task<OverallOperationResponse<DataSet>> GetDataSetAsync(List<SearchModel> searchItems,
        //    string TspCode, string FilterColumn);
        Task<List<T>> GetData(string Where_GeneralCriteria = "", string Where_InternalCriteria = "", bool? Return_All_Rows_ysn = false, string Sort_Expression = "", string Sort_Direction="", bool? Paginate_Clients_ysn = true, int? Page_Index_int = 1, int? Page_Size_int = 10);

        Task<DataSet> GetDataSet(FilterModel model);
    }
}
