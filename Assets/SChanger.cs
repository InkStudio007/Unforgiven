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
    private Transform SpawnPoint;

    [System.Obsolete]
    private void Start()
    {
        if(_Connection == Connection.ActiveConnection)
        {
            FindObjectOfType<Player>().transform.position = SpawnPoint.position;
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
