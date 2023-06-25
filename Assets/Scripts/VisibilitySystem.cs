using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilitySystem : MonoBehaviour
{
    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    public void Hide()
    {
        transform.localScale = Vector3.zero;
    }

    public void Show()
    {
        transform.localScale = initialScale;
    }
}
