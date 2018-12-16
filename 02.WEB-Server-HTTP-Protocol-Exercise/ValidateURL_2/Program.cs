using System;

namespace ValidateURL_2
{
    using System.Net;
    using System.Text;

    public class Program
    {
        private const string ERROR_MESSAGE = "Invalid URL";

        public static void Main()
        {
            var input = Console.ReadLine();
            var url = WebUtility.UrlDecode(input);
            var validUri = new Uri(url);

            var result = new StringBuilder();
            if (string.IsNullOrWhiteSpace(validUri.Scheme) ||
                string.IsNullOrWhiteSpace(validUri.Host) ||
                string.IsNullOrWhiteSpace(validUri.LocalPath) ||
                !validUri.IsDefaultPort)
            {
                result.AppendLine(ERROR_MESSAGE);
            }
            else
            {
                result.AppendLine($"Protocol: {validUri.Scheme}");
                result.AppendLine($"Host: {validUri.Host}");
                result.AppendLine($"Port: {validUri.Port}");
                result.AppendLine($"Path: {validUri.LocalPath}");

                if (!string.IsNullOrWhiteSpace(validUri.Query))
                {
                    result.AppendLine($"Query: {validUri.Query.Substring(1)}");
                }

                if (!string.IsNullOrWhiteSpace(validUri.Fragment))
                {
                    result.AppendLine($"Fragment: {validUri.Fragment.Substring(1)}");
                }
            }

            Console.WriteLine(result.ToString().Trim());
        }
    }
}