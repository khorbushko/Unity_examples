using UnityEngine;

[CreateAssetMenu(fileName = "PowerupSO", menuName = "PowerupSO")]
public class PowerupSO : ScriptableObject
{
    [SerializeField] string powerUpType;
    [SerializeField] float valueChange;
    [SerializeField] float time;

    public string getPowerUpType()
    {
        return powerUpType;
    }

    public float getValueChanged()
    {
        return valueChange;
    }

    public float getTime()
    {
        return time;
    }
}
