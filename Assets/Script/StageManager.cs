using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject m_Prefab = default;

    [System.NonSerialized] public List<GameObject> m_maps = new List<GameObject>();
    [SerializeField] int m_startMaps = 3;
    [SerializeField] int m_mapWidth = 14;

    private void Start()
    {
        StartCreate();
    }
    public void StartCreate()
    {
        m_maps.Add(Instantiate(m_Prefab));
        m_maps.Add(Instantiate(m_Prefab, new Vector3(0, 0, m_mapWidth), Quaternion.identity));
        m_maps.Add(Instantiate(m_Prefab, new Vector3(0, 0, m_mapWidth + m_mapWidth), Quaternion.identity));
    }
    public void CreateStage()
    {
        m_startMaps++;
        int createIndex = m_startMaps * m_mapWidth;
        Vector3 mapsWidth = new Vector3(0, 0, createIndex);

        m_maps.Add(Instantiate(m_Prefab, mapsWidth, Quaternion.identity));

        DestroyStage();
    }
    public void DestroyStage()
    {
        Destroy(m_maps[0]);
        m_maps.RemoveAt(0);
    }
}
