using UnityEngine;
using TMPro;
using br.com.arthurjf.shapewar.Managers;

namespace br.com.arthurjf.shapewar.UI
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [SerializeField] private string m_textFormat = "Score: {0}";
        [SerializeField] private TextMeshProUGUI m_scoreTextMeshProUGUI;

        private void OnEnable()
        {
            ScoreModel.OnScoreChanged += UpdateScoreDisplay;
        }

        private void OnDisable()
        {
            ScoreModel.OnScoreChanged -= UpdateScoreDisplay;
        }

        private void UpdateScoreDisplay(int score)
        {
            m_scoreTextMeshProUGUI.text = string.Format(m_textFormat, score);
        }
    }
}
