using System.Threading.Tasks;

namespace GUtilsUnity.ApplicationContextManagement
{
    public interface IApplicationContextResumable
    {
        Task Suspend();
        Task PreResume();
        Task Resume();
    }
}
