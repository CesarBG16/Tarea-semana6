using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartPressed);
    }

    private void OnStartPressed()
    {
        SceneManager.LoadScene("Game");
    }
}