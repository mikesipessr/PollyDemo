using Models;

namespace Utilities
{
    public static class GetGoogleWebsite
    {
        /// <summary>
        /// Makes an HTTP request to fetch the Google homepage content.
        /// Uses synchronous execution to return a response wrapped in an OperationResponse.
        /// </summary>
        /// <returns>
        /// An OperationResponse object containing the website content on success, 
        /// or an error message on failure.
        /// </returns>
        public static OperationResponse<string> GetGoogle()
        {
            using var httpClient = new HttpClient();
            try
            {
                // Make a synchronous request to fetch the Google homepage.
                var response = httpClient.GetStringAsync("https://www.google.com").Result;
                return OperationResponse<string>.Ok(response);
            }
            catch (HttpRequestException ex)
            {
                // Return failure response with the exception message.
                return OperationResponse<string>.Fail(ex.Message);
            }
        }
    }
}
