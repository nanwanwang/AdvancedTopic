using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanSample
{
    internal class MemorySample
    {
        public static void Test()
        {
            IMemoryOwner<char> owner = MemoryPool<char>.Shared.Rent();

            Console.Write("Enter a number;");

            try
            {
                var value = int.Parse(Console.ReadLine());
                var memory = owner.Memory;

                WriteInt32ToBuffer(value,memory);

                DisplyBufferToConsole(owner.Memory.Slice(0, value.ToString().Length).Span);
               // DisplayBufferToConsole(owner.Memory.Slice(0,value.ToString().Length));

            }
            catch (FormatException)
            {
                Console.WriteLine("You did not enter a valid number.");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"You entered a number less than {Int32.MinValue:N0} or greater than {Int32.MaxValue:N0}.");
            }
            finally
            {
                owner?.Dispose();
            }
        }

        private static void WriteInt32ToBuffer(int value, Memory<char> buffer)
        {
            var strValue = value.ToString();
            strValue.AsMemory().CopyTo(buffer);
            //var span = buffer.Slice(0, strValue.Length).Span;
            //strValue.AsSpan().CopyTo(span);
          
        }

        static void DisplyBufferToConsole(ReadOnlySpan<char> buffer)
        {
            Console.WriteLine($"Contents of the buffer:'{buffer}'");
        }

        static void DisplayBufferToConsole(Memory<char> buffer)=> Console.WriteLine($"Contents of the buffer:'{buffer}'");
    }
}
