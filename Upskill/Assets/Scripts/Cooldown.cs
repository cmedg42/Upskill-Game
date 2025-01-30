using UnityEngine;

[System.Serializable]
public class Cooldown
{
    [SerializeField] float cooldownTime;
    private float nextFireTime;

    public bool IsCoolingDown => Time.time < nextFireTime;
    public void StartCooldown() => nextFireTime = Time.time + cooldownTime;
}

