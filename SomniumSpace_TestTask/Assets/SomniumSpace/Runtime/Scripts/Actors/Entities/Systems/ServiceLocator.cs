using System;
using System.Collections.Generic;
using Common.Services;

namespace SomniumSpace.Actors.Entities.Systems
{
    public class SystemLocator : Singleton<SystemLocator>
    {
        public static T Get<T>() where T : class, ISystem => Inst.GetSystem<T>();
        public static void Init() => Inst.InitSystems();
        public static void Add<T>(T system) where T : class, ISystem => Inst.Add(system);

        private readonly IDictionary<Type, ISystem> _systems = new Dictionary<Type, ISystem>();

        public void Add(ISystem system)
        {
            if (!_systems.ContainsKey(system.GetType()))
                _systems.Add(system.GetType(), system);
        }

        public T GetSystem<T>() where T : class, ISystem
        {
            var t = typeof(T);
            var system = GetSystem(t);

            if (system != null)
            {
                T casted;
                try
                {
                    casted = system as T;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(string.Format("Cannot cast to type {0}", typeof(T)));
                }

                return casted;
            }

            throw new Exception(string.Format("Cannot find {0} system", typeof(T)));

            return null;
        }

        private object GetSystem(Type t)
        {
            return _systems[t];
        }

        private void InitSystem(Type t, ISystem system)
        {
            _systems.Add(t, system);
        }

        public void InitSystems()
        {
            foreach (var system in _systems.Values)
                system.Init();
        }

        public void DeInit()
        {
            foreach (var system in _systems)
            {
                system.Value.DeInit();
            }

            _systems.Clear();
        }
    }
}
