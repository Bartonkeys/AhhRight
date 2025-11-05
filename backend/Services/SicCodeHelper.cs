using System.Collections.ObjectModel;
using System.Text.Json;

namespace AhhRightApi.Services
{
    public static class SicCodeHelper
    {
        private static readonly IReadOnlyDictionary<string, string> _sicCodes = LoadSicCodes();

        public static IReadOnlyDictionary<string, string> GetSicCodesDictionary() => _sicCodes;

        private static IReadOnlyDictionary<string, string> LoadSicCodes()
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                var assembly = typeof(SicCodeHelper).Assembly;

                var resourceName = assembly.GetManifestResourceNames()
                    .FirstOrDefault(n => n.EndsWith("Resources.sic_codes.json", StringComparison.OrdinalIgnoreCase)
                                      || n.EndsWith(".sic_codes.json", StringComparison.OrdinalIgnoreCase));

                if (resourceName == null)
                    return new ReadOnlyDictionary<string, string>(dict);

                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null)
                    return new ReadOnlyDictionary<string, string>(dict);

                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(json))
                    return new ReadOnlyDictionary<string, string>(dict);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json, options) ?? dict;

                return new ReadOnlyDictionary<string, string>(dict);
            }
            catch (JsonException)
            {
                return new ReadOnlyDictionary<string, string>(dict);
            }
            catch (Exception)
            {
                return new ReadOnlyDictionary<string, string>(dict);
            }
        }
    }
}
