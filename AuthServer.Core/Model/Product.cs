using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Core.Model
{
    public class Product
    {
        /// <summary>
        /// Id alanını PK olarak belirlemek için
        /// 1.[Key] annotation kullanılabilir
        /// 2.Id otomatik algılanır.(aynı şekilde class ismi sonrasında Id ifadesi eklenerekde yapılabilir)
        /// </summary>

        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public int Stock { get; set; }
        public string UserId { get; set; }
    }
}
