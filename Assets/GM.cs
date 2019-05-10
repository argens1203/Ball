using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class GM : MonoBehaviour {

    private int totalScore;
    public int scoreTotal_pub;
    public Transform bar;
    public activeShoot activesho;
    public float x;
    public float len;
    private float chargeTime;
    public float minLen;
    public float maxLen;
    public TextMesh score_text;
    public TextMesh hiscore_text;

    public TextMesh timer_text;
    public TextMesh endMsg;
    private float countdown = 3;
    private bool masterStart = false;
    private bool start = false;

    private float timer = 30;
    public Camera cam;
    int [] highscores = new int [5];
    string[] names = new string[5];
    string playername = "";
    // Use this for initialization
    void Start () {
        totalScore = 0;
        chargeTime = activesho.chargeTime;
        score_text.text = 0.ToString();
        score_text.transform.position = new Vector3(-8, score_text.transform.position.y, score_text.transform.position.z);
        mainMenu();
        getHighscore();
    }
	
    void getHighscore()
    {
        StreamReader sr = new StreamReader("./Highscore");
        string temp;
        int value;
        for (int i = 0; i < 5; i++)
        {
            temp = sr.ReadLine();
            names[i] = temp;
            temp = sr.ReadLine();
            int.TryParse(temp, out value);
            highscores[i] = value;
        }
        hiscore_text.text = names[0] + "   " + highscores[0] + "\n" + names[1] + "   " + highscores[1] + "\n" + names[2] + "   " + highscores[2] + "\n" + names[3] + "   " + highscores[3] + "\n" + names[4] + "   " + highscores[4];
        return;
    }

    void updateHighscore ()
    {
        using (StreamWriter sw = new StreamWriter("./Highscore"))
        {
            for (int i = 0; i < 5; i++)
            {
                sw.Write(names[i]);
                sw.Write("\n");
                sw.Write(highscores[i]);
                sw.Write("\n");
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if (!masterStart)
            return;
        if (!start)
        {
            timer_text.text = countdown.ToString("#.00");
            if (countdown > 0) countdown -= Time.deltaTime;
            if (countdown < 0)
            {
                start = !start;
                activesho.activate();
            }
            return;
        }
        timer_text.text = timer.ToString("#.00");
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            endGame();
        }
	}

    public void setname (string name)
    {
        playername = name;
    }

    public void addHighscore ()
    {
        string name = playername;
        if (totalScore > highscores [0])
        {
            for (int i = 3; i >= 0; i--)
            {
                highscores[i + 1] = highscores[i];
                names[i + 1] = names[i];
            }
            highscores[0] = totalScore;
            names[0] = name;
        }
        else
        {
            int rank = 1;
            while (rank < 5 && totalScore <= highscores[rank])
            {
                rank += 1;
            }
            if (rank < 5)
            {
                for (int i = 3; i >= rank; i--)
                {
                    highscores[i + 1] = highscores[i];
                    names[i + 1] = names[i];
                }
                highscores[rank] = totalScore;
                names[rank] = name;
            }
        }
        updateHighscore();
        getHighscore();
    }

    private void endGame()
    {
        activesho.deactivate();
        masterStart = false;
        string endText = "";
        int pos = 0;
        if (totalScore > highscores[0])
        {
            cam.transform.position = new Vector3(-1200, cam.transform.position.y, cam.transform.position.z);
            return;
        }
        if (totalScore < 100)
        {
            endText = "Your score is low. Try again.";
            pos = -2274;
        }
        else
        {
            endText = "Nice work!";
            pos = -2133;
        }
        cam.transform.position = new Vector3(-2063, cam.transform.position.y, cam.transform.position.z);
        endMsg.text = endText;
        endMsg.transform.position = new Vector3(pos, endMsg.transform.position.y, endMsg.transform.position.z);
        return;
    }

    public void showTime (float ratio)
    {
        float length = ratio * (maxLen - minLen);
        
        bar.position = new Vector3 ((length - maxLen) / 2, bar.position.y, bar.position.z);
        bar.localScale = new Vector3(length, bar.localScale.y, bar.localScale.z);
        x = (length - maxLen) / 2;
        len = length;
    }

    public void addScore (int score)
    {
        totalScore += score;
        scoreTotal_pub = totalScore;
        updateScore(totalScore);
    }

    public void updateScore (int score)
    {
        score_text.text = score.ToString();
        int offset = Mathf.FloorToInt(Mathf.Log10 (score)) + 1;
        float new_x = offset * -9;
        score_text.transform.position = new Vector3(new_x, score_text.transform.position.y, score_text.transform.position.z);
    }

    public void play ()
    {
        cam.transform.position = new Vector3(0, cam.transform.position.y, cam.transform.position.z);
        masterStart = true;
        countdown = 3;
        start = false;
        timer = 30;
        totalScore = 0;
        score_text.text = 0.ToString();
        score_text.transform.position = new Vector3(-8, score_text.transform.position.y, score_text.transform.position.z);
    }

    public void hiscore ()
    {
        cam.transform.position = new Vector3(450, cam.transform.position.y, cam.transform.position.z);
    }

    public void mainMenu ()
    {
        cam.transform.position = new Vector3(-350, cam.transform.position.y, cam.transform.position.z);
    }
}
