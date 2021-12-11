using System.ComponentModel.DataAnnotations;

namespace SourceLearning_WebApi
{
    public class JsonConfiguration
    {
        [Key]
        public  string Key { get; set; }
        
        public  string Value { get; set; }
    }
}