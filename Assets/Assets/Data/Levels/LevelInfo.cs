using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelInfo
{
    public string levelName = "Level_";
    public int starAmount = 0;

    public Vector2 cellSize = new Vector2(40f, 40f);
    public int rows = 3;
    public int columns = 3;

    public List<int> candysIndex;
    public int cakeIndex;
    public int giftBoxIndex;
}
