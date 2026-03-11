namespace Common.Services
{
    public abstract class BaseService : IService
    {
        public bool IsInited { get; private set; }

        /*protected virtual void Construct()
        {

        }*/

        public void Init()
        {
            if (!IsInited)
            {
                IsInited = true;

                OnInit();
            }
        }

        public void DeInit()
        {
            if (IsInited)
            {
                IsInited = false;

                OnDeInit();
            }
        }

        protected virtual void OnInit()
        {
            //Construct();
        }

        protected virtual void OnDeInit()
        {

        }
    }
}
