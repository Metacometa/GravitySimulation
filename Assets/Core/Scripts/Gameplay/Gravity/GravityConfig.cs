using UnityEngine;

namespace GravitySimulator.Gameplay.Gravity
{
    [CreateAssetMenu(fileName = "GravityConfig", menuName = "Core/Config/Gravity Config")]
    public class GravityConfig : ScriptableObject
    {
        public float G;
        public float Softening;
    }
}