using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour, IDragHandler
{
    private UserInput userInput;
    public bool FaceUp = false;
    public bool faceUp;

    public bool isCardInShrankArea = false;

    public bool top = false;
    public string suit;
    public int value;
    public int row;
    public bool inDeckPile = false;
    private string valueString;
    public Vector3 cardLocation;
    public Vector3 startLocation;

    public int myColIndex;

    GameObject myParentBottom;

    IEnumerator ShrankDown()
    {
        yield return new WaitForEndOfFrame();
        this.transform.localScale -= new Vector3(0.15f, 0.2f, 0.2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<BottomInfo>())
        {
            myColIndex = gameObject.GetComponent<BottomInfo>().GetBottomIndex();
        }

        if (!this.gameObject.GetComponent<ShrankDetector>())
        {
            gameObject.AddComponent<ShrankDetector>();
        }

        userInput = FindObjectOfType<UserInput>();
        startLocation = gameObject.transform.position;
        if (CompareTag("Card"))
        {
            suit = transform.name[0].ToString();
            for (int i = 1; i < transform.name.Length; i++)
            {
                char c = transform.name[i];
                valueString = valueString + c.ToString();
            }
            if (valueString == "A")
            {
                value = 1;
            }
            if (valueString == "2")
            {
                value = 2;
            }
            if (valueString == "3")
            {
                value = 3;
            }
            if (valueString == "4")
            {
                value = 4;
            }
            if (valueString == "5")
            {
                value = 5;
            }
            if (valueString == "6")
            {
                value = 6;
            }
            if (valueString == "7")
            {
                value = 7;
            }
            if (valueString == "8")
            {
                value = 8;
            }
            if (valueString == "9")
            {
                value = 9;
            }
            if (valueString == "10")
            {
                value = 10;
            }
            if (valueString == "J")
            {
                value = 11;
            }
            if (valueString == "Q")
            {
                value = 12;
            }
            if (valueString == "K")
            {
                value = 13;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        cardLocation = gameObject.transform.position;
        if (FaceUp == true)
        {
            if (faceUp == false)
            {
                faceUp = true;
                StartCoroutine(ShrankDown());
            }
        }

        if (transform.parent)
        {
            Transform myParent = transform.parent;
            if (myParent.gameObject.GetComponent<BottomInfo>())
            {
                myColIndex = myParent.gameObject.GetComponent<BottomInfo>().GetBottomIndex();
            }
            else if (myParent.gameObject.GetComponent<Selectable>())
            {
                myColIndex = myParent.gameObject.GetComponent<Selectable>().myColIndex;
            }
        }
        if (transform.position.y < -3.2f)
        {
            GameObject[] bottoms = GameObject.FindGameObjectsWithTag("bottom");

            List<GameObject> allBottoms = bottoms.OfType<GameObject>().ToList();

            foreach (GameObject bottomInfo in allBottoms) 
            {
                if (bottomInfo.GetComponent<BottomInfo>() && bottomInfo.GetComponent<BottomInfo>().GetBottomIndex() == myColIndex) 
                {
                    bottomInfo.GetComponent<ShrankDetector>().ShrankAllCards();
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3)eventData.delta;
    }
}
