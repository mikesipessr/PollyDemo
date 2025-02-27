using Polly;
using Utilities;
using Models;

namespace PollyDemo
{
    public class Retry
    {
        /// <summary>
        /// Demonstrates the use of Polly's retry policy with exponential backoff.
        /// Attempts to retrieve the Google homepage and retries on failure.
        /// Exponential backoff: progressively increase the wait time between retry attempts after a failure
        /// </summary>
        public async Task RetryDemoWithResult()
        {
            // Define a retry policy that handles HttpRequestException and retries up to 3 times
            // with an exponential backoff (2^attempt seconds).
            // First Retry (attempt 1) → Wait 2¹ = 2 seconds
            // Second Retry(attempt 2) → Wait 2² = 4 seconds
            // Third Retry(attempt 3) → Wait 2³ = 8 seconds
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    );

            // Execute the retry policy and attempt to fetch the Google homepage.
            OperationResponse<string> retryResponse = await retryPolicy.ExecuteAsync(() => Task.FromResult(GetGoogleWebsite.GetGoogle()));

            // Check if the request was successful
            if (retryResponse.Success)
            {
                Console.WriteLine("Success: " + retryResponse.Data.Substring(0, 500));
            }
            else
            {
                Console.WriteLine("Failed to fetch data: " + string.Join(", ", retryResponse.ErrorMessages));
            }
        }
    }
}
