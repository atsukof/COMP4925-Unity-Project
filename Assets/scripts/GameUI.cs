using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI fastestTimeText;

    private void Update()
    {
        scoreText.text = "Score: " + GameManager.Instance.score;
        lifeText.text = "Life: " + GameManager.Instance.life;
        fastestTimeText.text = GameManager.Instance.fastestTime > 0
            ? "Fastest time: " + GameManager.Instance.fastestTime.ToString("F2")
            : "";
    }
}
