using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SourceLearning_WebApi
{
    public class EFConfigurationProvider:ConfigurationProvider
    {
        private readonly Action<DbContextOptionsBuilder> _optionsAction;

        public EFConfigurationProvider(Action<DbContextOptionsBuilder> optionsBuilder)
        {
            _optionsAction = optionsBuilder;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            _optionsAction(builder);
            using var dbContext = new AppDbContext(builder.Options);

            dbContext.Database.EnsureCreated();
            if (!dbContext.JsonConfigurations.Any())
            {
                CreateAndSaveDefaultValues(dbContext);
            }

            Data = EFJsonConfigurationParser.Parse(dbContext.JsonConfigurations);
        }

        private static void CreateAndSaveDefaultValues(AppDbContext dbContext)
        {
            dbContext.JsonConfigurations.AddRange(new []
            {
                new JsonConfiguration()
                {
                    Key = "Book",
                    Value = JsonSerializer.Serialize(
                        new BookOptions()
                        {
                            Name = "ef configuration book name",
                            Authors = new List<string>()
                            {
                                "ef configuration book author A",
                                "ef configuration book author B"
                            },
                            Bookmark = new BookmarkOptions()
                            {
                                 Remarks = "ef configuration bookmark Remarks"
                            }
                        })
                }
            });

            dbContext.SaveChanges();
        }
    }

    internal class EFJsonConfigurationParser
    {
        private EFJsonConfigurationParser(){}

        private readonly IDictionary<string, string> _data =
            new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private readonly Stack<string> _context = new();
        private string _currentPath;

        public static IDictionary<string, string> Parse(DbSet<JsonConfiguration> inputs)
            => new EFJsonConfigurationParser().ParseJsonConfigurations(inputs);

        private IDictionary<string, string> ParseJsonConfigurations(DbSet<JsonConfiguration> inputs)
        {
            _data.Clear();

            if (inputs?.Any() != true)
            {
                return _data;
            }

            var jsonDocumentOptions = new JsonDocumentOptions()
            {
                CommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };

            foreach (var input in inputs)
            {
                ParseJsonConfiguration(input, jsonDocumentOptions);
            }

            return _data;
        }

        private void ParseJsonConfiguration(JsonConfiguration input, JsonDocumentOptions options)
        {
            if (string.IsNullOrWhiteSpace(input.Key))
                throw new FormatException($"The key {input.Key} is invalid.");
            var jsonvalue = $"{{\"{input.Key}\":{input.Value}}}";
            using var doc = JsonDocument.Parse(jsonvalue, options);

            if (doc.RootElement.ValueKind != JsonValueKind.Object)
                throw new FormatException($"Unsupported JSON token '{doc.RootElement.ValueKind}' was found.");
            
            VisitElement(doc.RootElement);
        }

        private void VisitElement(JsonElement element)
        {
            foreach (JsonProperty property in element.EnumerateObject())
            {
                EnterContext(property.Name);
                VisitValue(property.Value);
                ExitContext();
            }
        }

        private void VisitValue(JsonElement value)
        {
            switch (value.ValueKind)
            {
                case JsonValueKind.Object:
                    VisitElement(value);
                    break;
                case JsonValueKind.Array:
                    var index = 0;
                    foreach (var arrayElement in value.EnumerateArray())
                    {
                     EnterContext(index.ToString());
                     VisitValue(arrayElement);
                     ExitContext();
                     index++;
                    }

                    break;
                case JsonValueKind.Number:
                    case JsonValueKind.String :
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                    case JsonValueKind.Null:
                    var key = _currentPath;
                    if (_data.ContainsKey(key))
                        throw new FormatException($"A duplicate key '{key}' was found.");
                    _data[key] = value.ToString();
                    break;
                default:
                    throw new FormatException($"Unsupported JSON　token '{value.ValueKind}' was found.");
      
                
            }
        }

        private void EnterContext(string context)
        {
            _context.Push(context);
            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }

        private void ExitContext()
        {
            _context.Pop();
            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }

    }
}