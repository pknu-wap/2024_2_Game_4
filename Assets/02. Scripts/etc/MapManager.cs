using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject selectA;
    public GameObject selectB;
    public GameObject mapA;
    public GameObject mapB;

    public static MapManager instance;
    void Awake()
    {
        MapManager.instance = this;
    }

    public void SelectMapA()
    {
        if (selectA.activeSelf)
        {
            return;
        }
        Debug.Log("SelectA");
        selectA.SetActive(true);
        selectB.SetActive(false);
        LobbyManager.instance.SetMap(mapA);
    }
    
    public void SelectMapB()
    {
        if (selectB.activeSelf)
        {
            return;
        }
        Debug.Log("SelectB");
        selectA.SetActive(false);
        selectB.SetActive(true);
        LobbyManager.instance.SetMap(mapB);
    }
}
