using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject controlPad;
    [SerializeField] 
    private GameObject gameScene;
    
    public static GameSceneManager instance;
    
    private void Awake()
    {
        GameSceneManager.instance = this;
        controlPad = GameObject.FindWithTag("ControlPad");
        gameScene = GameObject.FindWithTag("GameScene");
        Initialize();
    }

    void Initialize()
    {
        
    }
    
}
