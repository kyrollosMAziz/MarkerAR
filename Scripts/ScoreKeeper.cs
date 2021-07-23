using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public Selectable [] topStacks ;

    public Text timer;
    float timeVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HasWon())
        {
            Debug.Log("You Won");
        }
        float t = Time.time - timeVal;
        string min = ((int)t / 60).ToString();
        string sec = ((int)(t % 60)).ToString("f2");
        timer.text = min + " : " + sec;
    }
    
    public bool HasWon()
    {
        int i = 0;
        foreach(Selectable topstacks in topStacks)
        {
            i += topstacks.value;
        }
        if (i >= 52)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
