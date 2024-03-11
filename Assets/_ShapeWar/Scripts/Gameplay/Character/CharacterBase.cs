using UnityEngine;

namespace br.com.arthurjf.shapewar.Character
{
    // ABSTRACTION
    public abstract class CharacterBase : MonoBehaviour
    {
        [SerializeField] private int m_health = 10;
        [SerializeField] protected float m_moveSpeed = 2f;
        [SerializeField] protected float m_rotateSpeed = 4f;

        public void TakeDamage(int amount)
        {
            m_health -= amount;

            if (m_health <= 0)
            {
                Die();
            }
        }

        protected abstract void Move(float amount);

        protected virtual void Rotate(float amount)
        {
            transform.Rotate(amount * m_rotateSpeed * Time.fixedDeltaTime * -transform.forward);
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
