using UnityEngine;

namespace Triplano.Lanes
{
    [CreateAssetMenu(fileName = nameof(MovementData), menuName = "Triplano/Movement/MovementData")]
    public class MovementData : ScriptableObject
    {
        [SerializeField] private float speed = 10f;

        public float Speed { get => speed; }
    }
}