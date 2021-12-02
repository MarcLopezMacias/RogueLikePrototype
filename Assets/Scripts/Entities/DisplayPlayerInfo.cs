using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerInfo : MonoBehaviour
{

    GameObject Player;

    private Text NameInfo, KindInfo;

    string Name, ResultText;

    PlayerClass Kind;

    float Height, Speed, Weight;

    int CONSTANTEyNOBLE = 39;
    int CONSTANTEyNOBLEdeX = 385;
    int CONSTANTEyNOBLEdeY = 220;

    // Start is called before the first frame update
    void Start()
    {
        NameInfo = GetComponent<Text>();

        Player = GameObject.Find("Female");

        Name = Player.GetComponent<Player>().GetName();
        Kind = Player.GetComponent<Player>().GetKind();

        Height = Player.GetComponent<Player>().GetHeight();
        Speed = Player.GetComponent<Player>().GetSpeed();
        Weight = Player.GetComponent<Player>().GetWeight();

        ResultText = Name + " the " + Kind.ToString();

        NameInfo.text = ResultText;

        Debug.Log(ResultText);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ResultPosition = Player.transform.position;
        ResultPosition.x *= CONSTANTEyNOBLE;
        ResultPosition.y *= CONSTANTEyNOBLE;
        ResultPosition.x += CONSTANTEyNOBLEdeX + ResultText.Length / 2;
        ResultPosition.y += CONSTANTEyNOBLEdeY;
        transform.position = ResultPosition;
    }
}
