public interface ICharacterHealth
{
    public void InflictDamage(float damageAmount, bool fatal = false, bool ignoreInvulnerability = false);
}