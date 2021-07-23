using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateCards : MonoBehaviour
{

    hint hint;

    public Sprite BackCard;
    public Sprite FaceCard;
    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private Solitaire solitaire;
    private UserInput userInput;
    //private GameObject card;
    // Start is called before the first frame update
    void Start()
    {
        hint = FindObjectOfType<hint>();
        userInput = FindObjectOfType<UserInput>();
        //  Hint(card);
        List<string> deck = Solitaire.GenerateDeck();
        solitaire = FindObjectOfType<Solitaire>();

        int i = 0;
        foreach (string card in deck)
        {
            if (this.name == card)
            {
                FaceCard = solitaire.cardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
        userInput = FindObjectOfType<UserInput>();
    }

    // Update is called once per frame
    void Update()
    {

        if (name == userInput.slot1.name)
        {
            spriteRenderer.color = Color.yellow;
        }
        else
        {
            if (selectable.FaceUp == true)
            {
                spriteRenderer.sprite = FaceCard;
            }
            else
            {

                spriteRenderer.sprite = BackCard;
            }
            if (userInput.slot1)
            {
                if (name == userInput.slot1.name)
                {
                    spriteRenderer.color = Color.yellow;
                }
                else
                {
                    spriteRenderer.color = Color.white;
                }
              
            }
            if (hint.GetComponent<hint>().isStackable == true)
            {
                hint.GetComponent<hint>().renderCards();
                Invoke("ReturnHint", 3);
            }

            if ( hint.GetComponent<hint>().isHintDeck==true )
            {
                hint.GetComponent<hint>().rederDeck();
                // print(hint.GetComponent<hint>().isHintDeck);
                StartCoroutine(OnDisables());
            }
         
         
        }
    }

    void ReturnHint()
    {
        hint.GetComponent<hint>().isStackable =false;
        
        // spriteRenderer.color = Color.white;
    }
    public IEnumerator OnDisables()
    {
        yield return new WaitForSeconds(3f);
     //   hint.GetComponent<hint>().isHintDeck = false;
        hint.GetComponent<hint>().notHintDeck();
        
    }

}
