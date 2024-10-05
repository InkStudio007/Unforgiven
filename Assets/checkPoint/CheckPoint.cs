using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    public bool isOnCheckPoint;

    [SerializeField]
    public string SceneName;

    public PlayerDeath playerDeath;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isOnCheckPoint = true;
        }
    }
    private void Update()
    {
        if (isOnCheckPoint)
        {
            SceneManager.LoadScene(SceneName);
            playerDeath.CheckPointPos = transform.position;
        }
    }
}
