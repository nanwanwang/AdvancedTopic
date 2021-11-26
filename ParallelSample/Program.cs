using System.Net.Http.Headers;
using System.Net.Http.Json;
using ParallelSample;

//var size = ParallelForEachAsync.DirectoryFileSize(AppDomain.CurrentDomain.BaseDirectory, "*.txt", SearchOption.AllDirectories);
//Console.WriteLine(size);
var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
var result0 = ParallelForEachAsync.ParallelSum0(list);
var result1 = ParallelForEachAsync.ParallelSum1(list);
var result2 = ParallelForEachAsync.ParallelSum2(list);
Console.WriteLine($"result0={result0},result1={result1},result2={result2}" );
Console.ReadKey();  
