using System;

namespace SourceLearning_WebApi
{
    public class DateTimeOptions
    {
        public const string Beijing = "Beijing";
        public const string Tokyo = "Tokyo";
        
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
    }
}