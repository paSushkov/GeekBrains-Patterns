using UnityEngine;

namespace Asteroids.PlayerData
{
    [CreateAssetMenu(menuName = "Asteroids/PlayerDataSet-Move")]
    public class PlayerMoveDataSet : ScriptableObject
    {
        #region Private data

        [SerializeField] private float initialSpeed = 0f;
        [SerializeField] private float acceleration = 5f;

        #endregion

        
        #region Properties
        
        public float InitialSpeed => initialSpeed;
        public float Acceleration => acceleration;

        #endregion
    }
}