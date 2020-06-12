using System;
using System.Collections.Generic;
using System.Text;

namespace UniLife.Shared.Dto
{
    public interface IMementoDto
    {
        void SaveState();
        void RestoreState();
        void ClearState();
    }
}
