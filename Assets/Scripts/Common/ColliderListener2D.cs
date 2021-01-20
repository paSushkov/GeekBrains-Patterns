using UnityEngine;

namespace Asteroids.Common
{
    [RequireComponent(typeof(Collider2D))]
    public class ColliderListener2D : MonoBehaviour
    {
        #region Events
        
        public event TriggerEventHandler2D StayingInTrigger;
        public event TriggerEventHandler2D EnterTrigger;
        public event TriggerEventHandler2D ExitTrigger;
        public event CollisionEventHandler2D StayingInCollider;
        public event CollisionEventHandler2D EnterCollider;
        public event CollisionEventHandler2D ExitCollider;

        #endregion
        
        
        #region Static metods

        public static bool IsInLayerMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }

        #endregion
        
        
        #region Unity events

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnterTrigger?.Invoke(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            StayingInTrigger?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ExitTrigger?.Invoke(other);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            EnterCollider?.Invoke(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            StayingInCollider?.Invoke(other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            ExitCollider?.Invoke(other);
        }

        #endregion
    }
}