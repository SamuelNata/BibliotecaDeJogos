namespace GameLib.Model.Exception
{
    /// <summary>
    /// Exception that, when throwed, will return a 404 to the client
    /// </summary>
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string message) : base(message){

        }
    }
}