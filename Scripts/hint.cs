using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class hint : MonoBehaviour
{
    [SerializeField] GameObject image;

    public bool isHintDeck = false;
    public bool isStackable = false;
    [SerializeField] GameObject _D1;
    GameObject _s1, _s2;
    public float timeSpeed = 1;
    public GameObject[] s3;
    public Selectable selectable;
    public Transform[] s2;
    public SpriteRenderer HintspriteRenderer;

    public List<Selectable> selectables = new List<Selectable>();

    [SerializeField] UserInput userInput;
    private bool isS;

    void Start()
    {
        HintspriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void GiveHint()
    {
        selectables.Clear();
        StartCoroutine(CheckAllFaceUpCards());
    }
    public IEnumerator CheckAllFaceUpCards()
    {
        yield return new WaitForSeconds(1);

        s3 = GameObject.FindGameObjectsWithTag("bottom");

        for (int i = 1; i < s3.Length; i++)
        {
            s2 = s3[i].GetComponentsInChildren<Transform>();
            foreach (Transform cardChildren in s2)
            {
                if (cardChildren.GetComponent<Selectable>() && cardChildren.GetComponent<Selectable>().FaceUp == true)
                {
                    selectables.Add(cardChildren.GetComponent<Selectable>());
                    //var c = cardChildren.gameObject.GetComponent<SpriteRenderer>().color;
                    //c = Color.green;

                }
            }
        }
        CheckForStackable();
        yield return new WaitForEndOfFrame();
    }



    public List<BottomInfo> bottomInfos = new List<BottomInfo>();

    void CheckForStackable()
    {

        foreach (BottomInfo bottomInfo in bottomInfos)
        {
            List<Selectable> cardsWithTheSameIndex = selectables.FindAll(s => s.myColIndex == bottomInfo.GetBottomIndex());

            foreach (Selectable card in cardsWithTheSameIndex)
            {
                _s1 = card.gameObject;
                // na5od el krout eli gow bottom mo3ayn = nafs el index ---> neda5lha heya ely yet3emel 3aleha loop 
                foreach (Selectable selectable in selectables)
                {

                    if (selectable.myColIndex != _s1.GetComponent<Selectable>().myColIndex)
                    {
                        _s2 = selectable.gameObject;
                        if (_s2.GetComponent<Selectable>() && _s1.GetComponent<Selectable>() && _s2.GetComponent<Selectable>().FaceUp && _s1.GetComponent<Selectable>().FaceUp)
                        {
                            isStackable = userInput.Stackable(_s1.gameObject, _s2.gameObject);
                            if (isStackable)
                            {
                                return;
                            }
                            isStackable = userInput.Stackable(_s2.gameObject, _s1.gameObject);
                            if (isStackable)
                            {
                                return;
                            }
                        }

                    }
                }
            }   
        }
        StartCoroutine(rederDeck());
    }
    public void renderCards()
    {

        var s1SR = _s1.gameObject.GetComponent<SpriteRenderer>();
        var s2SR = _s2.gameObject.GetComponent<SpriteRenderer>();


        s1SR.color = Color.green;
        s2SR.color = Color.green;

    }
    public IEnumerator rederDeck()
    {
        var d2Sr = _D1.gameObject.GetComponent<SpriteRenderer>();
        d2Sr.color = Color.green;
        yield return new WaitForSeconds(3);
        d2Sr.color = Color.white;
    }
    public void notHintDeck()
    {

        var d2Sr = _D1.gameObject.GetComponent<SpriteRenderer>();
        if (!d2Sr.CompareTag("Deck"))
        {
            d2Sr.color = Color.white;
        }
        else
        {
            return;
        }

    }
    void Update()
    {
        Time.timeScale = timeSpeed;

        // print(isHintDeck);

    }
    public void PauseFun()
    {
        timeSpeed = 0;
        image.SetActive(true);
    }

    public void notPauseFun()
    {
        timeSpeed = 1;
        image.SetActive(false);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }


}
//int index = 0;
//while (!isStackable && index < 7)
//{

//    for (int i = 0; i < selectables.Count; i++)
//    {

//        if (!isStackable)
//        {
//            bool isS = false;
//            _s1 = selectables[index].gameObject;
//            _s2 = selectables[i].gameObject;

//            isS = userInput.Stackable(selectables[index].gameObject, selectables[i].gameObject);
//            if (_s1.gameObject.transform.parent != _s2 && _s2.gameObject.transform.parent != _s1)
//            {
//                isStackable = isS;
//            }
//            print(isStackable + "  No Hint  " + isHintDeck);

//        }
//        else
//        {

//            // isHintDeck = false;
//            return;
//        }
//    }
//    index++;

//}

