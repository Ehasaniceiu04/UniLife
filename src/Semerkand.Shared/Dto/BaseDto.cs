using UltraMapper;

namespace Semerkand.Shared.Dto
{
    public abstract class BaseDto : EntityDto<int> ,IMementoDto
    {
        private static readonly Mapper _mapper = new Mapper();
        private object state;

        public void SaveState()
        {
            state = _mapper.Map((object)this);
        }

        public void RestoreState()
        {
            if (state != null)
                _mapper.Map(state, this);
        }
        public void ClearState()
        {
            state = null;
        }
    }
}
