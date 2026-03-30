using TMPro;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Energy
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _energyValueText;

        [Inject] private EnergySystem _energySystem;

        private void Awake()
        {
            _energySystem.EnergyUpdated += UpdateText;
        }

        private void OnDestroy()
        {
            _energySystem.EnergyUpdated -= UpdateText;
        }

        private void UpdateText(int energy)
        {
            _energyValueText.text = "Energy: " + energy.ToString();
        }
    }
}