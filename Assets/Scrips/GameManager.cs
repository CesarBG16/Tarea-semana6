using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance { get; private set; }

    [Header("Game Stats")]
    public float SurvivalTime { get; private set; } = 0f;
    public int EnemiesKilled { get; private set; } = 0;
    public int CurrentLevel { get; private set; } = 1;

    // Evento para avisar a UIManager cuando cambian valores
    public event Action<float> OnTimeUpdated;
    public event Action<int> OnKillsUpdated;
    public event Action<int> OnLevelUpdated;

    private bool isGameOver = false;

    private void Awake()
    {
        // Implementación Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        // Aumentamos el tiempo de supervivencia
        SurvivalTime += Time.deltaTime;
        OnTimeUpdated?.Invoke(SurvivalTime);

        // Cada cierto tiempo / kills podemos aumentar nivel (ejemplo simple)
        // Por cada 20 kills, sube de nivel
        int newLevel = 1 + (EnemiesKilled / 20);
        if (newLevel != CurrentLevel)
        {
            CurrentLevel = newLevel;
            OnLevelUpdated?.Invoke(CurrentLevel);
        }
    }

    // Se llama cuando un enemigo muere
    public void OnEnemyKilled(int points = 1)
    {
        EnemiesKilled += points;
        OnKillsUpdated?.Invoke(EnemiesKilled);
    }

    // Llamar cuando el jugador muere
    public void OnPlayerDeath()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Guardamos stats en PlayerPrefs para leerlos en DefeatController (o podríamos usar el propio Singleton)
        PlayerPrefs.SetFloat("LastTime", SurvivalTime);
        PlayerPrefs.SetInt("LastKills", EnemiesKilled);
        PlayerPrefs.SetInt("LastLevel", CurrentLevel);
        PlayerPrefs.Save();

        // Cambiar a pantalla de derrota
        SceneManager.LoadScene("Defeat");
    }

    // Permite que DefeatController consulte los valores
    private void OnLevelWasLoaded(int levelIndex)
    {
        // Cuando carga Defeat, podemos asignar valores a las propiedades
        if (SceneManager.GetActiveScene().name == "Defeat")
        {
            SurvivalTime = PlayerPrefs.GetFloat("LastTime", 0f);
            EnemiesKilled = PlayerPrefs.GetInt("LastKills", 0);
            CurrentLevel = PlayerPrefs.GetInt("LastLevel", 1);
        }
    }
}