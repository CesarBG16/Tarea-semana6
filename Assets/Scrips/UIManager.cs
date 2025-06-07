using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Text timeText;
    [SerializeField] private Text killsText;
    [SerializeField] private Text levelText;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTimeUpdated += UpdateTimeUI;
            GameManager.Instance.OnKillsUpdated += UpdateKillsUI;
            GameManager.Instance.OnLevelUpdated += UpdateLevelUI;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTimeUpdated -= UpdateTimeUI;
            GameManager.Instance.OnKillsUpdated -= UpdateKillsUI;
            GameManager.Instance.OnLevelUpdated -= UpdateLevelUI;
        }
    }

    private void UpdateTimeUI(float t)
    {
        timeText.text = "Tiempo: " + t.ToString("F1") + "s";
    }

    private void UpdateKillsUI(int k)
    {
        killsText.text = "Enemigos: " + k;
    }

    private void UpdateLevelUI(int lvl)
    {
        levelText.text = "Nivel: " + lvl;
    }
}

