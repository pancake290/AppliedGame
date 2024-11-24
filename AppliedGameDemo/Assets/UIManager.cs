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
        turnText.text = "回合数: " + turnCount;
        phaseText.text = "当前阶段: " + (phase == TurnManager.Phase.PlayerPhase ? "玩家阶段" : "敌人阶段");
    }

    public void UpdateEnergy()
    {
        energy.text = TurnManager.Instance.actionPoints.ToString();
        slider.value = (float)TurnManager.Instance.actionPoints / 2.0f;
    }
}
