using System.Collections.Generic;

namespace PrismSample.Sevices
{
    public class DbCustomerStore:ICustomerStore
    {
        public List<string> GetAll()
        {
            return new List<string>() { "a","b","c" };
        }
    }
}