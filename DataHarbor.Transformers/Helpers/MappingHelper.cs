using System.Dynamic;

namespace DataHarbor.Transformers.Helpers
{
    public static class MappingHelper
    {
        public static T MapProperties<T>(ExpandoObject source, Dictionary<string, string> mapping) where T : new()
        {
            var result = new T();
            var resultType = typeof(T);

            foreach (var kvp in mapping)
            {
                string sourceProperty = kvp.Key;
                string targetProperty = kvp.Value;

                // Get value from source object
                var sourceProperties = (source as IDictionary<string, object>);
                var containsValue = sourceProperties.TryGetValue(sourceProperty, out var value) ? value : null;

                // Set value to target object if the property exists
                var targetProp = resultType.GetProperty(targetProperty);
                if (targetProp != null && targetProp.CanWrite && containsValue != null)
                {
                    var convertedValue = Convert.ChangeType(value, targetProp.PropertyType);
                    targetProp.SetValue(result, convertedValue);
                }
            }
            return result;
        }

    }
}
