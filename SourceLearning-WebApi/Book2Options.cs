using System;
using System.ComponentModel.DataAnnotations;

namespace SourceLearning_WebApi
{
    public class Book2Options
    {
        public const string Book = "Book2";
        
        // [Range(1,1000,
        //     ErrorMessage = "必须 {1} <= {0} <= {2}")]
        public  int Id { get; set; }
        // [StringLength(10,MinimumLength = 1,ErrorMessage = "必须{2}<={0} Length<={1}")]
        public string  Name { get; set; }
        public string Author { get; set; }
    }
}