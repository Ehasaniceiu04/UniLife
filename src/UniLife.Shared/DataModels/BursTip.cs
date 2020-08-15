using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class BursTip : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
        public int Kod { get; set; }
        public double TutarOran { get; set; }
        public int Tutar1Oran2 { get; set; }
        public int YoksisKod { get; set; }
        public bool Engelli { get; set; }
        public bool Sehit { get; set; }
    }
}
