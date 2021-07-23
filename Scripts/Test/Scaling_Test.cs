using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling_Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            rectTransform.localScale -= new Vector3(0, .1f, 0);
        }
    }
}
