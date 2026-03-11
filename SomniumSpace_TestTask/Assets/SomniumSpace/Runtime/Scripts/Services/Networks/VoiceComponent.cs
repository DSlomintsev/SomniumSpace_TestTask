using Photon.Voice.Fusion;
using Photon.Voice.Unity;
using UnityEngine;

namespace SomniumSpace.Services.Networks
{
    [RequireComponent(typeof(FusionVoiceClient))]
    [RequireComponent(typeof(Recorder))]
    public class VoiceComponent:MonoBehaviour
    {
        [SerializeField] private FusionVoiceClient fusionVoiceClient; 
        [SerializeField] private Recorder recorder;
        
        public FusionVoiceClient FusionVoiceClient=>fusionVoiceClient; 
        public Recorder Recorder=>recorder;
    }
}
