using UnityEngine;

public class Character : MonoBehaviour, IKillable, IDamageable<float>, IHealable<float>
{
    [SerializeField]
    protected string Name;

    [SerializeField]
    protected float Height, Speed, Weight;

    protected Vector2 InitialPosition;

    [SerializeField]
    protected float Health, MaxHealth;
    [SerializeField]
    protected int Lifes, MaxLifes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetSpeed()
    {
        return Speed;
    }

    public float GetWeight()
    {
        return Weight;
    }

    public float GetHeight()
    {
        return Height;
    }

    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    public string GetName()
    {
        return Name;
    }

    public void Kill()
    {
        if(CompareTag("Enemy"))
        {
            if(Lifes <= 0)
            gameObject.GetComponent<Enemy>().Die();
        } else if(CompareTag("Player"))
        {
            gameObject.GetComponent<Player>().DecreaseLifes(1);
        }
    }

    public void Heal(float amountHealed)
    {
        Health += amountHealed;
    }

    public float GetLifes()
    {
        return Lifes;
    }

    public void Damage(float damageTaken)
    {
        Health -= damageTaken;
        if (Health <= 0)
        {
            Kill();
        }
    }

    public float GetHealth()
    {
        return Health;
    }

    public float GetMaxHealth()
    {
        return MaxHealth;
    }

}
