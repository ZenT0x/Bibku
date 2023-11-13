using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptVaisseau_Ennemie : MonoBehaviour
{
    public GameObject vaisseau;
    public GameObject projectile;
    public int vie = 3;
    public float vitesse = 3f;
    public bool isAttacking = false;
    public float minAttackingTime = 5f;
    public float maxAttackingTime = 10f;
    public float minIdleTime = 5f;
    public float maxIdleTime = 10f;
    public float SreloadTime;
    protected float reloadTime;
    protected float attackingTime;
    protected float idleTime;

    protected float isMoovingIdleTime;


    void Start()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        attackingTime = Random.Range(minAttackingTime, maxAttackingTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (idleTime > 0)
        {
            idleTime -= Time.deltaTime;
        }
        else if (attackingTime > 0)
        {
            isAttacking = true;
            attackingTime -= Time.deltaTime;
        }
        else
        {
            isAttacking = false;
            idleTime = Random.Range(minIdleTime, maxIdleTime);
            attackingTime = Random.Range(minAttackingTime, maxAttackingTime);
        }
        if (isAttacking)
        {
            AlligmentY(vaisseau);
            GetComponent<SpriteRenderer>().color = Color.red;
            if (reloadTime > 0)
            {
                reloadTime -= Time.deltaTime;
            }
            else
            {
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.GetComponent<ScriptProjectile>().Lancer(-1, GetComponent<Collider2D>());
                reloadTime = SreloadTime;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            IdleY();
        }
    }

    void AlligmentY(GameObject vaisseau)
    {
        if (vaisseau.transform.position.y > transform.position.y + 0.1f)
        {
            transform.Translate(vitesse * Time.deltaTime, 0, 0);
        }
        else if (vaisseau.transform.position.y < transform.position.y - 0.1f)
        {
            transform.Translate(-vitesse * Time.deltaTime, 0, 0);
        }
    }
    void IdleY()
    {
        // Calculer la nouvelle position en Y
        float newY = Mathf.Sin(Time.time * (vitesse/2)) * 2f;

        // Assurez-vous que le vaisseau reste à l'intérieur des limites de la carte
        newY = Mathf.Clamp(newY, -4.5f, 4.5f);

        // Mettre à jour la position du vaisseau
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            vie -= 1;
            if(vie <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}