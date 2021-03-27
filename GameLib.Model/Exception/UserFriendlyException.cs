namespace GameLib.Model.Exception
{
    public class UserFriendlyException : System.Exception
    {
        public UserFriendlyException(string message) : base(message){
            
        }
    }
}