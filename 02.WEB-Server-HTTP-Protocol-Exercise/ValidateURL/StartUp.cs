namespace ValidateURL
{
    using System;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    public class StartUp
    {
        public static void Main()
        {
            var input = Console.ReadLine();
            var decoded = WebUtility.UrlDecode(input);

            var regex = new Regex(@"https*:\/\/[-a-z]+\.[a-z]+:*[0-9]*\/*[a-z]+.*");
            if (!regex.IsMatch(decoded))
            {
                Console.WriteLine("Invalid URL");
                return;
            }

            var tokens = decoded.Split('/');
            var result = new StringBuilder();

            var protocol = tokens[0].TrimEnd(':');
            result.AppendLine($"Protocol: {protocol}");
            if (tokens[2].Contains(':'))
            {
                var smallTokens = tokens[2].Split(':');
                result.AppendLine($"Host: {smallTokens[0]}");

                if ((protocol == "http" && smallTokens[1] == "80")
                    || (protocol == "https" && smallTokens[1] == "443"))
                {
                    result.AppendLine($"Port: {smallTokens[1]}");
                }
                else
                {
                    Console.WriteLine("Invalid URL");
                    return;
                }
            }
            else
            {
                result.AppendLine($"Host: {tokens[2]}");
                result.AppendLine(protocol == "http" ? "Port: 80" : "Port: 443");
            }

            if (tokens.Length == 4)
            {
                if (!tokens[3].Contains('?'))
                {
                    result.AppendLine(tokens[3] == string.Empty ? "Path: /" : $"Path: {tokens[3]}");
                }
                else
                {
                    var lastTokens = tokens[3].Split('?');
                    result.AppendLine($"Path: /{lastTokens[0]}");

                    var queryTokens = lastTokens[1].Split('#');
                    result.AppendLine($"Query: {queryTokens[0]}");
                    result.AppendLine($"Fragment: {queryTokens[1]}");
                }
            }

            if (tokens.Length == 5)
            {
                if (!tokens[4].Contains('?'))
                {
                    result.AppendLine($"Path: /{tokens[3]}/{tokens[4]}");
                }
                else
                {
                    result.AppendLine($"Path: /{tokens[3]}");

                    var lastTokens = tokens[4].Split('?');
                    var queryTokens = lastTokens[1].Split('#');
                    result.AppendLine($"Query: {queryTokens[0]}");
                    result.AppendLine($"Fragment: {queryTokens[1]}");
                }
            }

            if (tokens.Length > 5)
            {
                result.Append($"Path: ");
                for (int i = 3; i < tokens.Length - 2; i++)
                {
                    result.Append($"/{tokens[i]}");
                }
                result.AppendLine();

                var lastTokens = tokens[tokens.Length - 1].Split('?');
                var queryTokens = lastTokens[1].Split('#');
                result.AppendLine($"Query: {queryTokens[0]}");
                result.AppendLine($"Fragment: {queryTokens[1]}");
            }

            Console.WriteLine(result.ToString().Trim());
        }
    }
}