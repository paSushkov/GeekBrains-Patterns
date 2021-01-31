using Asteroids.Common;
using UnityEngine;

namespace Asteroids.PlayerData
{
    public class ShipMarkUp : MonoBehaviour
    {
        #region PrivateData

        [SerializeField] private Transform[] barrels;
        [SerializeField] private ColliderListener2D colliderListener;

        #endregion


        #region Properties

        public Transform[] Barrels => barrels;
        public ColliderListener2D ColliderListener => colliderListener;

        #endregion
    }
}