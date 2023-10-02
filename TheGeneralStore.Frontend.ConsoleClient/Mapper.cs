using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeneralStore.Frontend.ConsoleClient
{
    public static class Mapper
    {
        public static LoginDTO ToDTO(this Login login)
        {
            if (login != null)
            {
                return new LoginDTO
                {
                    Id = login.Id,
                    Name = login.Name,
                };
            }

            return null;
        }
    }
}
