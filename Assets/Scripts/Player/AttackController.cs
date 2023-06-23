using UnityEngine;
using UnityEngine.Serialization;

public class AttackController : MonoBehaviour
{
	public LayerMask enemies;

	public int damageAttack;

	public bool invensible = false;
	// Player
	[Header("RigidBody2D")] [SerializeField]
	private Transform player;
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if ((enemies.value & 1<<collision.gameObject.layer) == 1<<collision.gameObject.layer)
		{
			Enemy target = collision.gameObject.GetComponent<Enemy>();
			Vector2 collisionNormal = collision.contacts[0].normal;
			float angle = Vector2.Angle(collisionNormal, Vector2.up);
			if (!invensible)
			{
				damageAttack = 1;
				if (angle < target.Config.AngleDamage)
					Attack(collision.transform);
			}
			else
			{
				damageAttack = 10;
				Attack(collision.transform);
			}
			
		}
		
	}

	private void Attack(Transform enemy)
	{
		if (enemy.TryGetComponent(out IDamageable target))
			target.TakeHit(damageAttack);
	}
		
}