using ResourceManagment.Models;

namespace ResourceManagment.Repository
{
    public interface ILoginManger
    {
        LoginresponseModal validateUser(User user);
    }
}
