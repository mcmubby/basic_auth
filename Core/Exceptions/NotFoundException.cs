namespace Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Username not found!") { }
    }
}
