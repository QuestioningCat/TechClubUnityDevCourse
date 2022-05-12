using System;

public class Stats
{
    public float Health { get; protected set; }
    public float MaxHeath { get; protected set; }
    public float MoveSpeed { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public float AttackDamage { get; protected set; }

    public Action OnDamageTaken;

    public Stats(float maxHeath, float moveSpeed = 10f, float attackSpeed = 1f, float attackDamage = 10f)
    {
        Health = MaxHeath = maxHeath;
        MoveSpeed = moveSpeed;
        AttackDamage = attackDamage;
        AttackSpeed = attackSpeed;
    }
    public void TakeDamage(float ammount)
    {
        Health -= ammount;
        if(Health < 0)
            Health = 0;
    }
}
