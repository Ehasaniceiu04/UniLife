using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class SinavKayitDto : EntityDto<long>
    {
        public int SinavId { get; set; }
        public int OgrenciId { get; set; }
        public int Katilim { get; set; } = 1;
        public double OgrNot { get; set; }
        public long? MazeretiSinavKayitId { get; set; } // mazereti var olan finalin bütü var demektir final kale alınmaz. Vizeninde aynı şekilde.

        public int? MazeretSinavId { get; set; }
    }
}
