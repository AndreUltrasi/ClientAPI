namespace Core
{
    public class Output
    {
        public bool IsValid { get; private set; } = true;
        public List<string> ErrorMessages { get; private set; } = new List<string>();
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
            ErrorMessages.Add(errorMessage);
        }

        public void AddResult(object result)
        {
            Result = result;
        }
    }
}
