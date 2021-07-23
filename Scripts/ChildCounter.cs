using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChildCounter : MonoBehaviour
{
 
    public List<Selectable> myChildCards = new List<Selectable>();

    private void Start()
    {
       
        CountAllChildren();
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CountAllChildren();
            int x = GetNumberOfChildren();
            
            //switch (x)
            //{
            //    case 6:
            //        MiniSize(0.2f,0.8888f);
            //        break;
            //    case 9:
            //        MiniSize(0.1f,0.777777f);
            //        break;
            //    case 12:
            //        MiniSize(0.09f,0.6666f);
            //        break;

            //}
        }

    }
    public void CountAllChildren() 
    {
        this.myChildCards.Clear();
        Selectable[] allCards = FindObjectsOfType<Selectable>();

        List<Selectable> myCards = allCards.OfType<Selectable>().ToList();

        foreach (Selectable card in myCards)
        {
            if (card.CompareTag("Card") && card.myColIndex == this.GetComponent<BottomInfo>().GetBottomIndex())
            {
                this.myChildCards.Add(card);
            }
        }

    }
    public int GetNumberOfChildren() 
    {
        return this.myChildCards.Count();
    }

  
}

//GameObject[] allCards= GameObject.FindGameObjectsWithTag("Card");

//int index = gameObject.GetComponent<BottomInfo>().GetBottomIndex();

//List<GameObject> allCardsList = allCards.OfType<GameObject>().ToList();

//counter = allCards.Where(c=> c.GetComponent<Selectable>().myColIndex==index).Count();

//if (counter > 6 && counter < 7) 
//{
//    if (shrank) 
//    {
//        shrank = false;
//        foreach (Transform child in transform) 
//        {
//            if (child.GetComponent<Selectable>().faceUp) 
//            {
//                child.GetComponent<ShrankDetector>().ShrankAllCards();
//            }
//        }
//    }
//}
//else
//if (counter > 7 && counter < 8)
//{
//    shrank = true;
//    if (shrank)
//    {
//        shrank = false;
//        foreach (Transform child in transform)
//        {
//            if (child.GetComponent<Selectable>().faceUp)
//            {
//                child.GetComponent<ShrankDetector>().ShrankAllCards();
//            }
//        }
//    }
//}else
//if (counter >= 8 && counter < 9)
//{
//    if (shrank)
//    {
//        shrank = false;
//        foreach (Transform child in transform)
//        {
//            if (child.GetComponent<Selectable>().faceUp)
//            {
//                child.GetComponent<ShrankDetector>().ShrankAllCards();
//            }
//        }
//    }
//}else
//if (counter > 9 )
//{
//    if (shrank)
//    {
//        shrank = false;
//        foreach (Transform child in transform)
//        {
//            if (child.GetComponent<Selectable>().faceUp)
//            {
//                child.GetComponent<ShrankDetector>().ShrankAllCards();
//            }
//        }
//    }
//}
//else
//if(numberOfCards > counter)
//{
//    foreach (Transform child in transform)
//    {
//        if (child.GetComponent<Selectable>().faceUp)
//        {
//            child.GetComponent<ShrankDetector>().Deshrank();
//        }
//    }
//}
//if (index == 0)
//Debug.Log(counter + " :  " + numberOfCards);
