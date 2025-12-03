using UnityEngine;

public class Stage1Initializer : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.ResetGame();
    }
}