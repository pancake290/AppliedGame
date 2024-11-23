using UnityEngine;
using UnityEngine.EventSystems;

public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f); // �Ŵ����
    public float animationSpeed = 0.2f; // �����ٶ�
    private Vector3 originalScale; // ԭʼ��С
    private Vector2 originalPosition; // ԭʼλ��

    private RectTransform rectTransform; // ��Ƭ�� RectTransform
    public RectTransform canvasRectTransform; // Canvas �� RectTransform

    private Rect originalBounds; // ��Ƭ��ԭʼ��Χ
    private bool isHovering = false; // �������Ƿ��ڿ�Ƭ��

    private void Start()
    {
        // ��ʼ����Ƭ�ĳ�ʼ��Ϣ
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.anchoredPosition;

        // ���㲢���濨Ƭ��ԭʼ���緶Χ
        originalBounds = CalculateWorldBounds(rectTransform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // �����뿨Ƭ����ʱ���ƶ��� Canvas ԭ�㲢�Ŵ�
        isHovering = true;
        StopAllCoroutines();
        StartCoroutine(ScaleAndMoveTo(hoverScale, Vector2.zero)); // Canvas ԭ��Ϊ (0, 0)
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ����Ƴ���Ƭʱ�����ͣ״̬Ϊ false
        isHovering = false;

        // �������Ƿ���ԭʼ��Χ֮��
        if (!IsMouseInsideOriginalBounds())
        {
            StopAllCoroutines();
            StartCoroutine(ScaleAndMoveTo(originalScale, originalPosition));
        }
    }

    private bool IsMouseInsideOriginalBounds()
    {
        // ��ȡ������Ļ����
        Vector2 mousePosition = Input.mousePosition;

        // �ж�����Ƿ����ڿ�Ƭԭʼ��Χ��
        return originalBounds.Contains(mousePosition);
    }

    private Rect CalculateWorldBounds(RectTransform rectTransform)
    {
        // ��ȡ��Ƭ�����緶Χ
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);

        return new Rect(
            worldCorners[0].x, // ���½ǵ� X ����
            worldCorners[0].y, // ���½ǵ� Y ����
            worldCorners[2].x - worldCorners[0].x, // ���
            worldCorners[2].y - worldCorners[0].y  // �߶�
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

            // ƽ����ֵ��С��λ��
            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, progress);

            yield return null;
        }

        // ȷ����������ʱ��ȫ����Ŀ��״̬
        rectTransform.localScale = targetScale;
        rectTransform.anchoredPosition = targetPosition;
    }
}