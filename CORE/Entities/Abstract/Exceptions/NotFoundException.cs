
namespace Core.Entities.Abstract.Exceptions
{
    public abstract class NotFoundException : System.Exception
    {
        protected NotFoundException(string message) : base(message)
        {

        }
    }
}
