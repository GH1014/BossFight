using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript2 : MonoBehaviour
{
    private Collider2D childCollider;

    private void Start()
    {
        // 자식 객체의 콜라이더 가져오기
        childCollider = GetComponent<Collider2D>();
    }

    // 콜라이더 상태를 변경하는 메서드
    public void ToggleCollider()
    {
        if (childCollider != null)
        {
            childCollider.enabled = !childCollider.enabled;
        }
        else
        {
            Debug.LogWarning("Collider not found on the child object!");
        }
    }
}
