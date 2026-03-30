namespace DefaultNamespace;

public class EnergyView
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
        _enegyValueText.text = energy.toString();
    }
}