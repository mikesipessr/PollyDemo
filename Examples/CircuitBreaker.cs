using Polly;
using Utilities;
using Models;

namespace PollyDemo
{
    /// <summary>
    /// Demonstrates the use of Polly's Circuit Breaker policy to prevent excessive failures.
    /// </summary>
    public class CircuitBreaker
    {
        /// <summary>
        /// Implements a circuit breaker policy that temporarily stops executing requests 
        /// after a specified number of failures, allowing time for the system to recover.
        /// </summary>
        /// <remarks>
        /// - The circuit will break after **2 consecutive failures**.
        /// - Once broken, it remains open for **10 seconds**, preventing further attempts.
        /// - After 10 seconds, the circuit resets and allows new requests.
        /// </remarks>
        public async Task CircuitBreakerExample()
        {
            // Define a Circuit Breaker policy:
            // - If 2 consecutive HTTP request failures occur, the circuit "opens"
            // - No further requests will be attempted for 10 seconds.
            var circuitBreakerPolicy = Policy
                .Handle<HttpRequestException>()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10));

            // Execute the circuit breaker policy and attempt to fetch the Google homepage.
            OperationResponse<string> circuitBreakerResponse = await circuitBreakerPolicy.ExecuteAsync(() => Task.FromResult(GetGoogleWebsite.GetGoogle()));

            // Check if the request was successful
            if (circuitBreakerResponse.Success)
            {
                Console.WriteLine("Success: " + circuitBreakerResponse.Data.Substring(0, 500));
            }
            else
            {
                Console.WriteLine("Failed to fetch data: " + string.Join(", ", circuitBreakerResponse.ErrorMessages));
            }
        }
    }
}
