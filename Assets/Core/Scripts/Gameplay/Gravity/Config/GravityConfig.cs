using UnityEngine;

namespace GravitySimulator.Gameplay.Gravity.Config
{
    [CreateAssetMenu(fileName = "GravityConfig", menuName = "Core/Config/Gravity Config")]
    public class GravityConfig : ScriptableObject
    {
        public float G;
        public float Softening;
    }
}