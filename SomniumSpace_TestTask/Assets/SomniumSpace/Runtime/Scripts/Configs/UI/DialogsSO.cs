using System.Collections.Generic;
using Common.Components.UI;
using Common.UI.Dialogs.BaseDialog;
using UnityEngine;

namespace SomniumSpace.Configs.UI
{
    [CreateAssetMenu(fileName = "DialogsSO", menuName = "ScriptableObjects/UI/Dialogs", order = 1)]
    public class DialogsSO:ScriptableObject
    {
        public List<BaseDialogView> Items;
        public ExtBtn ModalBkg;
    }
}