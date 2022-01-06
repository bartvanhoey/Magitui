using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Magitui.Extensions
{
    public static class ObjectExtensions
    {
        public static string ConvertToJson(this object objectToSerialize)
        {
            if (objectToSerialize is null)
                throw new ArgumentNullException();

            return JsonSerializer.Serialize(objectToSerialize, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
