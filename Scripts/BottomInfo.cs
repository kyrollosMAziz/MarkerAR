using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomInfo : MonoBehaviour
{
    [SerializeField]
    int bottomIndex;

    public int numberOfChildren = 0;

    public ChildCounter childCounter;

    private void Start()
    {
        childCounter = GetComponent<ChildCounter>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0)) 
        {
            numberOfChildren = GetComponent<ChildCounter>().GetNumberOfChildren();
        }
    }

    public int GetBottomIndex() 
    {
        return bottomIndex;
    }
}
