using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private Animator animator;
    private bool isFlipped = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 判断点击的碰撞体是否属于卡片的左右侧
                if (hit.collider.CompareTag("CardLeft"))
                {
                    // 点击了左侧
                    if (!isFlipped)
                    {
                        animator.SetTrigger("FlipToBackLeft");
                    }
                    else
                    {
                        animator.SetTrigger("FlipToFrontLeft");
                    }
                    isFlipped = !isFlipped;
                    //animator.SetBool("IsFlipped", isFlipped);
                }
                else if (hit.collider.CompareTag("CardRight"))
                {
                    // 点击了右侧
                    if (!isFlipped)
                    {
                        animator.SetTrigger("FlipToBackRight");
                    }
                    else
                    {
                        animator.SetTrigger("FlipToFrontRight");
                    }
                    isFlipped = !isFlipped;
                    //animator.SetBool("IsFlipped", isFlipped);
                }
            }
        }
    }
}
