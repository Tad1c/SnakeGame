using System;
using UnityEngine;

[Serializable]
public struct WallStruct
{
    public GameObject wallPrefab;
    public ObjectPool<GameObject> wallPrefabPool;
    public int defaultSize;
    public int maxSize;
}
