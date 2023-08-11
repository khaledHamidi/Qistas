using System.Text.RegularExpressions;
namespace Qistas.QAnalyzer
{
    public class QSerializer
    {
        private Dictionary<string, Tuple<string, int>> patterns;

        public QSerializer()
        {
            patterns = new Dictionary<string, Tuple<string, int>>();
        }

        /// <summary>
        /// Adds a pattern for deserialization.
        /// </summary>
        /// <param name="patternKey">Qpattern as string</param>
        /// <param name="id">Use ID to return if deserialized using this pattern</param>
        /// <returns>The QSerializer instance</returns>
        public QSerializer AddPattern(string patternKey, int id)
        {
            string regexPattern = GenerateRegexPattern(patternKey);
            patterns.Add(patternKey, Tuple.Create(regexPattern, id));
            return this;
        }

        // Generates a regular expression pattern from the pattern key
        private static string GenerateRegexPattern(string patternKey)
        {
            string regexPattern = patternKey;
            regexPattern = Regex.Escape(regexPattern); // Escape special characters
            regexPattern = regexPattern.Replace(@"\#", "#"); // Remove the escape character from #

            var match = Regex.Match(regexPattern, @"##(\w+)##");
            if (match.Success)
            {
                string variableName = match.Groups[1].Value;
                regexPattern = regexPattern.Replace(match.Value, $"(?<{variableName}>.+)");
            }
            // Replace pattern variables (#variable#) with named capture groups (?<variable>...)
            regexPattern = Regex.Replace(regexPattern, @"#(\w+)#", @"(?<$1>\w+)");

            return regexPattern;
        }

        /// <summary>
        /// Deserializes the input data based on the registered patterns.
        /// </summary>
        /// <param name="data">The data to deserialize</param>
        /// <returns>A dynamic object representing the deserialized data</returns>
        public dynamic Deserialize(string data)
        {
            foreach (var pattern in patterns)
            {
                string patternKey = pattern.Key;
                string regexPattern = ReplacePatternVariables(pattern.Value.Item1);
                int patternId = pattern.Value.Item2;

                Match match = Regex.Match(data, regexPattern);
                if (match.Success)
                {
                    dynamic result = new System.Dynamic.ExpandoObject();
                    ((IDictionary<string, object>)result).Add("patternsId", patternId);

                    foreach (string groupName in match.Groups.Keys)
                    {
                        if (groupName != "0")
                        {
                            AddProperty(result, groupName, ExtractGroupValue(match.Groups[groupName]));
                        }
                    }

                    return result;
                }
            }

            throw new ArgumentException("Invalid input data.");
        }

        /// <summary>
        /// Serializes the model object based on the specified pattern ID.
        /// </summary>
        /// <param name="model">The model object to serialize</param>
        /// <param name="patternId">The ID of the pattern to use for serialization</param>
        /// <returns>The serialized string</returns>
        public string Serialize(dynamic model, int patternId)
        {
            var selectedPattern = patterns.FirstOrDefault(pair => pair.Value.Item2 == patternId);

            if (selectedPattern.Key != null)
            {
                string patternKey = selectedPattern.Key;
                string serialized = ReplacePatternVariables(patternKey, model);
                if (!string.IsNullOrEmpty(serialized))
                {
                    return serialized;
                }
            }
            else
            {
                throw new ArgumentException("Pattern not found for the given ID value.");
            }

            throw new ArgumentException("Invalid input model.");
        }

        // Replaces the pattern variables in a pattern string with the corresponding values from the model
        private string ReplacePatternVariables(string pattern, dynamic? model = null)
        {
            string replacedPattern = pattern;
            if (model != null)
            {
                var dictionaryModel = model as IDictionary<string, object>;
                if (dictionaryModel != null)
                {
                    foreach (var entry in dictionaryModel)
                    {
                        string variable = $"#{entry.Key}#";
                        if (pattern.Contains(variable))
                        {
                            if (entry.Value is string[] arrayValue)
                            {
                                string joinedValues = string.Join(",", arrayValue);
                                replacedPattern = replacedPattern.Replace(variable, joinedValues);
                            }
                            else
                            {
                                replacedPattern = replacedPattern.Replace(variable, entry.Value.ToString());
                            }
                        }
                    }
                }
            }

            return replacedPattern.Replace("#", "");
        }

        // Adds a property to the dynamic object
        private void AddProperty(dynamic obj, string propertyName, object value)
        {
            var expandoDict = obj as IDictionary<string, object>;
            if (expandoDict == null) return;
            if (expandoDict.ContainsKey(propertyName))
            {
                // Property already exists, convert to array if necessary
                var existingValue = expandoDict[propertyName];
                if (existingValue is List<object> existingArray)
                {
                    existingArray.Add(value);
                }
                else
                {
                    // Create a new array with the existing and new values
                    expandoDict[propertyName] = new List<object> { existingValue, value };
                }
            }
            else
            {
                // Property doesn't exist, add it to the dynamic object
                expandoDict.Add(propertyName, value);
            }
        }

        // Extracts the value from a Group object
        private object ExtractGroupValue(Group group)
        {
            if (group.Captures.Count > 1)
            {
                // Multiple captures, return them as a list
                List<string> captures = new List<string>();
                foreach (Capture capture in group.Captures)
                {
                    captures.Add(capture.Value);
                }
                return captures;
            }
            else
            {
                // Single capture, check if it contains a comma and split it into an array if necessary
                string value = group.Captures[0].Value;
                if (value.Contains(","))
                {
                    return value.Split(',');
                }
                else
                {
                    return value;
                }
            }
        }

        /// <summary>
        /// The patterns registered in the serializer.
        /// </summary>
        public Dictionary<string, Tuple<string, int>> Patterns => patterns;
    }
}