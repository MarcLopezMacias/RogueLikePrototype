using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    /*
     * UI: comptador de vides. Comptador d'enemics a l'escena. 
     * Comptador de punts per enemic 5.
     */

    private Text FrameText;
    private int FrameCount;

    private Text HeartsText;
    private int HeartsCount;

    private Text EnemiesText;
    private int EnemiesCounter;

    private Text ScoreText;
    private int ScoreCounter;

    // Start is called before the first frame update
    void Start()
    {
        FrameCount = 0;
        FrameText = GetComponent<Text>();
        HeartsText = GetComponent<Text>();
        EnemiesText = GetComponent<Text>();
        ScoreText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        FrameCount++;
        FrameText.text = FrameCount.ToString();
    }
}
