using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    NavMeshAgent agent;
    int health, damage;

    Text damageTxt, lifeTxt;

    void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        health = 9;
        damage = 3;

        lifeTxt = GameObject.Find("LifeText").GetComponent<Text>();
        damageTxt = GameObject.Find("DamageText").GetComponent<Text>();
    }
    
    void Update()
    {
        Movement();

        lifeTxt.text = "Vida: " + health.ToString();
        damageTxt.text = "Dano: " + damage.ToString();
    }

    void Movement()
    {
        if (Input.GetKeyDown(InputManager.IM.walk))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
    }

    public void AddLife(int status)
    {
        health += status;
    }

    public void AddDamage(int status)
    {
        damage += status;
    }
}
