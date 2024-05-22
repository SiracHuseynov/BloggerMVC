using Blogger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Blogger.Business.Services.Abstracts
{
    public interface IEventService
    {
        Task AddAsyncEvent(Event eventt);
        void DeleteEvent(int id);
        void UpdateEvent(int id, Event newEvent);
        Event GetEvent(Func<Event, bool>? func = null);
        List<Event> GEtAllEvents(Func<Event, bool>? func = null);
        Task<IPagedList<Event>> GetPaginatedEventAsync(int pageIndex, int pageSize);

    }
}
