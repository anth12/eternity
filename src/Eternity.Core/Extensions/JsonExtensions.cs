using Newtonsoft.Json;

namespace Eternity.Core.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }

        public static TType ParseJson<TType>(this string source)
        {
            return JsonConvert.DeserializeObject<TType>(source);
        }
    }
}
