using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector3 Direction;

    public float timeToDestroy = 2f;

    public float side = 1f;

    public int damageAmount = 1;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Direction * Time.deltaTime * side);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyBase>();

        if (enemy != null)
        {
            enemy.Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
