using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Shared.DataTransferObjects
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
