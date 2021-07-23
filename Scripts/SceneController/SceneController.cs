using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject winPanel;

    public List<ChildCounter> childCounters;
    bool isCounting = false;
    void Start()
    {
        UtilityHelper.sceneController = this;

    }
    public void Win() 
    {
        winPanel.SetActive(true);
    }
    private void Update()
    {
        if (!isCounting&&Input.GetMouseButtonUp(0)) 
        {
            StartCoroutine(StartCounting());            
        }
        
        if (isCounting && childCounters.FindAll(c => c.GetNumberOfChildren() == 0).Count == 7)
        {
            Win();
            StopAllCoroutines();
        }
        Debug.Log(childCounters.FindAll(c => c.GetNumberOfChildren() == 0).Count);
    }
    IEnumerator StartCounting() 
    {
        yield return new WaitForEndOfFrame();
        isCounting = true;

    }

}
