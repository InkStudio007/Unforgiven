using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SChanger : MonoBehaviour
{
    [SerializeField]
    private Connection _Connection;

    [SerializeField]
    private string TargetSceneName;

    [SerializeField]
    private Transform SpawnPointLeft;

    [SerializeField]
    private Transform SpawnPointRight;

    [SerializeField]
    private Player isFacingRight;

    [System.Obsolete]
    private void Start()
    {
        if(_Connection == Connection.ActiveConnection)
        {
            if (isFacingRight)
            {
                FindObjectOfType<Player>().transform.position = SpawnPointRight.position;
            }
            if(isFacingRight == false)
            {
                FindObjectOfType<Player>().transform.position = SpawnPointLeft.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Connection.ActiveConnection = _Connection;
            SceneManager.LoadScene(TargetSceneName);
        }
    }
}
