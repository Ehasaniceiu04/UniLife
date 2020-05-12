using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.Dto.Definitions
{
    public class ApplicationUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }
    }
}
