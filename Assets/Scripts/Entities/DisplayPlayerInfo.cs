using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerInfo : MonoBehaviour
{
    GameObject Player;

    private Text NameInfo, KindInfo;

    string Name, ResultText;

    float Height, Speed, Weight;

    int CONSTANTEyNOBLE = 39;
    int CONSTANTEyNOBLEdeX = 385;
    int CONSTANTEyNOBLEdeY = 220;

    // Start is called before the first frame update
    void Start()
    {
        NameInfo = GetComponent<Text>();

        Player = GameObject.Find("Female");

        Name = Player.GetComponent<Player>().playerData.Name;
        Height = Player.GetComponent<Player>().playerData.Height;
        Speed = Player.GetComponent<Player>().playerData.Speed;
        Weight = Player.GetComponent<Player>().playerData.Weight;

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
