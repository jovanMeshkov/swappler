using System;

namespace Swappler.Services
{
    public abstract class Service<TEntity, TContext>
    {
        protected TContext Context { get; private set; }
        protected Service()
        {
            this.Context = Activator.CreateInstance<TContext>();
        }
    }
}