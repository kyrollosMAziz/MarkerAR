using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 lastPos;
    public GameObject parent;
    public bool isFaceUp;
    public string cardName;
    public int row;

    Stack<Move> moves = new Stack<Move>();

    

    private void Awake()
    {
        UtilityHelper.move = this;
    }

    public void AddMove(Move move) 
    {
        moves.Push(move);
    }
    public Stack<Move> GetAllMoves() 
    {
        return moves;
    }
}