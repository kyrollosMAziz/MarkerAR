using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Solitaire : MonoBehaviour
{
    public GameObject deckButton;
    public GameObject[] bottomPos;
    public GameObject[] topPos;

    public List<string>[] bottoms;
    public List<string>[] tops;

    public List<string> bottom0 = new List<string>();
    public List<string> bottom1 = new List<string>();
    public List<string> bottom2 = new List<string>();
    public List<string> bottom3 = new List<string>();
    public List<string> bottom4 = new List<string>();
    public List<string> bottom5 = new List<string>();
    public List<string> bottom6 = new List<string>();

    // click 3 Cards
    public List<string> tripsOnDisplay = new List<string>();
    public List<List<string>> deckTrips = new List<List<string>>();

    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public List<string> HintCard;
    //create suits
    public static string[] suits = {"C", "D","H","S"};
    //create values of suits
    public static string[] values = { "A", "2", "3","4","5","6","7","8","9","10","J","Q","K" };
    public List<string> deck;
    public List<string> discardPile = new List<string>();

    private int trips;
    private int tripsRemainder;
    private int deckLocation;
    public int numCards = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        UtilityHelper.solitaire = this;
    }
    void Start()
    {
        bottoms = new List<string>[] { bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6 };
        playCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void playCard()
    {
        deck = GenerateDeck();
        Shuffle(deck);
        SolitairSort();
        StartCoroutine(SolitaiarDeal());
        SortDeckIntroTrips();
    } 
    //Generate Cards
    public static List<string> GenerateDeck () {

        List<string> newDeck = new List<string>();
        foreach(string s in suits)
        {
            foreach(string v in values)
            {
                newDeck.Add(s + v);
                
            }
        }
        return newDeck;
      //  print(newDeck);
    }
    //make random cards
    void Shuffle<T> (List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    // spawn cards according random decks in bottom  
    IEnumerator SolitaiarDeal()
    {
        for (int i = 0; i < 7; i++)
        {
            float yoffset = 0;
            float zoffset = 0.05f;
            foreach (string card in bottoms[i])
            {
                yield return new WaitForSeconds(0.01f);
                GameObject newCard = Instantiate(
                    cardPrefab, new Vector3(
                       bottomPos[i].transform.position.x,
                        bottomPos[i].transform.position.y - yoffset,
                        bottomPos[i].transform.position.z - zoffset),
                    Quaternion.identity, bottomPos[i].transform);
                newCard.name = card;
                newCard.GetComponent<Selectable>().row=i;

                 // make last card Face 
                if (card==bottoms[i][bottoms[i].Count - 1])
                {
                  
                       // print(card);
                    
                    
                    newCard.GetComponent<Selectable>().FaceUp = true;
                }
             
                yoffset = yoffset + 0.3f;
                zoffset = zoffset + 0.05f;
                discardPile.Add(card);
            }
        }
        foreach(string card in discardPile)
        {
            if (deck.Contains(card))
            {
                deck.Remove(card);
            }
        }
        discardPile.Clear();
    }
    
    void SolitairSort()
    {
        for(int i = 0; i < 7; i++)
        {
            for(int j = i; j < 7; j++)
            {
                
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
               
            }
        }
    }
    // ???????????????? 
    public void SortDeckIntroTrips()
    {
        trips = deck.Count /numCards ;
        tripsRemainder = deck.Count % numCards;
        deckTrips.Clear();
        int modifire = 0;
        for (int i =0 ; i < trips; i++)
        {
            List<string> myTrips = new List<string>();
          for(int j=0; j < numCards; j++)
            {
                myTrips.Add(deck[j + modifire]);
            }
            deckTrips.Add(myTrips);
            modifire = modifire + numCards;
            //Debug.Log("not = 0 :" + tripsRemainder);
        } 

        //??????????????????????
        if(tripsRemainder != 0)
        {
           // Debug.Log("not = 0 :"+tripsRemainder);
            List<string> myRemainder = new List<string>();
            modifire = 0;

            for(int k = 0; k < tripsRemainder; k++)
            {
                myRemainder.Add(deck[deck.Count - tripsRemainder + modifire]);
                modifire++;
            }
            deckTrips.Add(myRemainder);
            trips++;
        }
        deckLocation = 0;

    }
    public void DealFromDeck()
    {
        foreach(Transform child in deckButton.transform)
        {
            if (child.CompareTag("Card"))
            {
                deck.Remove(child.name);
                discardPile.Add(child.name);
                Destroy(child.gameObject);
            }
            
        }

        if (deckLocation < trips)
        {
            tripsOnDisplay.Clear();
            float xoffset = 2.5f;
            float zoffset = -0.2f;
            foreach (string card in deckTrips[deckLocation])
            {
                GameObject newTopCard = Instantiate(cardPrefab, new Vector3(
                    deckButton.transform.position.x+xoffset,
                    deckButton.transform.position.y,
                    deckButton.transform.position.z +zoffset
                    ),Quaternion.identity,deckButton.transform);
                xoffset = xoffset + 0.5f;
                zoffset = zoffset - 0.2f;
                newTopCard.name = card;
                tripsOnDisplay.Add(card);
                newTopCard.GetComponent<Selectable>().FaceUp=true;
                newTopCard.GetComponent<Selectable>().inDeckPile = true;
            }
            deckLocation++;

        }
        else
        {
            ReStackTopDeck();
        }
    }

    void ReStackTopDeck()
    {
        deck.Clear();
        foreach(string card in discardPile)
        {
            deck.Add(card);
        }
        discardPile.Clear();
        SortDeckIntroTrips();
    }
}
