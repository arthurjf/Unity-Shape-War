using System.Collections.Generic;
using UnityEngine;

namespace br.com.arthurjf.shapewar.Managers
{
    public class WaypointsManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> m_waypoints;

        private void Reset()
        {
            ListChildWaypoints();
        }

        public Vector3 GetRandomWaypointPosition()
        {
            var randomIndex = Random.Range(0, m_waypoints.Count);

            return m_waypoints[randomIndex].position;
        }

        private void ListChildWaypoints()
        {
            var childCount = transform.childCount;

            m_waypoints = new List<Transform>();

            for (var i = 0; i < childCount; i++)
            {
                m_waypoints.Add(transform.GetChild(i));
            }
        }
    }
}
