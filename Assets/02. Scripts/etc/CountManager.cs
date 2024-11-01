using System.Collections;
using TMPro;
using UnityEngine;

public class CountManager : MonoBehaviour
{
    public static CountManager instance;
    private TMP_Text cntText;
    
    void Awake()
    {
        CountManager.instance = this;
        cntText = GetComponent<TMP_Text>();
        cntText.text = "";
    }

    public void GameStart()
    {
        PlayerMove.instance.Moving();
        StartCoroutine(StartCounting());
    }
    
    IEnumerator StartCounting()
    {
        yield return StartCoroutine(Counting());
        cntText.text = "";
        CameraMove.instance.StartMoveCamera();
    }

    IEnumerator Counting()
    {
        int cnt = 3;
        while (cnt > 0)
        {   
            cntText.text = cnt.ToString();
            yield return new WaitForSeconds(1f);
            cnt--;
        }    
        cntText.text = "Start!";
    }
}
