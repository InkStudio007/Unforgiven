using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class switching : MonoBehaviour
{
    private bool main = true;
    public GameObject Main;
    public GameObject Static;

    private void Start()
    {
        //Main = GameObject.FindGameObjectWithTag("main").GetComponent<CinemachineVirtualCamera>();
        //Static = GameObject.FindGameObjectWithTag("static").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (main)
        {
            Main.gameObject.SetActive(false);
            Static.gameObject.SetActive(true);
            main = false;
        }
        else
        {
            Static.gameObject.SetActive(false);
            Main.gameObject.SetActive(true);
            main = true;
        }
    }
}
