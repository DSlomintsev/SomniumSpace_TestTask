using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using Common.Services;
using Common.Utils;
using Fusion;
using Fusion.Sockets;
using Photon.Voice.Fusion;
using Photon.Voice.Unity;
using SomniumSpace.Configs;
using SomniumSpace.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SomniumSpace.Services.Networks
{
    public class NetworkService : IService, INetworkRunnerCallbacks
    {
        private NetworkConfigSO _networkConfig;
        private NetworkPrefabRef _playerPrefab;

        public event Action<NetworkRunner> OnRunnerStarted;
        public event Action OnRunnerShutdown;
        public event Action<PlayerRef> OnRemotePlayerJoined;
        public event Action<PlayerRef> OnRemotePlayerLeft;
        public event Action<string> OnConnectionError;

        public NetworkRunner Runner { get; private set; }
        public bool IsOnline => Runner != null && Runner.IsRunning;

        private readonly Dictionary<PlayerRef, NetworkObject> _spawnedPlayers = new();

        public void Init()
        {
            var gameModel = ModelsLocator.Get<GameModel>();
            _networkConfig = gameModel.Configs.Network;
            _playerPrefab = gameModel.Configs.Prefabs.Player;
        }

        public void DeInit()
        {
        }

        public async Task JoinOrCreateRoom(string roomName)
        {
            roomName ??= _networkConfig.DefaultRoomName;
            if (Runner != null)
            {
                Debug.LogWarning("[NetworkManager] Runner already active. Call LeaveRoom first.");
                return;
            }

            var gameObject = SpawnUtils.Instantiate(ModelsLocator.Get<GameModel>().Configs.Prefabs.NetworkRunner);
            //var gameObject = new GameObject("NetworkService");

            Runner = gameObject.GetComponent<NetworkRunner>();
            //var voiceClient = gameObject.AddComponent<FusionVoiceClient>();
            //voiceClient.AddRecorder(gameObject.AddComponent<Recorder>());
            //voiceClient.AddSpeaker(gameObject.AddComponent<Speaker>(), );
            Runner.ProvideInput = true;

            Runner.AddCallbacks(this);

            var args = new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = roomName,
                Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
                SceneManager = gameObject.GetComponent<NetworkSceneManagerDefault>(),
                PlayerCount = _networkConfig.MaxPlayers,
            };

            var result = await Runner.StartGame(args);

            if (result.Ok)
            {
                Debug.Log($"[NetworkManager] Joined room '{roomName}' as {Runner.LocalPlayer}.");

                OnRunnerStarted?.Invoke(Runner);
            }
            else
            {
                Debug.LogError($"[NetworkManager] Failed to start: {result.ShutdownReason}");
                OnConnectionError?.Invoke(result.ShutdownReason.ToString());
                Cleanup();
            }
        }

        public async Task LeaveRoom()
        {
            if (Runner == null) return;
            await Runner.Shutdown();
            Cleanup();
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (player != runner.LocalPlayer)
            {
                OnRemotePlayerJoined?.Invoke(player);
                return;
            }

            if (_spawnedPlayers.ContainsKey(player))
            {
                Debug.LogWarning($"[NetworkManager] OnPlayerJoined fired again for local player {player} — ignoring duplicate.");
                return;
            }

            if (_playerPrefab == NetworkPrefabRef.Empty)
            {
                Debug.LogError("[NetworkManager] _playerPrefab is not assigned! " +
                               "Assign it in the NetworkManager Inspector AND register it " +
                               "in Fusion → Network Project Config → Prefabs.");
                return;
            }

            var spawnPos = new Vector3(
                UnityEngine.Random.Range(-3f, 3f),
                0f,
                UnityEngine.Random.Range(-3f, 3f)
            );

            var obj = runner.Spawn(_playerPrefab, spawnPos, Quaternion.identity, player);
            _spawnedPlayers[player] = obj;

            Debug.Log($"[NetworkManager] Local player {player} spawned at {spawnPos}.");
            OnRemotePlayerJoined?.Invoke(player);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (_spawnedPlayers.TryGetValue(player, out var obj))
            {
                runner.Despawn(obj);
                _spawnedPlayers.Remove(player);
            }

            OnRemotePlayerLeft?.Invoke(player);
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason reason)
        {
            Debug.Log($"[NetworkManager] Runner shutdown: {reason}");
            Cleanup();
            OnRunnerShutdown?.Invoke();
        }

        public void OnConnectedToServer(NetworkRunner runner) => Debug.Log("[NetworkManager] Connected to server.");

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
            => Debug.LogWarning($"[NetworkManager] Disconnected: {reason}");

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            Debug.LogError($"[NetworkManager] Connect failed: {reason}");
            OnConnectionError?.Invoke(reason.ToString());
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnInput(NetworkRunner runner, NetworkInput input) { }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }

        private void Cleanup()
        {
            _spawnedPlayers.Clear();

            if (Runner != null)
            {
                GameObject.Destroy(Runner);
                Runner = null;
            }
        }
    }
}
