using System.Collections.Generic;

namespace SourceLearning_WebApi
{
    public class BookOptions
    {
        public const string Book = "Book";
        public  string Name { get; set; }
        public  BookmarkOptions Bookmark { get; set; }
        public  List<string> Authors { get; set; }
    }

    public class BookmarkOptions
    {
        public  string Remarks { get; set; }
    }
}