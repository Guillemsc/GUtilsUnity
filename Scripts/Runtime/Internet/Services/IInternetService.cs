using System.Threading.Tasks;

namespace GUtilsUnity.Internet.Services
{
    /// <summary>
    /// Represents a service for checking internet connectivity.
    /// </summary>
    public interface IInternetService
    {
        /// <summary>
        /// Checks if the device is connected to the internet.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result is true if the device is online; otherwise, false.</returns>
        Task<bool> IsOnline();
    }
}
