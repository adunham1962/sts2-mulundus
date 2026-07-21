using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class ConstitutionPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource)
    {
        if (CombatState.Enemies.Contains(dealer) && target == Owner)
        {
            return -1 * Amount;
        }

        return 0;
    }

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (CombatState.Enemies.Contains(dealer) && target == Owner && !result.WasFullyBlocked)
        {
            await CommonActions.Apply<ConstitutionPower>(choiceContext, Owner, null, -1);
        }
    }
}