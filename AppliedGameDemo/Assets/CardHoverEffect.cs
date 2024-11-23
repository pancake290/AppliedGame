using UnityEngine;
using UnityEngine.EventSystems;

public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f); // 放大比例
    public float animationSpeed = 0.2f; // 动画速度
    private Vector3 originalScale; // 原始大小
    private Vector2 originalPosition; // 原始位置

    private RectTransform rectTransform; // 卡片的 RectTransform
    public RectTransform canvasRectTransform; // Canvas 的 RectTransform

    private Rect originalBounds; // 卡片的原始范围
    private bool isHovering = false; // 标记鼠标是否在卡片上

    private void Start()
    {
        // 初始化卡片的初始信息
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.anchoredPosition;

        // 计算并保存卡片的原始世界范围
        originalBounds = CalculateWorldBounds(rectTransform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 鼠标进入卡片区域时，移动到 Canvas 原点并放大
        isHovering = true;
        StopAllCoroutines();
        StartCoroutine(ScaleAndMoveTo(hoverScale, Vector2.zero)); // Canvas 原点为 (0, 0)
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 鼠标移出卡片时标记悬停状态为 false
        isHovering = false;

        // 检查鼠标是否在原始范围之外
        if (!IsMouseInsideOriginalBounds())
        {
            StopAllCoroutines();
            StartCoroutine(ScaleAndMoveTo(originalScale, originalPosition));
        }
    }

    private bool IsMouseInsideOriginalBounds()
    {
        // 获取鼠标的屏幕坐标
        Vector2 mousePosition = Input.mousePosition;

        // 判断鼠标是否仍在卡片原始范围内
        return originalBounds.Contains(mousePosition);
    }

    private Rect CalculateWorldBounds(RectTransform rectTransform)
    {
        // 获取卡片的世界范围
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);

        return new Rect(
            worldCorners[0].x, // 左下角的 X 坐标
            worldCorners[0].y, // 左下角的 Y 坐标
            worldCorners[2].x - worldCorners[0].x, // 宽度
            worldCorners[2].y - worldCorners[0].y  // 高度
        );
    }

    private System.Collections.IEnumerator ScaleAndMoveTo(Vector3 targetScale, Vector2 targetPosition)
    {
        Vector3 startScale = rectTransform.localScale;
        Vector2 startPosition = rectTransform.anchoredPosition;
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime / animationSpeed;

            // 平滑插值大小和位置
            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, progress);

            yield return null;
        }

        // 确保动画结束时完全到达目标状态
        rectTransform.localScale = targetScale;
        rectTransform.anchoredPosition = targetPosition;
    }
}