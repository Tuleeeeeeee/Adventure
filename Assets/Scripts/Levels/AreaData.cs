using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Area Data", menuName = "Scriptable Objects/Level/Area Data")]
public class AreaData : ScriptableObject
{
    public string AreaID;
    public string AreaName;
    public List<LevelDataSO> Levels = new List<LevelDataSO>();
}