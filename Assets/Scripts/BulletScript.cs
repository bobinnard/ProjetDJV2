using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public int damage;

    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
    }

    private void OnTriggerEnter(Collider other){
        if(other.TryGetComponent<EnnemyScript>(out var enemy)) enemy.TakeDamage(damage);
        Destroy(gameObject);
    }
}
