using Blogger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Blogger.Core.RepositoryAbstracts
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IPagedList<Event>> GetPaginatedEventsAsync(int pageIndex, int pageSize);

    }
}
