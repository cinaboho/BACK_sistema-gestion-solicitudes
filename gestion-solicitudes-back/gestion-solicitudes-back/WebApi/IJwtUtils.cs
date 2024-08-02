using PlantillaApiJWT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaApiJWT.WebApi
{
    public interface IJwtUtils
    {
        public string GenTokenkey(UserTokens model);        
        public Task<string> ValidateToken(string token);
    }
}
