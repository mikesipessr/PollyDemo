namespace PollyDemo
{
    public class Program
    {
        public static void Main()
        {
            // Demo of Polly Retry
            var retry = new Retry();
            retry.RetryDemoWithResult().Wait();

            // Demo of Polly Circuit Breaker
            var circuitBreaker = new CircuitBreaker();
            circuitBreaker.CircuitBreakerExample().Wait();

            // Demo of Polly Timeout
            var timeout = new Timeout();
            timeout.TimeoutExample().Wait();

            // Demo of Polly Rate Limiting
            var rateLimiting = new RateLimiter();
            rateLimiting.RateLimiterExample().Wait();
        }
    }
}
