﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Core.Dto
{
    public class ClientTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
    }
}
