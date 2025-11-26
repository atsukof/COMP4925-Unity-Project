using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifeText;

    private void Update()
    {
        scoreText.text = "Score: " + GameManager.Instance.score;
        lifeText.text = "Life: " + GameManager.Instance.life;
    }
}
