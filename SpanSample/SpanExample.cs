using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpanSample
{
    internal class SpanExample
    {
        public static void SpanUsage()
        {
            var array = new byte[100];
            var arraySpan = new Span<byte>(array);
            byte data = 0;
            for (int i = 0; i < arraySpan.Length; i++)
            {
                arraySpan[i] = data++;
            }


            int arraySum = 0;
            foreach (var item in arraySpan)
            {
                arraySum += item;
            }

            Console.WriteLine(arraySum);
        }


        public static void SpanWithNative()
        {
            var native = Marshal.AllocHGlobal(100);
            Span<byte> nativeSpan;
            unsafe
            {
                nativeSpan = new Span<byte>(native.ToPointer(), 100);
            }
            byte data = 0;
            for (int ctr = 0; ctr < nativeSpan.Length; ctr++)
                nativeSpan[ctr] = data++;

            int nativeSum = 0;
            foreach (var value in nativeSpan)
                nativeSum += value;

            Console.WriteLine($"The sum is {nativeSum}");
            Marshal.FreeHGlobal(native);
        }

        public static void SpanWithStackalloc()
        {
            byte data = 0;
            Span<byte> stackSpan = stackalloc byte[100];
            for (int ctr = 0; ctr < stackSpan.Length; ctr++)
                stackSpan[ctr] = data++;

            int stackSum = 0;
            foreach (var value in stackSpan)
                stackSum += value;

            Console.WriteLine($"The sum is {stackSum}");
        }


        public static int GetContentLength(ReadOnlySpan<char> span)
        {
            var slice = span.Slice(16);
            return int.Parse(slice);
        }

    }
}
