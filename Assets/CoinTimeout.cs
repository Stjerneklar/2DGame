using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //So you can use SceneManager


public class CoinTimeout : MonoBehaviour
{
    public CharacterController2D controller;
    public CoinCollisions cc;

    void Awake()
    {
        cc = GetComponent<CoinCollisions>();
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.name == "Floor")
        {
            CancelInvoke(); //cancel if coin leaves the ground
        }
    }

    void GameOver()
    {
        Debug.Log("floor timeout");        
        cc.GameOver();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Floor")
        {            
            Invoke("GameOver", 1f); //if coin stays on the ground for x seconds, passes out or goes limp - the game is OVER.        
        }
    }
}