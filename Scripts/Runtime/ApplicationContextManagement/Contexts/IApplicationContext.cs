using System.Threading.Tasks;

namespace GUtilsUnity.ApplicationContextManagement
{
    public interface IApplicationContext
    {
        Task PreEnter();
        Task Enter();
        Task Exit();
    }
}
