namespace ResultCommunication
{
    public interface IExecutionResult
    {
        bool Success { get; }
        object Result { get; }
        Enums.ErrorType? ErrorType { get; }
        string ErrorMetadata { get; }
        string FriendlyMessage { get; set; }
    }
}