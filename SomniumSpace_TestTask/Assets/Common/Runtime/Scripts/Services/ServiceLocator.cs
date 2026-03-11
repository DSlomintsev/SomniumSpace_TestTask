using System;
using System.Collections.Generic;
using Common.Services.Dialogs;
using UnityEngine;

namespace Common.Services
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        public static T Get<T>() where T : class, IService => Inst.GetService<T>();
        public static void Init() => Inst.InitServices();
        public static void Add<T>(T service) where T : class, IService => Inst.Add(service);

        private readonly IDictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public void Add(IService service)
        {
            if (!_services.ContainsKey(service.GetType()))
                _services.Add(service.GetType(), service);
        }

        public T GetService<T>() where T : class, IService
        {
            var t = typeof(T);
            var service = GetService(t);

            if (service != null)
            {
                T casted;
                try
                {
                    casted = service as T;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(string.Format("Cannot cast to type {0}", typeof(T)));
                }

                return casted;
            }

            throw new Exception(string.Format("Cannot find {0} service", typeof(T)));

            return null;
        }

        private object GetService(Type t)
        {
            if (!_services.ContainsKey(t))
            {
                //if (t.IsAssignableFrom(typeof(DialogService)))
                    //InitService(t, new DialogService());
            }

            return _services[t];
        }

        private void InitService(Type t, IService service)
        {
            _services.Add(t, service);
            //service.Init();
        }

        public void InitServices()
        {
            foreach (var service in _services.Values)
                service.Init();
        }

        public void DeInit()
        {
            foreach (var service in _services)
            {
                service.Value.DeInit();
            }

            _services.Clear();
        }
    }
}
