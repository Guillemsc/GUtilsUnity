﻿using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.Sequencing.Instructions
{
    public abstract class InstantInstruction : Instruction
    {
        protected sealed override Task OnExecute(CancellationToken cancellationToken)
        {
            OnInstantExecute();

            return Task.CompletedTask;
        }

        protected abstract void OnInstantExecute();
    }
}
