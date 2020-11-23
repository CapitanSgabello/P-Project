using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text score;
    public GameObject cuore100;
    public GameObject cuore90;
    public GameObject cuore80;
    public GameObject cuore70;
    public GameObject cuore60;
    public GameObject cuore50;
    public GameObject cuore40;
    public GameObject cuore30;
    public GameObject cuore20;
    public GameObject cuore10;
    public float health;
    void Start()
    {
        score=GetComponent<Text>();
        health = PlayerController.instance.currentHealth;
    }

    void Update()
    {
        health = PlayerController.instance.currentHealth;
        score.text = "" + health;

        cuore100.SetActive(false);
        cuore90.SetActive(false);
        cuore80.SetActive(false);
        cuore70.SetActive(false);
        cuore60.SetActive(false);
        cuore50.SetActive(false);
        cuore40.SetActive(false);
        cuore30.SetActive(false);
        cuore20.SetActive(false);
        cuore10.SetActive(false);
      
        if (health >90)
            cuore100.SetActive(true);
        else if(health <= 90 && health > 80)
            cuore90.SetActive(true);
        else if (health <= 80 && health > 70)
            cuore80.SetActive(true);
        else if (health <= 70 && health > 60)
            cuore70.SetActive(true);
        else if (health <= 60 && health > 50)
            cuore60.SetActive(true);
        else if (health <= 50 && health > 40)
            cuore50.SetActive(true);
        else if (health <= 40 && health > 30)
            cuore40.SetActive(true);
        else if (health <= 30 && health > 20)
            cuore30.SetActive(true);
        else if (health <= 20 && health > 10)
            cuore20.SetActive(true);
        else if (health <= 10)
            cuore10.SetActive(true);
    }
}
