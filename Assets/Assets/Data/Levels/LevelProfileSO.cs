using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsProfileSO", menuName = "SO/LevelsProfileSO" )]
public class LevelProfileSO : ScriptableObject
{
    public List<LevelInfo> levelInfos;
}
