using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplayManager : MonoBehaviour
{
    public GameObject cardPrefab;       // 卡片的3D模型预制体
    public Transform cameraTransform;  // 摄像机的引用

    private GameObject currentCard;    // 当前展示的卡片
    private bool isEnglish = true;     // 当前语言状态


    public void ShowCard(GameObject obj)
    {
        if (currentCard != null) return; // 防止重复生成

        // 动态生成卡片
        currentCard = Instantiate(obj.GetComponent<CardObj>().cardObj);
        currentCard.transform.position = cameraTransform.position + cameraTransform.forward * 6f; // 悬浮在摄像机前方
        //currentCard.transform.rotation = Quaternion.LookRotation(cameraTransform.forward);       // 面向摄像机
    }

    public void HideCard()
    {
        if (currentCard != null)
        {
            Destroy(currentCard); // 销毁当前卡片
            currentCard = null;
        }
    }

    public void ToggleLanguage()
    {
        if (currentCard == null) return;

        isEnglish = !isEnglish;
        string currentType = currentCard.name; // 假设卡片名字包含当前类型
        //UpdateCardTexture(currentType, currentCard.name.Contains("Enemy"));
    }
}
