using UnityEngine;
using Unity.Netcode;

namespace UV.Player
{
    [System.Serializable]
    public struct PlayerTransformData : INetworkSerializable
    {
        private float _x, _y, _z;
        private float _yRot;

        public Vector3 Position
        {
            readonly get => new(_x, _y, _z);
            set { _x = value.x; _z = value.z; }
        }

        public Quaternion Rotation
        { 
            readonly get => Quaternion.Euler(0, _yRot, 0);
            set => _yRot = value.eulerAngles.y;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _x);
            serializer.SerializeValue(ref _y);
            serializer.SerializeValue(ref _z);
            serializer.SerializeValue(ref _yRot);
        }
    }
}
