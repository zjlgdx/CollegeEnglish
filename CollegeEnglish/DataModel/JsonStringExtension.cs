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

            var path = string.Empty;
            if (!string.IsNullOrEmpty(prefix))
            {
                path = prefix + obj.GetString();
                path = path.Replace("\\", "/");
                return path;
            }
            path = obj.GetString().Replace("\\", "/");
            return path;
        }
    }
}