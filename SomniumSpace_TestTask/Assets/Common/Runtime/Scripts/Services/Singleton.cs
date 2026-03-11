namespace Common.Services
{
    public class Singleton<T> where T : new()
    {
        private static T _inst;

        protected Singleton()
        {
        }

        public static T Inst
        {
            get
            {
                _inst ??= new T();

                return _inst;
            }
        }

        public void Destroy() => _inst = default;
    }
}
