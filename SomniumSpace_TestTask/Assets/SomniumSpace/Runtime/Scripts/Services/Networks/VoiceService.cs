// VoiceManager.cs
// Manages Photon Voice 2 in a Fusion project using FusionVoiceClient.
//
// SETUP:
//   1. Add FusionVoiceClient to the NetworkManager GameObject.
//   2. Set AppIdVoice in Photon → Voice → Settings (PhotonAppSettings asset).
//   3. Each Player prefab needs a Speaker component on a child named "VoiceSpeaker".
//      FusionVoiceClient links remote Recorders to Speakers automatically.
//
// FusionVoiceClient owns its own Recorder internally — do NOT add a separate
// Recorder component. Access it via FusionVoiceClient.PrimaryRecorder.

using Common.Models;
using Common.Services;
using Common.Utils;
using Photon.Voice.Fusion;
using SomniumSpace.Models;
using UnityEngine;

namespace SomniumSpace.Services.Networks
{
    [RequireComponent(typeof(FusionVoiceClient))]
    public class VoiceService : IService
    {
        private FusionVoiceClient _voiceClient;

        public void Start()
        {
            return;
            var gameObject = new GameObject("VoiceClient");
            gameObject.AddComponent<FusionVoiceClient>();
            //var voiceComponent = SpawnUtils.Instantiate(ModelsLocator.Get<GameModel>().Configs.Prefabs.VoiceComponent);
            //_voiceClient = voiceComponent.GetComponent<FusionVoiceClient>();
            ConfigureRecorder();
        }

        private void ConfigureRecorder()
        {
            var recorder = _voiceClient.PrimaryRecorder;

            if (recorder == null)
            {
                Debug.LogWarning("[VoiceManager] PrimaryRecorder is null — " +
                                 "assign a Recorder to FusionVoiceClient.PrimaryRecorder in the Inspector.");
                return;
            }

            recorder.TransmitEnabled     = true;
            recorder.VoiceDetection      = true;

            Debug.Log("[VoiceManager] Recorder configured.");
        }

        public void SetMuted(bool muted)
        {
            var recorder = _voiceClient.PrimaryRecorder;
            if (recorder == null) return;

            recorder.TransmitEnabled = !muted;
            Debug.Log($"[VoiceManager] Microphone {(muted ? "muted" : "active")}.");
        }

        public bool IsMuted => _voiceClient.PrimaryRecorder != null
                               && !_voiceClient.PrimaryRecorder.TransmitEnabled;

        public void Init()
        {
            
        }

        public void DeInit()
        {
        }
    }
}
