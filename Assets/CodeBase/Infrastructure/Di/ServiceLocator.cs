using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Di
{
    public class ServiceLocator
    {
        private static readonly Dictionary<Type, IService> _services = new();

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Type type = typeof(TService);

            if (_services.ContainsKey(type))
                return;
            
            _services[type] = implementation;
        }

        public static TService GetService<TService>() where TService : IService =>
            (TService)_services[typeof(TService)];
    }
}