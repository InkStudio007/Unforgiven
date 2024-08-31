using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_HealSystem : MonoBehaviour
{
    public List<Image> HeartsD;
    public List<Image> HeartsH;

    public int HealthAmount = 4;
    public int MedicineAmount = 0;
    public Text MedicineText;
    public GameObject Medicine;
    private GameObject MedicineSpawner;
    private float CoolDown = 10;
    public PlayerInfo Info;

    // Start is called before the first frame update
    void Start()
    {
        MedicineSpawner = GameObject.FindGameObjectWithTag("MedicineSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        MedicineText.text = MedicineAmount.ToString();

        if (Input.GetKeyDown(KeyCode.E) && MedicineAmount > 0)
        {
            Heal(1);
            MedicineAmount -= 1;
        }

        if (Input.GetKeyDown(KeyCode.F))
        { 
            Damage(1);
        }
        

        CoolDown -= Time.deltaTime;
        if (CoolDown <= 0)
        {
            Instantiate(Medicine, MedicineSpawner.transform);
            CoolDown = 10;
        }

        if (HealthAmount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Damage(int Damage)
    {
        Info.Health -= Damage;
         
        foreach (Image Heart in HeartsD)
        {
            if (Heart.IsActive())
            {
                Heart.enabled = false;
                break;
            }
            else
            {
                // Do nothing
            }
        }
        
    }

    public void Heal(int Heal)
    {
        Info.Health += Heal;
        foreach (Image Heart in HeartsH)
        {
            if (Heart.IsActive() == false)
            {
                Heart.enabled = true;
                break;
            }
            else
            {
                // Do nothing
            }
        }

    }
}
