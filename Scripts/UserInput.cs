using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class UserInput : MonoBehaviour
{
    public GameObject slot1;
    private float timer;
    private float doubleClickTime =0.3f;
    private Solitaire solitaire;
    private int clickCount = 0;
    public int undo = 0;

    Selectable s2;
    private Selectable s1;

    Move move ;

    // Start is called before the first frame update
    void Start()
    {
        move = UtilityHelper.move;
        solitaire = FindObjectOfType<Solitaire>();
        slot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (clickCount == 1)
        {
            timer += Time.deltaTime;
        }
        if (clickCount == 3)
        {
            timer = 0;
            clickCount = 1;
        }
        if (timer > doubleClickTime)
        {
            timer = 0;
            clickCount = 0;
        }
        GetMouseClick();
    }
    void GetMouseClick()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            // what has been hit ??

            if (hit)
            {
                if (hit.collider.CompareTag("Deck"))
                {
                    //clicked Deck
                    Deck();
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    //clicked Card
                    Card(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Top"))
                {
                    //clicked Top
                    Top(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("bottom"))
                {
                    //clicked bottom
                    Bottom(hit.collider.gameObject);
                }
                

            }

        }


    }
    void Deck()
    {
        
        solitaire.DealFromDeck();
        slot1 = this.gameObject;
    }
    void Card(GameObject selected)
    {
        if (!selected.GetComponent<Selectable>().FaceUp)
        {
            if (!Blocked(selected))
            {
                selected.GetComponent<Selectable>().FaceUp = true;
                slot1 = this.gameObject;
            }
            
        }
        else if (selected.GetComponent<Selectable>().inDeckPile)
        {
            if (!Blocked(selected))
            {
                if (slot1 == selected)
                {


                    if (DoubleClick())
                    {
                        Debug.Log(DoubleClick());
                        AutoStack(selected);
                    }
                }
                else
                {
                    slot1 = selected;
                }

            }
          
        }
        else
        {
            if (slot1 == this.gameObject)
            {
                slot1 = selected;
            }
            else if (slot1 != selected)
            {
                if (Stackable(selected))
                {

                    Stack(selected);
                    Debug.Log("Move slot1");

                }
                else
                {
                    slot1 = selected;
                }

            }

            else if (slot1 == selected) // if the same card is clicked twice
            {
                if (DoubleClick())
                {
                    // attempt auto stack
                    AutoStack(selected);
                }
            }
        }


      
       
        Debug.Log("Card");
    }
    void Top(GameObject selected)
    {
        if (slot1.CompareTag("Card"))
        {
            if (slot1.GetComponent<Selectable>().value==1)
            {
                Stack(selected);
            }
        }
        {

        }
        Debug.Log("Top");
    }
    void Bottom(GameObject selected)
    {
        if (slot1.CompareTag("Card"))
        {

            if (slot1.GetComponent<Selectable>().value == 13)
            {
                Stack(selected);
            }
        }
        Debug.Log("Bottom");
    }
   public void Undoo(GameObject selected)
    {
        Stack(selected);
    }
   public bool Stackable(GameObject selected = null , GameObject secondPlace = null)
    {   
        
        if (!secondPlace)
        {
            this.s2 = selected.GetComponent<Selectable>();
        }
        else
        {
            this.s2 = secondPlace.GetComponent<Selectable>();
        }
        if (!secondPlace)
        {
            this.s1 = slot1.GetComponent<Selectable>();
        }
        else
        {
            this.s1 = selected.GetComponent<Selectable>();
        }
        Selectable s2 = this.s2;
        Selectable s1 = this.s1;
       // print(s1 + "  " + s2);
        if (!s2.inDeckPile)
        {
            if (s2.top)
            {
                if (s1.suit == s2.suit || (s1.value == 1 && s2.suit == null))
                {
                    if (s1.value == s2.value + 1)
                    {
                        return true;
                    }

                }
                else
                {
                    
                    return false;
                   
                }
            }
            else
            {
                if (s1.value == s2.value - 1)
                {
                    bool cardRed = true;
                    bool card2Red = true;
                    if (s1.suit == "C" || s1.suit == "S")
                    {
                        cardRed = false;
                    }
                    if (s2.suit == "C" || s2.suit == "S")
                    {
                        card2Red = false;
                    }
                    if (cardRed == card2Red)
                    {
                        Debug.Log("Not Stackable");
                        return false;
                    }
                    else
                    {
                        Debug.Log("Stackable");
                        return true;
                    }
                }
                else
                {
                    Debug.Log("Not Stackable");
                }
            }
        }
        return false;
    }
  

    public void Stack(GameObject selected)
    {
        undo++;

        move.cardName = slot1.gameObject.name;
        move.lastPos = slot1.gameObject.transform.position;
        move.parent = slot1.gameObject.transform.parent.gameObject;
        move.row = slot1.gameObject.GetComponent<Selectable>().row;
        UtilityHelper.move.AddMove(move);

        Selectable s1 = slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();
        float yoffset = 0.3f;
        if(s2.top || (!s2.top && s1.value == 13))
        {
            yoffset = 0;
        }
        slot1.transform.position = new Vector3(selected.transform.position.x,
            selected.transform.position.y - yoffset,
            selected.transform.position.z-0.01f);
            slot1.transform.parent = selected.transform;

        // remove any card on top & dublicate cards
        if (s1.inDeckPile)
        {
            solitaire.tripsOnDisplay.Remove(slot1.name);
        }
        // set rule on top
        else if(s1.top && s2.top && s1.value == 1)
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = 0;
            solitaire.topPos[s1.row].GetComponent<Selectable>().suit=null;
        }
        else if (s1.top)
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = s1.value - 1;
        }
        else
        {
            solitaire.bottoms[s1.row].Remove(slot1.name);
        }
        // you cannot add cards to the trips pile  
        s1.inDeckPile = false;
        s1.row = s2.row;
        if (s2.top)
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = s1.value;
            solitaire.topPos[s1.row].GetComponent<Selectable>().suit = s1.suit;
            s1.top = true;
        }
        else
        {
            s1.top = false; 
        }
        //reset slot1
        slot1 = this.gameObject;
    }

    bool Blocked (GameObject selected)
    {
        Selectable s2 = selected.GetComponent<Selectable>();
        if (s2.inDeckPile==true)
        {
            if (s2.name == solitaire.tripsOnDisplay.Last())
            {
                //ynf3 t control 
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (s2.name == solitaire.bottoms[s2.row].Last())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    bool DoubleClick()
    {
        if(timer < doubleClickTime && clickCount == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void AutoStack(GameObject selected)
    {
      for(int i = 0 ; i< solitaire.topPos.Length ; i++)
        {
            Selectable stack = solitaire.topPos[i].GetComponent<Selectable>();
            if (selected.GetComponent<Selectable>().value == 1)
            {
                if (solitaire.topPos[i].GetComponent<Selectable>().value == 0)
                {
                    slot1 = selected;
                    Stack(stack.gameObject);
                    break;

                }
            }

            else
            {
                if ((solitaire.topPos[i].GetComponent<Selectable>().suit == slot1.GetComponent<Selectable>().suit) && (solitaire.topPos[i].GetComponent<Selectable>().value == slot1.GetComponent<Selectable>().value - 1))
                {
                    if (noChildren(slot1))
                    {
                        

                        slot1 = selected;
                        string LastCardname = stack.suit + stack.value.ToString();
                        if (stack.value == 1)
                        {
                            LastCardname = stack.suit + "A";
                        }
                        if (stack.value == 11)
                        {
                            LastCardname = stack.suit + "J";
                        }
                        if (stack.value == 12)
                        {
                            LastCardname = stack.suit + "Q";
                        }
                        if (stack.value == 13)
                        {
                            LastCardname = stack.suit + "K";
                        }
                        GameObject lastCard = GameObject.Find(LastCardname);
                        Stack(lastCard);
                        break;
                    }
                }
            }
            
        }
    }
    bool noChildren(GameObject card)
    {
        int i = 0;
        foreach(Transform child in card.transform)
        {
            i++;
        }
        if (i == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


  
    





















}
