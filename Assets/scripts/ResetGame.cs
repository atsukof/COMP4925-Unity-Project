using UnityEngine;

//public class ResetGame
//{
//    [Header("Game Objects")]
//    public GameManager gameManager;

//    void Start()
//    {
//        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
//        gameManager.ResetGame();
//    }
//}

public class Stage1Initializer : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.ResetGame();
    }
}