using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Player_HealSystem Health;
    public Vector3 CheckPointPos;
    public Player Player;
    public CheckPoint CheckPoint;

    void Update()
    {
        if (Health.PlayerIsDead)
        {
            SceneManager.LoadScene(CheckPoint.SceneName);
            Player.transform.position = CheckPointPos; 
        }
    }
}
