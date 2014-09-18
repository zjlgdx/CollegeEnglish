using Windows.Data.Json;

namespace CollegeEnglish.DataModel
{
    public static class JsonStringExtension
    {
        public static string ToJsonString(this IJsonValue obj, string prefix = null)
        {
            if (obj == null || obj.ValueType == JsonValueType.Null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(prefix))
            {
                return prefix + obj.GetString();
            }

            return obj.GetString();
        }
    }
}