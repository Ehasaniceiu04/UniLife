using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class BaseOdataController : ControllerBase
    {

        internal readonly IApplicationDbContext _applicationDbContext;
        private IEnumerable<UserProgramYetki> _userYetki;

        public BaseOdataController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Guid GuidUserId
        {
            get
            {
                //if (User != null) /* The user object is found to be null here. */
                //{
                    return new Guid(User.GetSubjectId());
                //}

                //return new Guid();
            }
        }

        public IEnumerable<UserProgramYetki> UserYetki
        {
            get
            {
                if (_userYetki!=null)
                {
                    return _userYetki;
                }
                else
                {
                    _userYetki = _applicationDbContext.UserProgramYetkis.Where(x => x.UserId == GuidUserId);
                    return _userYetki;
                }
            }
        }
    }
}
