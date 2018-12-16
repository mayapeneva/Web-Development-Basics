namespace RequestParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            var validPaths = new Dictionary<string, List<string>>();
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var tokens = input.Split('/');
                if (!validPaths.ContainsKey(tokens[1]))
                {
                    validPaths[tokens[1]] = new List<string>();
                }

                validPaths[tokens[1]].Add(tokens[2]);
            }

            var request = Console.ReadLine();
            var reqTokens = request.Split('/');
            var method = reqTokens[0].Trim();
            var path = reqTokens[1].Split()[0].Trim();

            var result = new StringBuilder();
            if (validPaths.ContainsKey(path) && validPaths[path].Any(p => p.Equals(method, StringComparison.InvariantCultureIgnoreCase)))
            {
                result.AppendLine("HTTP/1.1 200 OK");
                result.AppendLine("Content-Length: 2");
                result.AppendLine("Content-Type: text/plain");
                result.AppendLine();
                result.AppendLine("OK");
            }
            else
            {
                result.AppendLine("HTTP/1.1 404 NotFound");
                result.AppendLine("Content-Length: 8");
                result.AppendLine("Content-Type: text/plain");
                result.AppendLine();
                result.AppendLine("NotFound");
            }

            Console.WriteLine(result.ToString().Trim());
        }
    }
}