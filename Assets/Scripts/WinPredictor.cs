using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using TMPro;

public class WinPredictor : MonoBehaviour
{
    public string cvsFileName;
    [SerializeField] string filePath;
    public int playerScore = 1000;
    public int rivalScore = 400;
    string m = "";
    public TMP_InputField PlayerScoreText;
    public TMP_InputField RivalScoreText;

    private void Start()
    {
        filePath = Application.dataPath + "/" + cvsFileName;
        Debug.Log(filePath);
        Debug.Log(System.IO.File.ReadAllText(filePath));
        m = System.IO.File.ReadAllText(filePath);
    }

    public void WriteDataToCVS()
    {
        StringBuilder sb = new StringBuilder();
        string[] array = new string[5];
        array[0] = "Player";
        array[1] = playerScore.ToString();
        array[2] = "Rival";
        array[3] = rivalScore.ToString();
        array[4] = "1";
        sb.AppendLine(string.Join(",", array));
        StreamWriter outStream = new StreamWriter(filePath);
        outStream.WriteLine(m);
        outStream.WriteLine(sb);
        outStream.Flush();
        outStream.Close();
    }

    public void OnButtonClick()
    {
        if (PlayerScoreText.text == "" || RivalScoreText.text == "")
            return;
        playerScore = int.Parse(PlayerScoreText.text);
        rivalScore = int.Parse(RivalScoreText.text);

        WriteDataToCVS();
    }

    public TextMeshProUGUI ResulText;
    public string LastResult = "";
    public void ShowResult(string data)
    {
        LastResult = data;
        Debug.Log(data);
    }

    private void Update()
    {
        if(LastResult != "")
        {
            if (LastResult == "0##")
            {
                ResulText.text = "Oyuncu kaybeder.";
                Debug.LogError("L O S E");
            }
            else if (LastResult == "1##")
            {
                ResulText.text = "Oyuncu kazanır.";
                Debug.LogError("W I N");
            }
            LastResult = "";
        }
    }
}
