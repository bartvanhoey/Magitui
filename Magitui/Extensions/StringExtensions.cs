using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Magitui.Extensions
{
    public static class StringExtensions
    {
        public static T ConvertToType<T>(this string json)
        {
            return string.IsNullOrWhiteSpace(json)
                ? throw new ArgumentNullException()
                : JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
