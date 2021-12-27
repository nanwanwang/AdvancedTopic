
var queue = new PriorityQueue<string,int>();
queue.Enqueue("1",4);
queue.Enqueue("2",2);
queue.Enqueue("3",3);

Console.WriteLine(queue.Peek());
Test(null);


static void Test(string str)
{
    ArgumentNullException.ThrowIfNull(str);
    Console.WriteLine(str);
}
