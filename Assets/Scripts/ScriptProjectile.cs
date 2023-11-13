using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptProjectile : MonoBehaviour
{   
    public float vitesse = 10f;
    public float timeToLive = 5f;
    public bool isLaunched = false;

    void Start()
    {
        // Ignore les collisions entre les objets de la couche "Projectile"
        int projectileLayer = LayerMask.NameToLayer("Projectile");
        Physics2D.IgnoreLayerCollision(projectileLayer, projectileLayer);
    }

    void Update()
    {
        
    }

    public void Lancer(int direction, Collider2D creatorCollider)
    {
        isLaunched = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction * vitesse, 0);
        Destroy(gameObject, timeToLive);

        // Ignore les collisions avec l'objet qui a créé le projectile
        Collider2D projectileCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(projectileCollider, creatorCollider);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
    
        if(collision.gameObject.tag != "Projectile")
        {
            Destroy(gameObject);
        }
    }
}
