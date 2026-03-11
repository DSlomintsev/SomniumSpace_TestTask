using System;
using Common.Components.UI;
using Common.UI.Dialogs.BaseDialog;
using Common.Utils;
using TMPro;
using UnityEngine;

namespace SomniumSpace.Services.Networks.MainMenuDialog
{
    public class MainMenuDialogView : BaseDialogView
    {
        [SerializeField] private TMP_InputField roomNameInput;
        [SerializeField] private ExtBtn joinButton;
        [SerializeField] private ExtBtn leaveButton;
        [SerializeField] private TextMeshProUGUI feedbackText;

        private Action<string> _onJoinPressed;
        private Action _onLeavePressed;

        private bool _isOnline;

        public void SetIsOnline(bool isOnline)
        {
            _isOnline = isOnline;
            UpdateView();
        }

        public void Construct(Action<string> onJoinPressed, Action onLeavePressed)
        {
            _onJoinPressed = onJoinPressed;
            _onLeavePressed = onLeavePressed;
        }

        public void OnJoinPressed() => _onJoinPressed.Call(roomNameInput.text);

        public void OnLeavePressed() => _onLeavePressed.Call();

        public void UpdateView()
        {
            joinButton.interactable = !_isOnline;
            leaveButton.interactable = _isOnline;
            roomNameInput.interactable = !_isOnline;
            SetFeedback(_isOnline ? "Connected." : "");
        }

        public void SetInteractable(bool value)
        {
            joinButton.interactable = value;
            leaveButton.interactable = value;
        }

        public void SetFeedback(string message)
        {
            if (feedbackText != null) feedbackText.text = message;
        }

        public void ShowError(string error) => SetFeedback($"Error: {error}");

        public void SetRoomId(string roomId) => roomNameInput.SetTextWithoutNotify(roomId);
    }
}