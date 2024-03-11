using UnityEngine;
using br.com.arthurjf.shapewar.Character;

namespace br.com.arthurjf.shapewar.Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private const float LIFETIME = 3f;

        [SerializeField] private float m_speed = 10;
        [SerializeField] private int m_bulletDamage = 1;
        [SerializeField] private Rigidbody2D m_rigidbody;

        private void Reset()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Destroy(gameObject, LIFETIME);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            m_rigidbody.MovePosition(m_rigidbody.position + m_speed * Time.fixedDeltaTime * (Vector2)transform.up);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CharacterBase character))
            {
                character.TakeDamage(m_bulletDamage);
            }

            Destroy(gameObject);
        }
    }
}
