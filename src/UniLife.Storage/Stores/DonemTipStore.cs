using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class DonemTipStore : BaseStore<DonemTip, DonemTipDto>, IDonemTipStore
    {
        //private readonly IApplicationDbContext _db;
        //private readonly IMapper _autoMapper;

        public DonemTipStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            //_db = db;
            //_autoMapper = autoMapper;
        }




        //private readonly IApplicationDbContext _db;
        //private readonly IMapper _autoMapper;

        //public DonemTipStore(IApplicationDbContext db, IMapper autoMapper)
        //{
        //    _db = db;
        //    _autoMapper = autoMapper;
        //}


        //public async Task<List<DonemTipDto>> GetAll()
        //{
        //    //return await _autoMapper.ProjectTo<DonemTipDto>(_db.DonemTips).ToListAsync();
        //    //var asd=  _autoMapper.Map<List<DonemTipDto>>(await _db.DonemTips.Include("Universite").ToListAsync());
        //    var asd = _autoMapper.Map<List<DonemTipDto>>(await _db.DonemTips.ToListAsync());
        //    return asd;
        //}

        //public async Task<DonemTipDto> GetById(int id)
        //{
        //    var donemTip = await _db.DonemTips.SingleOrDefaultAsync(t => t.Id == id);

        //    if (donemTip == null)
        //        throw new InvalidDataException($"Unable to find DonemTip with ID: {id}");

        //    return _autoMapper.Map<DonemTipDto>(donemTip);
        //}

        //public async Task<DonemTip> Create(DonemTipDto donemTipDto)
        //{
        //    var donemTip = _autoMapper.Map<DonemTipDto, DonemTip>(donemTipDto);
        //    await _db.DonemTips.AddAsync(donemTip);
        //    await _db.SaveChangesAsync(CancellationToken.None);
        //    return donemTip;
        //}

        //public async Task<DonemTip> Update(DonemTipDto donemTipDto)
        //{
        //    var donemTip = await _db.DonemTips.SingleOrDefaultAsync(t => t.Id == donemTipDto.Id);
        //    if (donemTip == null)
        //        throw new InvalidDataException($"Unable to find DonemTip with ID: {donemTipDto.Id}");

        //    donemTip = _autoMapper.Map(donemTipDto, donemTip);
        //    _db.DonemTips.Update(donemTip);
        //    await _db.SaveChangesAsync(CancellationToken.None);

        //    return donemTip;
        //}

        //public async Task DeleteById(int id)
        //{
        //    var donemTip = _db.DonemTips.SingleOrDefault(t => t.Id == id);

        //    if (donemTip == null)
        //        throw new InvalidDataException($"Unable to find DonemTip with ID: {id}");

        //    _db.DonemTips.Remove(donemTip);
        //    await _db.SaveChangesAsync(CancellationToken.None);
        //}
    }
}
