using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UndoMove : MonoBehaviour
{
    Stack<Move> moves = new Stack<Move>();
    int faceUpCount;
    GameObject faceUpCard;

    private void Awake()
    {
        UtilityHelper.undo = this;
    }
    public void UndoLastMove()
    {
        bool isUndo = false;

        Debug.Log(UtilityHelper.move.cardName);

        this.moves = UtilityHelper.move.GetAllMoves();
        Move move = moves.Pop();
        Transform parent = move.parent.transform;
        if (!parent.CompareTag("Deck"))
        {
            foreach (Transform child in parent.transform)
            {
                Selectable selectableCard = child.gameObject.GetComponent<Selectable>();
                isUndo = selectableCard.FaceUp;
                if (isUndo)
                {
                    faceUpCard = selectableCard.gameObject;
                    faceUpCount++;
                    if (faceUpCount > 1)
                    {
                        UtilityHelper.move.AddMove(move);
                        faceUpCount = 0;
                        return;
                    }
                }
            }
        }
        faceUpCount = 0;
        if (faceUpCard && faceUpCard.name != move.cardName)
        {
            faceUpCard.GetComponent<Selectable>().FaceUp = false;
        }
        GameObject ob = GameObject.Find(move.cardName);

        int index = ob.gameObject.GetComponent<Selectable>().row;
        UtilityHelper.solitaire.bottoms[move.row].Add(ob.name);

        ob.transform.position = move.lastPos;
        ob.transform.parent = move.parent.transform;
        UtilityHelper.move.AddMove(move);
    }

}
/*
 1- el kart c1 kan makano kaza w el kart c 2 kan makano kaza 
kaza list btet8ayr
ak\\akaza parent w child

*/