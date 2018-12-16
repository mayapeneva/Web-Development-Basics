namespace Torshia.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net;

    public class DataValidator
    {
        public static bool ValidateObject(object obj)
        {
            var vContext = new ValidationContext(obj);
            var vResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, vContext, vResults, true);
        }

        public static bool ValidateLink(string link)
        {
            var url = WebUtility.UrlDecode(link);
            var validUri = new Uri(url);
            if (string.IsNullOrWhiteSpace(validUri.Scheme) ||
                string.IsNullOrWhiteSpace(validUri.Host) ||
                string.IsNullOrWhiteSpace(validUri.LocalPath) ||
                !validUri.IsDefaultPort)
            {
                return false;
            }

            return true;
        }
    }
}