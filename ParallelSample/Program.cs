using System.Net.Http.Headers;
using System.Net.Http.Json;
using ParallelSample;

var size = ParallelForEachAsync.DirectoryFileSize(AppDomain.CurrentDomain.BaseDirectory, "*.txt", SearchOption.AllDirectories);
Console.WriteLine(size);
Console.ReadKey();  
