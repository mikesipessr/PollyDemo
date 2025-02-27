using Polly;
using Polly.RateLimit;
using Utilities;
using Models;

namespace PollyDemo
{
    /// <summary>
    /// Demonstrates the use of Polly's Rate Limiter policy to control request rates.
    /// Ensures that requests do not exceed a specified limit within a given time window.
    /// </summary>
    public class RateLimiter
    {
        /// <summary>
        /// Implements a rate limiter policy that restricts the number of requests allowed within a specific time window.
        /// </summary>
        /// <remarks>
        /// - Allows up to **3 requests per 10 seconds**.
        /// - Additional requests within the window will be **rejected**.
        /// - The OperationResponse will contain either the result or an error message.
        /// </remarks>
        public async Task RateLimiterExample()
        {
            // Define a Rate Limiter Policy:
            // - Allows up to 3 executions per 10 seconds.
            // - Excess requests within the time window will be rejected.
            var rateLimiterPolicy = Policy
                .RateLimitAsync(3, TimeSpan.FromSeconds(10));

            // Execute the rate limiter policy and attempt to fetch the Google homepage.
            OperationResponse<string> rateLimiterResponse;

            try
            {
                rateLimiterResponse = await rateLimiterPolicy.ExecuteAsync(() => Task.FromResult(GetGoogleWebsite.GetGoogle()));
            }
            catch (RateLimitRejectedException)
            {
                // Handles the case where the request limit has been exceeded
                rateLimiterResponse = OperationResponse<string>.Fail("Rate limit exceeded. Please wait and try again.");
            }

            // Check if the request was successful
            if (rateLimiterResponse.Success)
            {
                Console.WriteLine("Success: " + rateLimiterResponse.Data.Substring(0, 500));
            }
            else
            {
                Console.WriteLine("Failed to fetch data: " + string.Join(", ", rateLimiterResponse.ErrorMessages));
            }
        }
    }
}
