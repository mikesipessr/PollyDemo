namespace Models
{
    /// <summary>
    /// Represents a generic operation response that encapsulates the success state, 
    /// data (if applicable), and error messages (if any).
    /// </summary>
    /// <typeparam name="T">The type of data returned in a successful response.</typeparam>
    public class OperationResponse<T>
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Holds the data returned by the operation, if successful.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// A list of error messages providing details about a failed operation.
        /// </summary>
        public List<string> ErrorMessages { get; set; } = new List<string>();

        /// <summary>
        /// Creates a successful operation response containing the provided data.
        /// </summary>
        /// <param name="data">The data associated with a successful operation.</param>
        /// <returns>An OperationResponse instance indicating success.</returns>
        public static OperationResponse<T> Ok(T data)
        {
            return new OperationResponse<T> { Success = true, Data = data };
        }

        /// <summary>
        /// Creates a failed operation response with one or more error messages.
        /// </summary>
        /// <param name="errors">One or more error messages explaining the failure.</param>
        /// <returns>An OperationResponse instance indicating failure.</returns>
        public static OperationResponse<T> Fail(params string[] errors)
        {
            return new OperationResponse<T> { Success = false, ErrorMessages = errors.ToList() };
        }

        /// <summary>
        /// Merges another OperationResponse into this one, 
        /// combining error messages if the other response indicates failure.
        /// </summary>
        /// <param name="other">Another OperationResponse instance to merge.</param>
        public void Merge(OperationResponse<T> other)
        {
            if (!other.Success)
            {
                Success = false;
                ErrorMessages.AddRange(other.ErrorMessages);
            }
        }
    }
}
