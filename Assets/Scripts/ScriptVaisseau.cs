using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptVaisseau : MonoBehaviour
{   
    public GameObject projectile;
    public float vitesse = 10f;
    public int vie = 3;
    public float reloadTime = 0f;
    public int projectileAmmo = 10;
    public float projectileAmmoReloadTime = 1f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {   
        float newSpeed = 0;

        if(Input.GetKey(KeyCode.UpArrow) && transform.position.y < 4.5f)
        {
            newSpeed += -vitesse;
        }
        if(Input.GetKey(KeyCode.DownArrow) && transform.position.y > -4.5f)
        {   
            newSpeed += vitesse;
        }
        transform.Translate(newSpeed * Time.deltaTime, 0, 0);

        if(Input.GetKey(KeyCode.Space))
        {   
            if(reloadTime > 0)
            {   
                reloadTime -= Time.deltaTime;
            }
            else if(projectileAmmo > 0)
            {   
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.GetComponent<ScriptProjectile>().Lancer(1, GetComponent<Collider2D>());
                projectileAmmo -= 1;
                reloadTime = 0.1f;
            }  
        }
        if(projectileAmmo < 10)
        {   
            if(projectileAmmoReloadTime > 0)
            {
                projectileAmmoReloadTime -= Time.deltaTime;
            }
            else
            {
                projectileAmmo += 1;
                projectileAmmoReloadTime = 1f;
            }
        }
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