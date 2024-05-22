using Blogger.Business.Exceptions;
using Blogger.Business.Extensions;
using Blogger.Business.Services.Abstracts;
using Blogger.Core.Models;
using Blogger.Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Blogger.Business.Services.Concretes
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IWebHostEnvironment _env;

        public EventService(IEventRepository eventRepository, IWebHostEnvironment env)
        {
            _eventRepository = eventRepository;
            _env = env;
        }

        public async Task AddAsyncEvent(Event eventt)
        {
            if (eventt.ImageFile == null)
                throw new MustBeImageFileException("Image olmalidir!");

            eventt.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\events", eventt.ImageFile);

            await _eventRepository.AddAsync(eventt);
            await _eventRepository.CommitAsync();
        }

        public void DeleteEvent(int id)
        {
            var existEvent = _eventRepository.Get(x => x.Id == id);

            if (existEvent == null)
                throw new EntityNotFoundException("Event yoxdur!");

            Helper.DeleteFile(_env.WebRootPath, @"uploads\events", existEvent.ImageUrl);

            _eventRepository.Delete(existEvent);    
            _eventRepository.Commit();
        }

        public List<Event> GEtAllEvents(Func<Event, bool>? func = null)
        {
            return _eventRepository.GetAll(func);
        }

        public Event GetEvent(Func<Event, bool>? func = null)
        {
            return _eventRepository.Get(func);
        }

        public async Task<IPagedList<Event>> GetPaginatedEventAsync(int pageIndex, int pageSize)
        {
            return await _eventRepository.GetPaginatedEventsAsync(pageIndex, pageSize); 
        }

        public void UpdateEvent(int id, Event newEvent)
        {
            var oldEvent = _eventRepository.Get(x => x.Id == id);

            if (oldEvent == null)
                throw new EntityNotFoundException("Event yoxdur!");

            if(newEvent.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\events", oldEvent.ImageUrl);

                oldEvent.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\events", newEvent.ImageFile);

            }

            oldEvent.Title = newEvent.Title;
            oldEvent.Description= newEvent.Description;
            oldEvent.SubDescription = newEvent.SubDescription;

            _eventRepository.Commit();

        }
    }
}
