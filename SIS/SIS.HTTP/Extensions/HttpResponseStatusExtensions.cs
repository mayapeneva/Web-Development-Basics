namespace SIS.HTTP.Extensions
{
    using Enums;

    public static class HttpResponseStatusExtensions
    {
        public static string GetResponseLine(this HttpResponseStatusCode statusCode)
        {
            //var isValid = Enum.TryParse(statusCode, out HttpResponseStatusCode responseStatusCode);
            //if (!isValid)
            //{
            //    throw new BadRequestException();
            //}

            //return $"{(int) responseStatusCode} {responseStatusCode.ToString()}";

            return $"{(int)statusCode} {statusCode.ToString()}";
        }
    }
}