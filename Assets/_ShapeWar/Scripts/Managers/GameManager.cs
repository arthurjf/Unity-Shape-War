using UnityEngine;
using UnityEngine.Events;
using br.com.arthurjf.shapewar.Gameplay.Character;
using UnityEngine.SceneManagement;

namespace br.com.arthurjf.shapewar.Managers
{
    public class GameManager : MonoBehaviour
    {
        public UnityEvent OnGameStart;
        public UnityEvent OnGameOver;

        [SerializeField] private PlayerCharacter m_player;
        [SerializeField] private EnemyCharacter m_enemyPrefab;
        [SerializeField] private float m_enemySpawnInterval = 2f;
        [SerializeField] private WaypointsManager m_waypointsManager;

        private GameStates _state = GameStates.Home;
        private float _nextEnemySpawnTime = 0f;

        public void StartGame()
        {
            _state = GameStates.NewGame;

            ScoreModel.Score = 0;

            OnGameStart?.Invoke();

            InstantiatePlayer();

            _state = GameStates.Playing;
        }

        public void Retry()
        {
            ScoreModel.Score = 0;

            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex);
        }

        private void OnPlayerDied(CharacterBase player)
        {
            _state = GameStates.GameOver;

            OnGameOver?.Invoke();
        }

        private void OnEnemyDied(CharacterBase character)
        {
            var enemyCharacter = character as EnemyCharacter;

            ScoreModel.Score += enemyCharacter.DefeatScoreEarn;
        }

        private void Update()
        {
            switch (_state)
            {
                case GameStates.Playing:
                    SpawnEnemyOnInterval();
                    break;
            }
        }

        private void SpawnEnemyOnInterval()
        {
            if (Time.time >= _nextEnemySpawnTime)
            {
                InstantiateEnemy();

                _nextEnemySpawnTime = Time.time + m_enemySpawnInterval;
            }
        }

        private void InstantiatePlayer()
        {
            m_player.gameObject.SetActive(true);

            m_player.OnDied += OnPlayerDied;
        }

        private void InstantiateEnemy()
        {
            EnemyCharacter enemyInstance = Instantiate(m_enemyPrefab, m_waypointsManager.GetRandomWaypointPosition(), Quaternion.identity);

            enemyInstance.Target = m_player.transform;

            enemyInstance.OnDied += OnEnemyDied;
        }
    }
}
