using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Identities
{
    public interface IJwtIdentity
    {
        public string GenerateJwtToken(string userName);
    }
}
