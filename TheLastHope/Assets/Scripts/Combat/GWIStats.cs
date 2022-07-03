public interface GWIStats
{
    public float maxHealth {get; set;}
    public float currentHealth {get; set;}
    public float movementSpeed {get; set;}

    public bool isDisarmed { get; set; }

    public bool isBurning { get; set; }

    public bool isSlowed { get; set; }

    public bool isStunned { get; set; }
}
