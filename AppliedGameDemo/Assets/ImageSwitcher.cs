using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Image displayImage; // ������ʾͼƬ�� Image ���
    public Sprite[] images;    // ͼƬ����
    private int currentIndex = 0; // ��ǰͼƬ������

    public void ShowNextImage()
    {
        // ��������
        currentIndex = (currentIndex + 1) % images.Length;
        // ������ʾ��ͼƬ
        displayImage.sprite = images[currentIndex];
    }
}