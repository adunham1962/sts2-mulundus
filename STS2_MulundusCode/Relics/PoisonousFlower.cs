using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class PoisonousFlower() : STS2_MulundusRelic()
{
    private decimal _poisonAmount = 2;
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;

    public override Task AfterActEntered()
    {
        _poisonAmount++;
        return Task.CompletedTask;
    }

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != CombatSide.Player || combatState.RoundNumber > 1)
            return;
        Flash();
        foreach (var combatStateEnemy in combatState.Enemies)
        {
            await PowerCmd.Apply<PoisonPower>(combatStateEnemy, _poisonAmount, Owner.Creature, null);
        }
    }
}