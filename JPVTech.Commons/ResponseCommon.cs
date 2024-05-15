using JPVTech.Commons.Interfaces;
using JPVTech.Domain.Interfaces.Services;
using JPVTech.Domain.Models;

namespace JPVTech.Commons
{
    public class ResponseCommon : IResponseCommon
    {
        public Dictionary<string, object> GenerateHttpResponse(string msg, int status, object result)
        {
            Dictionary<string, object> response = new Dictionary<string, object>
            {
                {"msg", msg},
                {"status", status},
                {"result", result}
            };
            return response;
        }

        public PagedModel<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationModel validFilter, int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedModel<List<T>>(validFilter.PageNumber, validFilter.PageSize, pagedData);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationModel(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationModel(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationModel(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationModel(roundedTotalPages, validFilter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}   