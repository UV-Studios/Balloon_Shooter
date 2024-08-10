using UnityEngine;
using Unity.Netcode;

namespace UV.Player
{
    /// <summary>
    /// Manages the player data over the network 
    /// </summary>
    public class PlayerNetwork : NetworkBehaviour
    {
        /// <summary>
        /// The lerp rate for transform values 
        /// </summary>
        [Header("Settings")]
        [SerializeField] private float _lerpRate = 20;

        /// <summary>
        /// The transform server transform data 
        /// </summary>
        private readonly NetworkVariable<PlayerTransformData> _transformData = new(writePerm: NetworkVariableWritePermission.Owner);

        public void Update()
        {
            if (IsOwner)
                WriteData();
            else
                ReadData();
        }

        /// <summary>
        /// Writes all the player data to the server 
        /// </summary>
        private void WriteData()
        {
            WriteTransformData();
        }

        /// <summary>
        /// Writes all the player transform data to the server 
        /// </summary>
        private void WriteTransformData()
        {
            _transformData.Value = new()
            {
                Position = transform.position,
                Rotation = transform.rotation
            };
        }

        /// <summary>
        /// Reads all the server data 
        /// </summary>
        private void ReadData()
        {
            ReadTransformData();
        }

        /// <summary>
        /// Reads all the transform data from the server 
        /// </summary>
        private void ReadTransformData()
        {
            transform.SetPositionAndRotation(Vector3.Lerp(transform.position,
                                                          _transformData.Value.Position,
                                                          _lerpRate * Time.deltaTime),
                                             Quaternion.Lerp(transform.rotation,
                                                             _transformData.Value.Rotation,
                                                             _lerpRate * Time.deltaTime));
        }
    }
}
