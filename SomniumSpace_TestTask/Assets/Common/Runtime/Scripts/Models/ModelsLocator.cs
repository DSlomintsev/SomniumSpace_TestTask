using System;
using System.Collections.Generic;
using Common.Services;

namespace Common.Models
{
    public class ModelsLocator : Singleton<ModelsLocator>
    {
        public static T Get<T>() where T : class, IModel => Inst.GetModel<T>();
        public static void Add<T>(T model) where T : class, IModel => Inst.Add(model);

        private readonly IDictionary<Type, IModel> _models = new Dictionary<Type, IModel>();

        public void Add(IModel model)
        {
            if (!_models.ContainsKey(model.GetType()))
                _models.Add(model.GetType(), model);
        }

        public T GetModel<T>() where T : class, IModel
        {
            var t = typeof(T);
            var model = GetModel(t);

            if (model != null)
            {
                T casted;
                try
                {
                    casted = model as T;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(string.Format("Cannot cast to type {0}", typeof(T)));
                }

                return casted;
            }

            throw new Exception(string.Format("Cannot find {0} model", typeof(T)));
            return null;
        }

        private object GetModel(Type t)
        {
            /*if (!_models.ContainsKey(t))
            {
                if (t.IsAssignableFrom(typeof(GameModel)))
                    InitModel(t, new GameModel());
            }*/

            return _models[t];
        }

        private void InitModel(Type t, IModel model)
        {
            _models.Add(t, model);
            //model.Init();
        }

        public void DeInit()
        {
            foreach (var model in _models)
            {
                model.Value.DeInit();
            }

            _models.Clear();
        }
    }
}
