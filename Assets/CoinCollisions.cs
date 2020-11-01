using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using Utility;
using Assets;

public class CoinCollisions : MonoBehaviour
{
    private int playerhits = 0;
    private int groundhits = 0;
    //private int hitcombo = 0;
    private bool delayactive = false;
    public CharacterController2D controller;

    private Rigidbody2D rb2D;
    private GameDataHandler mytools;
    private float thrust = 3.0f;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mytools = GetComponent<GameDataHandler>();
    }

    void Start()
    {
        Messages.Instance.Publish(new MessageScore(0)); //get highscore
    }
    void Update()
    {
        if (Input.GetKeyDown("r")) //manual game reset
        {
            Invoke("RestartTheGame", 0f);
        }
    }
    public void RestartTheGame()
    {
        //Utility.Messages.Instance.UnsubscribeAll();
        SceneManager.LoadScene("SampleScene");//reload game scene
        Time.timeScale = 1;//restart time
        //Messages.Instance.Publish(new MessageScore(0));
    }
    
    public int Scorelogic(int newhighscore)
    {
        int oldHighscore = mytools.settings.HighScore;
        if (newhighscore > oldHighscore)
        {
            mytools.settings.HighScore = newhighscore;
            mytools.Save();
            TextUpdate("highscoreText", newhighscore);
            //Debug.Log("we did it! " + newhighscore);
            return newhighscore;
        }
        else
        {
            TextUpdate("highscoreText", oldHighscore);
            //Debug.Log("we NOT did it! " + oldHighscore);
            return oldHighscore;
        }
    }

    public int Statlogic(string stat, bool increment)
    {
        mytools.ChangeStatJump(increment);
        return mytools.settings.PlayerStatsJumpHeightPts;
    }

    public void TextUpdate(string whatToUpdate, int txt)
    {
            GameObject theGameObject = GameObject.Find(whatToUpdate);
            theGameObject.GetComponent<UnityEngine.UI.Text>().text = txt.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameObject scoreGameObject = GameObject.Find("playerHitsText");
        Messages.Instance.Publish(new MessageScore(int.Parse(scoreGameObject.GetComponent<UnityEngine.UI.Text>().text)));
    }

    public void EndDelay()
    {
        delayactive = false;
    }    

    void OnCollisionEnter2D(Collision2D col)
    {        
        if (!delayactive)
        {
            delayactive = true;
            Invoke("EndDelay", 0.25f); //wait x time between doing collision effects/tracking for more stability
            string collisionobj = col.gameObject.name;
            if (collisionobj == "Floor")
            {
                groundhits++;               
                if (groundhits > 2)
                {
                    Invoke("GameOver",0f); 
                }
                //rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);
                TextUpdate("groundHitsText",groundhits);
            }
            if (collisionobj == "Player")
            {
                if (groundhits > 0) { groundhits--; }

                Messages.Instance.Publish(new PlayerStat("jump", true));
                playerhits++;
                //hitcombo++;
                /*if (hitcombo > 9)
                {
                    hitcombo = 0;
                    int randomnumber = Random.Range(0, 9);
                    //Debug.Log("randomnumber: " + randomnumber);
                    if (randomnumber > 4)
                    {
                        mytools.ChangeStatPts(true, "JumpHeight");
                    }
                    else
                    {
                        mytools.ChangeStatPts(false, "JumpHeight");
                    }
            }*/
                TextUpdate("playerHitsText",playerhits);
                rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            }            
        }
    }
}


