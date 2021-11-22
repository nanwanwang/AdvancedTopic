using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAsyncEnumerableSample
{
    internal class IEnumerableSample
    {
        
        public static async Task<IEnumerable<int>> FetchIOTData()
        {
            List<int> data = new List<int>();
            for (int i = 1; i <=10; i++)
            {
                await Task.Delay(1000);

                data.Add(i);
            }

            return data;
        }

        public static async IAsyncEnumerable<int> FetchIOTDataAsync()
        {
            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(1000);
                yield return i;
            }
        }
    }
}
