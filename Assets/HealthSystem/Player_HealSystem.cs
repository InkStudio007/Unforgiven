using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_HealSystem : MonoBehaviour
{
    public Image HealthBar;

    public float HealthAmount = 100f;
    public float MedicineAmount = 0;
    public Text MedicineText;
    public GameObject Medicine;
    private GameObject MedicineSpawner;
    private float CoolDown = 10;

    // Start is called before the first frame update
    void Start()
    {
        MedicineSpawner = GameObject.FindGameObjectWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        MedicineText.text = MedicineAmount.ToString();

        if (Input.GetKeyDown(KeyCode.E) && MedicineAmount > 0)
        {
            Heal(20);
            MedicineAmount -= 1;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Damage(20);
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

    public void Damage(float Damage)
    {
        HealthAmount -= Damage;
        HealthBar.fillAmount = HealthAmount / 100f;
    }

    public void Heal(float Heal)
    {
        HealthAmount += Heal;
        HealthAmount = Mathf.Clamp(HealthAmount, 0, 100);

        HealthBar.fillAmount = HealthAmount / 100;
    }
}
