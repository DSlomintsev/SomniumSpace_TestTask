using Common.Services;
using Common.Services.Dialogs;

namespace Common.UI.Dialogs.BaseDialog
{
    public class BaseDialogMediator
    {
        public string Id;
        public bool IsInited;

        protected virtual void Construct() { }
        
        public virtual void Init(BaseDialogView view)
        {
            Construct();

            IsInited = true;
        }
        
        public virtual void DeInit()
        {
            IsInited = false;
        }
        
        public void CloseDialog() => ServiceLocator.Get<DialogService>().CloseDialog(this);
    }
}