using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class UserProgramYetki : Entity<int>, IAuditable, ISoftDelete
    {
        public Guid UserId { get; set; }
        public int ProgramId { get; set; }
        public int BolumId { get; set; }
        public int FakulteId { get; set; }
    }
}
