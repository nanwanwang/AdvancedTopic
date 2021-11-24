using SpanSample;

//MemorySample.Test();

SpanExample.SpanUsage();
SpanExample.SpanWithNative();
SpanExample.SpanWithStackalloc();

var contentLength = "Content-Length: 132";

Console.WriteLine(SpanExample.GetContentLength(contentLength.AsSpan()));
Console.WriteLine(SpanExample.GetContentLength(contentLength.ToCharArray()));
Console.ReadLine();