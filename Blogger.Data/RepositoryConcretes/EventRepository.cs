using Blogger.Core.Models;
using Blogger.Core.RepositoryAbstracts;
using Blogger.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Blogger.Data.RepositoryConcretes
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly AppDbContext _appDbContext;
        public EventRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext= appDbContext;
        }

        public async Task<IPagedList<Event>> GetPaginatedEventsAsync(int pageIndex, int pageSize)
        {
            var query = _appDbContext.Events.AsQueryable();
            return await query.ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
