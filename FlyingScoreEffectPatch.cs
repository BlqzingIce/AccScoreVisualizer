using SiraUtil.Affinity;
using TMPro;
using UnityEngine;
using Zenject;

namespace AccScoreVisualizer
{
    internal class FlyingScoreEffectPatch : IAffinity
    {
        [Inject] private readonly ScoreController _scoreController;
        
        [AffinityPostfix]
        [AffinityPatch(typeof(FlyingScoreEffect), nameof(FlyingScoreEffect.RefreshScore))]
        private void Postfix(int maxPossibleCutScore, int score, ref TextMeshPro ____text, ref Color ____color, ref float ____colorAMultiplier, ref SpriteRenderer ____maxCutDistanceScoreIndicator)
        {
            //fuck base formatting
            ____colorAMultiplier = 1.0f;
            ____maxCutDistanceScoreIndicator.enabled = false;
            ____text.enableWordWrapping = false;
            ____text.richText = true;

            //chain segment
            if (maxPossibleCutScore == 20)
            {
                ____text.text = "•";
                SetColor(ref ____color, 1.0f, 1.0f, 1.0f, 1.0f);
                return;
            }

            //chain head
            if (maxPossibleCutScore < 115)
                score += 115 - maxPossibleCutScore;

            //set text
            if (score == 115)
                ____text.text = "<size=150%><3</size>";
            else if (score >= 111)
                ____text.text = $"<size=130%>{score % 100}</size>";
            else if (score >= 108)
                ____text.text = $"<size=120%>{score % 100}</size>";
            else if (score >= 104)
                ____text.text = $"<size=110%>{score % 100}</size>";
            else if (score >= 101)
                ____text.text = $"{score % 100}";
            else if (score == 100)
                ____text.text = "where acc?";
            else if (score >= 80)
                ____text.text = $"{score}";
            else if (score >= 40)
                ____text.text = "F";
            else
                ____text.text = "trash";

            //set color
            if (score == 115)
                SetColor(ref ____color, 1.0f, 0.0f, 0.75f, 1.0f);
            else if (score >= 111)
                SetColor(ref ____color, 0.5f, 0.0f, 0.5f, 1.0f);
            else if (score >= 108)
                SetColor(ref ____color, 0.08f, 0.71f, 0.45f, 1.0f);
            else if (score >= 104)
                SetColor(ref ____color, 0.0f, 0.28f, 1.0f, 1.0f);
            else if (score >= 100)
                SetColor(ref ____color, 1.0f, 1.0f, 1.0f, 1.0f);
            else
                SetColor(ref ____color, 0.78f, 0.03f, 0.03f, 1.0f);

            /*
            //how does it affect the current score?
            float percentIgnoreDistance = 0.001f;
            float scorePercent = score / 115.0f;
            float currentPercent = _scoreController.multipliedScore / (float)_scoreController.immediateMaxPossibleMultipliedScore;

            if (Mathf.Abs(scorePercent - currentPercent) <= percentIgnoreDistance)
                return;

            if (scorePercent > currentPercent)
                ____text.text += " ↑";
            else
                ____text.text += " ↓";
            */
        }

        private void SetColor(ref Color color, float r, float g, float b, float a)
        {
            color.r = r;
            color.g = g;
            color.b = b;
            color.a = a;
        }
    }
}
