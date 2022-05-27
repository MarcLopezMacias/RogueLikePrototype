using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private int Score;
    public List<int> lastScores;
    public int maxScore;

    public int coins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int value)
    {
        Score += value;
    }

    public void DecreaseScore(int value)
    {
        Score -= value;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public int GetScore()
    {
        return Score;
    }

    public void RecordScore()
    {
        if (Score > maxScore) SetHighestScore();
        lastScores.Add(Score);
        coins += Score;
    }

    private void SetHighestScore()
    {
        Debug.Log("Recording Score");
        GameManager.Instance.GetComponent<UIController>().SetHighScore();
        maxScore = Score;
    }

    public void DecreaseCoins(int amount)
    {
        coins -= amount;
    }

    public void ResetCoins()
    {
        coins = 0;
    }
}
