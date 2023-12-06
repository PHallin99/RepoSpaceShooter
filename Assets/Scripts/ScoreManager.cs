using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance;
    public int LastGameScore { get; private set; }
    public int LocalHighScore { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GetLocalHighScore();
        GetLastGameScore();
    }

    private void GetLocalHighScore() {
        LocalHighScore = PlayerPrefs.GetInt(Constants.HighScoreKey, 0);
    }

    private void GetLastGameScore() {
        LastGameScore = PlayerPrefs.GetInt(Constants.LastGameKey, 0);
    }

    public bool CompareToHighScore(int score) {
        UpdateLastGameScore(score);
        if (score <= LocalHighScore) {
            return false;
        }

        UpdateLocalHighScore(score);
        return true;
    }

    private static void UpdateLocalHighScore(int score) {
        PlayerPrefs.SetInt(Constants.HighScoreKey, score);
    }

    private static void UpdateLastGameScore(int score) {
        PlayerPrefs.SetInt(Constants.LastGameKey, score);
    }

    public void ResetScores() {
        PlayerPrefs.DeleteAll();
    }
}