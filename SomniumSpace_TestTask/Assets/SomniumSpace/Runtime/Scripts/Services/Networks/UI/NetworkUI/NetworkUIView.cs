using System;
using Common.UI.Dialogs.BaseDialog;
using Common.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SomniumSpace.Runtime.Scripts.Services.Networks.UI.NetworkUI
{
    public class NetworkUIView : BaseDialogView
    {
        [Header("Status")] [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private TextMeshProUGUI _playerCountText;
        [SerializeField] private TextMeshProUGUI _pingText;

        [Header("Voice")] [SerializeField] private Button _muteButton;
        [SerializeField] private TextMeshProUGUI _muteButtonLabel;

        private Action _mutePressed;

        public void Construct(Action mutePressed)
        {
            _mutePressed = mutePressed;
        }

        public void OnMuteClicked() => _mutePressed.Call();

        public void SetStatus(string status)
        {
            if (_statusText != null)
                _statusText.text = $"Status: {status}";
        }

        public void SetPlayerCount(int count, int maxPlayers)
        {
            if (_playerCountText != null)
                _playerCountText.text = $"Players: {count} / {maxPlayers}";
        }

        public void SetPing(double rttSeconds)
        {
            if (_pingText != null)
                _pingText.text = $"Ping: {(int)(rttSeconds * 1000)} ms";
        }

        public void RefreshMuteLabel(bool isMuted) => _muteButtonLabel.text = isMuted ? "Unmute" : "Mute";
    }
}