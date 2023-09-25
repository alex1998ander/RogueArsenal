using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossAttackManager : MonoBehaviour
{

    private IBossAttack[] _activeBossAttacks;

    private static readonly List<IBossAttack> BossAttackPool = new();
    
    public IBossAttack[] SelectRandomBossAttacks(int count)
    {
        System.Random rnd = new System.Random();
        _activeBossAttacks = BossAttackPool.OrderBy(x => rnd.Next()).Take(count).ToArray();

        return _activeBossAttacks;
    }

    private void Start()
    {
        GetComponent<BossAttackLaserFocus>().ExecuteAbility();
    }
}
