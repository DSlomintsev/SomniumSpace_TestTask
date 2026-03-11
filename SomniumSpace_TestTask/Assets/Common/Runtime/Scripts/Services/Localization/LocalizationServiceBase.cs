using System;
using System.Collections.Generic;

namespace Common.Services.Localization
{
    public abstract class LocalizationServiceBase<T> : BaseService
    {
        private static Dictionary<T, string> _items = new();

        protected override void OnInit()
        {
            base.OnInit();

            AddStrings();
        }

        protected virtual void AddStrings()
        {
            throw new NotImplementedException();
        }

        protected void AddString(T key, string value) => _items.Add(key, value);

        public string GetLocale(T id)
        {
            if (!_items.TryGetValue(id, out string locale))
                throw new KeyNotFoundException($"Localization string {id} not found");

            return locale;
        }
    }
}