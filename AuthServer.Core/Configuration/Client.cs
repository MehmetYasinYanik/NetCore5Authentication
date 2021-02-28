using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Core.Configuration
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }

        // Hangi API lara erişim sağlanacak
        // www.myapi1.com www.myapi.2.com
        public List<string> Audiences { get; set; } 
    }
}
