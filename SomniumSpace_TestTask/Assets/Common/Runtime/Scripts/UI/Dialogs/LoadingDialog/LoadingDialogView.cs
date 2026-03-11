using Common.Components.UI;
using Common.UI.Dialogs.BaseDialog;
using UnityEngine;

namespace Common.UI.Dialogs.LoadingDialog
{
    public class LoadingDialogView : BaseDialogView
    {
        [SerializeField] private ExtTMPText versionTF;
        public void SetVersion(string version) => versionTF.SetText(version);
    }
}