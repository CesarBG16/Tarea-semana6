using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Text timeSurvivedText;
    [SerializeField] private Text enemiesKilledText;
    [SerializeField] private Text levelReachedText;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
       
        timeSurvivedText.text = "Tiempo: " + GameManager.Instance.SurvivalTime.ToString("F1") + "s";
        enemiesKilledText.text = "Enemigos muertos: " + GameManager.Instance.EnemiesKilled;
        levelReachedText.text = "Nivel: " + GameManager.Instance.CurrentLevel;

        mainMenuButton.onClick.AddListener(OnMainMenuPressed);
    }

    private void OnMainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
