using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Common
{
    public class LightInjectContainer : IContainer
    {
        private readonly ServiceContainer _container = new ServiceContainer();
        private static LightInjectContainer instance;

        public static LightInjectContainer Current
        {
            get
            {
                if (instance == null)
                    instance = new LightInjectContainer();
                return instance;
            }
        }

        private LightInjectContainer()
        { }


        public void Register<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>();
        }

        public void Register<TService>()
        {
            _container.Register<TService>();
        }

        public void RegisterInstance<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        public void Register<TService, TArgument>(Expression<Func<TArgument, TService>> factory)
        {
            _container.Register(serviceFactory => factory);
        }

        public TService Resolve<TService>()
        {
            return _container.GetInstance<TService>();
        }

        public bool IsRegistered<TService>()
        {
            return _container.CanGetInstance(typeof(TService), string.Empty);
        }
    }
}
