﻿using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class OgrenimDurum : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
        public int Kod { get; set; }
    }
}