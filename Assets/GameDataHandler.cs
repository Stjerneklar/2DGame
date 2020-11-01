using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataHandler : ConfigurableFromFile<GameDataHandlerSettings>
{
    private GameDataHandler mytools;
    private GameObject statObjJump;
    private GameObject statObjHit;
    private GameObject statObjMove;
    private GameObject statObjGrav;
    private GameObject statObjTime;

    private void Start()
    {
        mytools = GetComponent<GameDataHandler>();
        statObjJump = GameObject.Find("PlayerStatsJumpHeightPtsValue");
        statObjHit = GameObject.Find("PlayerStatsHitForcePtsValue");
        statObjMove = GameObject.Find("PlayerStatsMoveSpeedPtsValue");
        statObjGrav = GameObject.Find("PlayerStatsCoinGravityPtsValue");
       
        //mytools.settings.PlayerStatsJumpHeightPts = 2;
        //mytools.settings.PlayerStatsHitForcePts = 2;
        //mytools.settings.PlayerStatsMoveSpeedPts = 2;
        //mytools.settings.PlayerStatsCoinGravityPts = 2;
        //mytools.settings.PlayerStatsTimeControlPts = 2;
    }
    /*public void AddHighscore(int newHighscore)
    {
        int oldHighscore = mytools.settings.HighScore;
        if (newHighscore > oldHighscore)
        {
            mytools.settings.HighScore = newHighscore;
            mytools.Save();
        }
    }*/

    public void ChangeStatJump(bool increase) { ChangeStatPts(increase, "JumpHeight"); } 
    public void ChangeStatHit(bool increase) { ChangeStatPts(increase, "HitForce"); } 
    public void ChangeStatMove(bool increase) { ChangeStatPts(increase, "MoveSpeed"); } 
    public void ChangeStatGrav(bool increase) { ChangeStatPts(increase, "CoinGrav"); } 
    public void ChangeStatTime(bool increase) { ChangeStatPts(increase, "TimeCont"); } 


    public void ChangeStatPts(bool increase, string stat) 
    {
        int points = 0;
        GameObject gameObject = statObjJump;//defaulting to make the compiler happy
        
        if (stat == "JumpHeight") { points = mytools.settings.PlayerStatsJumpHeightPts; gameObject = statObjJump;}
        else if (stat == "HitForce") { points = mytools.settings.PlayerStatsHitForcePts; gameObject = statObjHit; }
        else if (stat == "MoveSpeed") { points = mytools.settings.PlayerStatsMoveSpeedPts; gameObject = statObjMove; }
        else if (stat == "CoinGrav") { points = mytools.settings.PlayerStatsCoinGravityPts; gameObject = statObjGrav; }
        else if (stat == "TimeCont") { points = mytools.settings.PlayerStatsTimeControlPts; gameObject = GameObject.Find("PlayerStatsTimeControlPtsValue"); ; }
        else { Debug.Log("wrong input to stats");}

        if (increase && points < 5)
        {
            points++;
            Debug.Log(stat+" Increased to " + points);
            mytools.Save();
        }
        else if (!increase && points > 0)
        {
            points--;
            Debug.Log(stat + " Decreased to " + points);
            mytools.Save();
        }
        else
        {
            Debug.Log("unable to change "+stat+" - "+points);
        }
        gameObject.GetComponent<UnityEngine.UI.Text>().text = points.ToString();
    }

    public override string SavePath() => Application.persistentDataPath + @"/dev.json";
}
