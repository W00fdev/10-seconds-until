using Infrastructure.Services.SoundService;
using UnityEngine.SceneManagement;
using System.Collections;
using Infrastructure;
using UnityEngine;

namespace Logic.Managers
{
    public class GameDirector : MonoBehaviour
    {
        [SerializeField] private GameObject _wonTitle;
        [SerializeField] private GameObject _loseTitle;
        [SerializeField] private GameObject _firstTitle;

        [SerializeField] private AudioListener _audioListener;

        [SerializeField] private EnemySpawnManager _spawnDirector;
        private ISoundService _soundService;

        public bool GameEnd = false;
        public bool GameStarted = false;

        private void Awake()
        {
            _soundService = AllServices.Container.Single<ISoundService>();
            StartCoroutine(StartGameCoroutine());
        }

        public void StartGame()
        {
            _firstTitle.SetActive(false);
            //_spawnDirector.StartGame();

            _soundService.PlaySoundOfType(SoundType.GAMELOOP);

            GameStarted = true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (GameStarted == false)
                    StartGame();
            }
        }

        public void Win()
        {
            GameEnd = true;

            _wonTitle.SetActive(true);

            _soundService.PlaySoundOfType(SoundType.GAMEVICTORY);


            StartCoroutine(RestartScene());
        }

        public void Lose()
        {
            GameEnd = true;
            _loseTitle.SetActive(true);

            _soundService.PlaySoundOfType(SoundType.GAMELOSE);

            StartCoroutine(RestartScene());
        }

        IEnumerator RestartScene()
        {
            yield return new WaitForSeconds(5f);

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        IEnumerator StartGameCoroutine()
        {
            yield return new WaitForSeconds(3f);

            if (GameStarted == false)
                StartGame();
        }
    }
}

