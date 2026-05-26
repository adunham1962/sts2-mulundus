using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class CuttingEdgePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power.Type != PowerType.Debuff) return;
        if (Owner.CombatState is null) return;
        if (amount < 1) return;
        if (cardSource is null) return;
        if (cardSource.CurrentTarget != Owner && cardSource.CurrentTarget is not null) return;
        

        await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), Owner.CombatState.HittableEnemies, Amount, ValueProp.Unpowered, Owner, null);
        
    }
}