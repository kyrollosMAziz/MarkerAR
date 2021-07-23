using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityHelper 
{
    public static Move move;
    public static UndoMove undo;
    public static Solitaire solitaire ;

    public static List<Selectable> selectables = new List<Selectable>();

    public static SceneController sceneController;
}
