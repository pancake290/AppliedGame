using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Image displayImage; // 用于显示图片的 Image 组件
    public Sprite[] images;    // 图片数组
    private int currentIndex = 0; // 当前图片的索引

    public void ShowNextImage()
    {
        // 更新索引
        currentIndex = (currentIndex + 1) % images.Length;
        // 更新显示的图片
        displayImage.sprite = images[currentIndex];
    }
}