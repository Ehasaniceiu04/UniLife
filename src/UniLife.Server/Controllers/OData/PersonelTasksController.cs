using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class PersonelTasksController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PersonelTasksController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        public IEnumerable<PersonelTask> Get()
        {
            return _applicationDbContext.PersonelTasks;
        }

        [AcceptVerbs("POST", "OPTIONS")]
        public void Post([FromBody] PersonelTask eventData)
        {
            PersonelTask insertData = new PersonelTask();
            insertData.Id = (_applicationDbContext.PersonelTasks.ToList().Count > 0 ? _applicationDbContext.PersonelTasks.ToList().Max(p => p.Id) : 1) + 1;
            insertData.StartTime = Convert.ToDateTime(eventData.StartTime).ToLocalTime();
            insertData.EndTime = Convert.ToDateTime(eventData.EndTime).ToLocalTime();
            insertData.Subject = eventData.Subject;
            insertData.IsAllDay = eventData.IsAllDay;
            insertData.Location = eventData.Location;
            insertData.Description = eventData.Description;
            insertData.RecurrenceRule = eventData.RecurrenceRule;
            insertData.RecurrenceID = eventData.RecurrenceID;
            insertData.RecurrenceException = eventData.RecurrenceException;
            insertData.StartTimezone = eventData.StartTimezone;
            insertData.EndTimezone = eventData.EndTimezone;

            _applicationDbContext.PersonelTasks.Add(insertData);
            _applicationDbContext.SaveChanges();
        }

    }
}
