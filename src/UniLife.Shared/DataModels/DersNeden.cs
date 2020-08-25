using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class DersNeden : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
        public int Kod { get; set; }
    }
}
