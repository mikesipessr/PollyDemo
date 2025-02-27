using Models;
using Polly;
using Polly.Timeout;
using Utilities;

namespace PollyDemo
{
    /// <summary>
    /// Demonstrates the use of Polly's Timeout policy to prevent long-running operations.
    /// Ensures that requests do not exceed a defined execution time.
    /// </summary>
    public class Timeout
    {
        /// <summary>
        /// Implements a timeout policy that cancels an HTTP request if it exceeds the defined duration.
        /// </summary>
        /// <remarks>
        /// - The request will be **canceled if it takes longer than 5 seconds**.
        /// - A TimeoutRejectedException is thrown if the request exceeds the limit.
        /// - The OperationResponse will contain either the result or an error message.
        /// </remarks>
        public async Task TimeoutExample()
        {
            // Define a Timeout Policy:
            // - If the operation exceeds 5 seconds, it is canceled.
            var timeoutPolicy = Policy
                .TimeoutAsync(TimeSpan.FromSeconds(5));

            // Execute the timeout policy and attempt to fetch the Google homepage.
            OperationResponse<string> timeoutResponse;

            try
            {
                timeoutResponse = await timeoutPolicy.ExecuteAsync(() => Task.FromResult(GetGoogleWebsite.GetGoogle()));
            }
            catch (TimeoutRejectedException)
            {
                // Handles the case where the request exceeded the timeout limit
                timeoutResponse = OperationResponse<string>.Fail("Request timed out. Please try again later.");
            }

            // Check if the request was successful
            if (timeoutResponse.Success)
            {
                Console.WriteLine("Success: " + timeoutResponse.Data.Substring(0, 500));
            }
            else
            {
                Console.WriteLine("Failed to fetch data: " + string.Join(", ", timeoutResponse.ErrorMessages));
            }
        }
    }
}
