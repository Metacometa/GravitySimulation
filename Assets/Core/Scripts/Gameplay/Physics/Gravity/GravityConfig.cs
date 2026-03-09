using UnityEngine;

namespace GravitySim.Gameplay.Gravity
{
    [CreateAssetMenu(fileName = "GravityConfig", menuName = "Core/Config/Gravity Config")]
    public class GravityConfig : ScriptableObject
    {
        public float G;
    }
}