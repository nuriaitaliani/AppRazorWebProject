namespace ResultCommunication
{
    public class ExecutionResult : IExecutionResult
    {
        private bool _success = true;
        private object _result = null;
        private Enums.ErrorType? _errorType;
        private string _errorMetadata;

		public bool Success
        {
            get
            { return _success;}
            set
            { _success = value;}
        }

        public object Result
        {
            get
            { return _result;}
            set
            {_result = value;}
        }

        public Enums.ErrorType? ErrorType
        {
            get
            {return _errorType;}
            set
            {_errorType = value;}
        }

        public string ErrorMetadata
        {
            get
            { return _errorMetadata;}
            set
            {_errorMetadata = value;}
        }

        public string FriendlyMessage { get; set; }


        public ExecutionResult(object result = null)
        {
            _result = result;
        }

        public ExecutionResult(Enums.ErrorType errorType, string errorMetadata = null, string friendlyMessage = null)
        {
            _success = false;
            _errorType = errorType;
            _errorMetadata = errorMetadata;
            FriendlyMessage = friendlyMessage;
        }
	}
}