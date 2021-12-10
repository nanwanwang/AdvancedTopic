namespace SourceLearning_WebApi
{
    public class UserService:IUserService
    {
        public string Get()
        {
            return "User";
        }
    }

    public interface IUserService
    {
        string Get();
    }
}