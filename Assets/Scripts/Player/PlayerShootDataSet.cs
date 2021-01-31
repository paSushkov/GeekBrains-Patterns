using UnityEngine;

namespace Asteroids.PlayerData
{
    [CreateAssetMenu(menuName = "Asteroids/PlayerDataSet-Shoot")]
    public class PlayerShootDataSet : ScriptableObject
    {
        #region Private data

        [SerializeField] private Rigidbody2D bullet;
        [SerializeField] private float shootForce;
        
        #endregion


        #region Properties

        public Rigidbody2D Bullet => bullet;
        public float ShootForce => shootForce;

        #endregion
    }
}