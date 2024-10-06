using UnityEngine;

public class ClosePopUp : MonoBehaviour
{
    public void CloseWindow()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
