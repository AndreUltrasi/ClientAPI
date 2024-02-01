namespace Core
{
    public class Output
    {
        public bool IsValid { get; private set; } = true;
        public string ErrorMessage { get; private set; } = string.Empty;
        public object Result { get; private set; } = null!;

        public Output()
        {
        }

        public Output(object result)
        {
            Result = result;
        }

        public void AddErrorMessage(string errorMessage)
        {
            IsValid = false;
            ErrorMessage = errorMessage;
        }
    }
}
