using UnityEngine;
using Unity.Netcode;

namespace UV.Player.Controller
{
    /// <summary>
    /// Controls the basic movement of the player
    /// </summary>
    [AddComponentMenu("UV/Player/Player Controller")]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : NetworkBehaviour
    {
        /// <summary>
        /// The character controller of the current player controller
        /// </summary> 
        [field: Header("References")]
        [SerializeField] private CharacterController _characterController;

        /// <summary>
        /// The movement speed of the player 
        /// </summary>
        [field: Header("Basic Settings")]
        [field: SerializeField] public float Speed { get; private set; }

        /// <summary>
        /// Finds all references at Reset
        /// </summary>
        private void Reset() => FindReferences();

        /// <summary>
        /// Finds all the necessary references
        /// </summary>
        private void FindReferences()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkDespawn();
            enabled = IsOwner;
            _characterController.enabled = enabled;
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            var force = transform.forward * vertical + transform.right * horizontal;
            _characterController.Move(Speed * Time.deltaTime * force.normalized);
        }
    }
}
