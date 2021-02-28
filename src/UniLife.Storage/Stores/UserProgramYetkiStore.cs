using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Definitions.Bussines;

namespace UniLife.Storage.Stores
{
    public class UserProgramYetkiStore : BaseStore<UserProgramYetki, UserProgramYetkiDto>, IUserProgramYetkiStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        public UserProgramYetkiStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<UserProgramYetkiDto>> GetUPYByUserId(Guid userId)
        {
            var upylist = await (from p in _db.Programs
                                 join upy in _db.UserProgramYetkis.Where(x => x.UserId == userId) on p.Id equals upy.ProgramId into ps
                                 from upy in ps.DefaultIfEmpty()
                                 select new UserProgramYetkiDto
                                 {
                                     Id = upy != null ? upy.Id : 0,
                                     BolumId = p.BolumId,
                                     FakulteId = p.FakulteId,
                                     ProgramId = p.Id,
                                     UserId = upy != null ? upy.UserId : new Guid(),
                                     ProgramAd = p.Ad
                                 }
                                 ).ToListAsync();


            return upylist;
        }

        public async Task UpdateUserProgramYetkis(ProgramYetkiListUserIdDto programYetkiListUserIdDto)
        {

            //var zxc = _db.UserProgramYetkis.Where(x => x.UserId == programYetkiListUserIdDto.UserId);

            //_db.UserProgramYetkis.RemoveRange(zxc);

            //_db.SaveChanges();

            var rawBulkUpdateQuery = $"Delete from public.'UserProgramYetkis' where 'UserId' = £{programYetkiListUserIdDto.UserId}£";

            int numberOfRowAffected = await _db.Database.ExecuteSqlRawAsync(rawBulkUpdateQuery.Replace('\'', '"').Replace('£', '\''));

            List<UserProgramYetki> userProgramYetkis = new List<UserProgramYetki>();

            foreach (var item in programYetkiListUserIdDto.UserProgramYetkiList)
            {
                item.UserId = programYetkiListUserIdDto.UserId;
                var zxczc = _autoMapper.Map<UserProgramYetkiDto, UserProgramYetki>(item);
                userProgramYetkis.Add(zxczc);
            }

            _db.UserProgramYetkis.AddRange(userProgramYetkis);

            _db.SaveChanges();
        }

        
    }
}
