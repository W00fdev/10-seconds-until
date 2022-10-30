using Assets.CodeBase.Logic.UI;
using Logic.Player;
using UnityEngine;

namespace Infrastructure.GameStateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly SceneLoader _sceneLoader;
        private readonly Curtain _curtain;

        private const string PathPlayerResources = "Player/Player";
        private const string PathHudResources = "HUD/HUD";
        private const string InitialSpawnPointTag = "InitialSpawnPoint";

        public LoadLevelState(IStateSwitcher stateSwitcher, SceneLoader sceneLoader, Curtain curtain)
        {
            _stateSwitcher = stateSwitcher;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }


        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnLoaded);
            _curtain.Show();
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindGameObjectWithTag(InitialSpawnPointTag);

            GameObject player = Instantiate(PathPlayerResources, initialPoint.transform.position);
            Instantiate(PathHudResources);

            CameraFollow(player.GetComponent<PlayerFormChanger>().PlayerTransform.gameObject);
            _stateSwitcher.Enter<CoreGameLoopState>();
        }

        private static void CameraFollow(GameObject player)
        {
            CameraFollow cameraFollow = Camera.main.transform.GetComponent<CameraFollow>();
            cameraFollow.Target = player.transform;
        }

        private static GameObject Instantiate(string path)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(playerPrefab);
        }

        private static GameObject Instantiate(string path, Vector3 at)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(playerPrefab, at, Quaternion.identity);
        }
    }
}
