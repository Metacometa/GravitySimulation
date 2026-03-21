using UnityEngine;
using Zenject;

public class Attractor : MonoBehaviour
{
    [Inject] private AttractorRegistry _attractorRegistry;

    private void Awake()
    {
        _attractorRegistry.Add(this);
    }

    private void OnDestroy()
    {
        _attractorRegistry.Remove(this);
    }
}