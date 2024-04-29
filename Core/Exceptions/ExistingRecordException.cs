namespace Core.Exceptions
{
    public class ExistingRecordException : Exception
    {
        public ExistingRecordException() : base("Username taken!") { }
    }
}
