using UnityEngine;

namespace Asteroids.PlayerData
{
    [CreateAssetMenu(menuName = "Asteroids/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        #region Private data

        [SerializeField] private GameObject playerShipTransform;
        [SerializeField] private float playerHP = 100f;
        [SerializeField] private PlayerMoveDataSet moveData;
        [SerializeField] private PlayerShootDataSet shootData;

        #endregion


        #region Properties

        public GameObject PlayerShipTransform => playerShipTransform;
        public float PlayerHp => playerHP;
        public PlayerMoveDataSet MoveData => moveData;
        public PlayerShootDataSet ShootData => shootData;

        #endregion
    }
}