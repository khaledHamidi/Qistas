namespace Qistas
{
    public static class QFunctions
    {
        /// <summary>
        /// Echo function to console print like Console.Write. 
        /// in this function will give colors for text
        /// </summary>
        /// <param name="text">text to print</param>
        /// <param name="newLines">count of new lines after printing defual 0</param>
        public static void Echo(string text = "", int lines = 1)
        {
            // Define the word-color mappings
            var wordColors = new Dictionary<string, ConsoleColor>
        {
            { "error", ConsoleColor.Red },
            { "problem", ConsoleColor.Red },
            { "close", ConsoleColor.Red },
            { "important", ConsoleColor.Red },
            { "info", ConsoleColor.Green },
            { "data", ConsoleColor.Green },
            { "open", ConsoleColor.Green },
            { "warning", ConsoleColor.Yellow },
            { "success", ConsoleColor.Cyan },
            { "debug", ConsoleColor.Magenta },
            { "critical", ConsoleColor.DarkRed },
            { "note", ConsoleColor.DarkYellow },
            { ":", ConsoleColor.Blue },
            { ">", ConsoleColor.Blue },
            { ">>", ConsoleColor.Blue },
            { "<", ConsoleColor.Blue },
            { "<<", ConsoleColor.Blue },
            { "\"", ConsoleColor.DarkGray },
            { "{", ConsoleColor.DarkMagenta },
            { "}", ConsoleColor.DarkMagenta },
            { "[", ConsoleColor.DarkCyan },
            { "]", ConsoleColor.DarkCyan },
            { "(", ConsoleColor.DarkYellow },
            { ")", ConsoleColor.DarkYellow },
            { "|", ConsoleColor.Magenta },
            { "/", ConsoleColor.DarkGreen },
            { "-", ConsoleColor.DarkGreen },
            { "----------", ConsoleColor.DarkGreen },
            { "-----", ConsoleColor.DarkGreen }
            // Add more words and colors as needed
        };

            // Split the text into words
            var words = text.Split();

            // Iterate over each word and apply color if needed
            foreach (var word in words)
            {
                var lowercaseWord = word.ToLower();
                if (wordColors.ContainsKey(lowercaseWord))
                {
                    var color = wordColors[lowercaseWord];
                    Console.ForegroundColor = color;
                }
                else if (IsNumber(word))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta; // Custom color for numbers
                }

                // Print the word
                Console.Write(word + " ");

                // Reset the console color
                Console.ResetColor();
            }

            // Add a new line after printing
            for (int i = 0; i < lines; i++)
            {
                Console.WriteLine();

            }
        }

        public static void Echo(object obj, int lines = 1)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            Echo(text: obj.ToString(), lines);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        static bool IsNumber(string word)
        {
            // Check if the word is a number, double, or float
            return double.TryParse(word, out _) || float.TryParse(word, out _);
        }


        /// <summary>
        /// Convert string to type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ChangeType<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }


        public static int ToInt(this string text)
        {
            return int.Parse(text);
        }
        public static double ToDouble(this string text)
        {
            return double.Parse(text);
        }
        public static float ToFloat(this string text)
        {
            return float.Parse(text);
        }
    }
}
