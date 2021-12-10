using System;

namespace SourceLearning_WebApi
{
    public class DependencyService1:IDisposable
    {
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Console.WriteLine("DependencyService1 dispose");
        }
    }
    
    public class DependencyService2:IDisposable
    {
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Console.WriteLine("DependencyService2 dispose");
        }
    }
    
    public class DependencyService3:IDisposable,IService
    {
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Console.WriteLine("DependencyService3 dispose");
        }
    }
    public class DependencyService4:IDisposable,IService
    {
        public  string Name { get; set; }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Console.WriteLine("DependencyService4 dispose");
        }
    }
}