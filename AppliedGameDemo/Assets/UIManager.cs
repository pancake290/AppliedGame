using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text turnText;
    public Text phaseText;
    public Text energy;
    public Slider slider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // 初始化UI元素
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateEnergy();
    }

    public void UpdatePhaseInfo(int turnCount, TurnManager.Phase phase)
    {
        turnText.text = "Turn:" + turnCount;
        phaseText.text = "Phase " + (phase == TurnManager.Phase.PlayerPhase ? "Player Phase" : "Bugs Phase");
    }

    public void UpdateEnergy()
    {
        energy.text = TurnManager.Instance.actionPoints.ToString();
        slider.value = (float)TurnManager.Instance.actionPoints / 2.0f;
    }
}
