using UnityEngine;

namespace br.com.arthurjf.shapewar.Weapon
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private GameObject m_bulletPrefab;
        [SerializeField] private Transform m_bulletExit;
        [SerializeField] private float fireRate = 0.5f;
        private float nextFireTime;

        public void AttemptShoot()
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();

                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        private void Shoot()
        {
            Instantiate(m_bulletPrefab, m_bulletExit.position, m_bulletExit.rotation);
        }
    }
}
