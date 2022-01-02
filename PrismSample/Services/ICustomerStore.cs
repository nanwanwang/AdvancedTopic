using System.Collections.Generic;

namespace PrismSample.Sevices
{
    public interface ICustomerStore
    {
        List<string> GetAll();
    }
}