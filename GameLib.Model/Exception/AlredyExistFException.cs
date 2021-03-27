namespace GameLib.Model.Exception
{
    /// <summary>
    /// Exception that, when throwed, will return a 404 to the client
    /// </summary>
    public class AlredyExistFException : System.Exception
    {
        public AlredyExistFException(string message) : base(message){

        }
    }
}