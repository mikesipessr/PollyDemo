# PollyDemo

A .NET 8 console application demonstrating resilience patterns with [Polly](https://github.com/App-vNext/Polly) v8. Each example fetches a live webpage and applies a different fault-handling strategy, making it easy to see how each pattern behaves in practice.

## Patterns Demonstrated

| Pattern | Description |
|---|---|
| **Retry with Exponential Backoff** | Automatically retries failed HTTP requests up to 3 times, with increasing delays (2 s, 4 s, 8 s). |
| **Circuit Breaker** | Opens after 2 consecutive failures and stays open for 10 seconds, preventing cascading failures. |
| **Timeout** | Cancels any request that exceeds a 5-second threshold. |
| **Rate Limiter** | Caps traffic at 3 requests per 10-second window to protect downstream services. |

## Project Structure

```
PollyDemo/
├── Program.cs                 # Entry point — runs all four demos sequentially
├── Examples/
│   ├── Retry.cs               # Retry + exponential backoff
│   ├── CircuitBreaker.cs      # Circuit breaker
│   ├── Timeout.cs             # Timeout handling
│   └── RateLimiter.cs         # Rate limiting
├── Models/
│   └── OperationResponse.cs   # Generic success/failure response wrapper
└── Utilities/
    └── GetGoogleWebsite.cs    # HTTP client that fetches google.com
```

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Internet connection (the demos hit https://www.google.com)

## Getting Started

```bash
# Clone the repository
git clone https://github.com/<your-username>/PollyDemo.git
cd PollyDemo

# Build
dotnet build

# Run
dotnet run --project PollyDemo
```

The application will execute each resilience demo in sequence, printing results (or handled errors) to the console.

## How It Works

Each example in the `Examples/` folder creates a Polly **resilience pipeline**, wraps an HTTP call inside it, and handles the outcome:

- **Retry** — catches `HttpRequestException` and retries with exponential backoff, logging each attempt.
- **Circuit Breaker** — tracks consecutive failures and short-circuits requests once a threshold is reached, giving the downstream service time to recover.
- **Timeout** — enforces a maximum duration per request and catches `TimeoutRejectedException` gracefully.
- **Rate Limiter** — enforces a fixed-window rate limit and catches `RateLimitRejectedException` when the quota is exceeded.

All examples return an `OperationResponse<T>` that cleanly separates success data from error messages.

## Built With

- [.NET 8](https://dotnet.microsoft.com/)
- [Polly 8.5](https://github.com/App-vNext/Polly) — resilience and transient-fault-handling library for .NET

## License

This project is provided as-is for educational purposes.
