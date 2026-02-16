using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward*speed*Time.deltaTime;
    }

    void OnTriggerEnter(Collider other){
        EnnemyScript ennemy;
        if(other.TryGetComponent<EnnemyScript>(out ennemy)) ennemy.TakeDamage(damage);
        Destroy(gameObject);
    }
}
