using JPVTech.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPVTech.Domain.Interfaces.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationModel filter, string route);
    }
}
