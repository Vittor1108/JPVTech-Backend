using JPVTech.Domain.Interfaces.Services;
using JPVTech.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPVTech.Commons.Interfaces
{
    public interface IResponseCommon
    {
        public Dictionary<string, object> GenerateHttpResponse(string msg, int status, object result);
        public PagedModel<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationModel validFilter, int totalRecords, IUriService uriService, string route);
    }
}
