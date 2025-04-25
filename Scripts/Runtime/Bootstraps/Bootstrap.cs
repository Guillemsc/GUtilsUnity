using System.Threading;
using System.Threading.Tasks;
using GUtils.Extensions;
using UnityEngine;

namespace GUtilsUnity.Runtime.Bootstraps
{
    public abstract class Bootstrap : MonoBehaviour
    {
        void Start()
        {
            Run(CancellationToken.None).FireAndForget();
        }

        protected abstract Task Run(CancellationToken cancellationToken);
    }
}