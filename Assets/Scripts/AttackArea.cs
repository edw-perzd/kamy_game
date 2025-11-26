using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage = 1;
    public LayerMask bossLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto pertenece al layer del boss
        if (((1 << collision.gameObject.layer) & bossLayer) != 0)
        {
            BossFinal boss = collision.GetComponent<BossFinal>();

            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
        }
    }
}
