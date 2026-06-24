using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class SerratedBarbsPower : CustomPowerModel
{
    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        var thorns = Owner.Powers.ToList().Find(p => p is ThornsPower);
        if (thorns is null || target != Owner || dealer is null) return;
        await CreatureCmd.Damage(choiceContext, dealer, new DamageVar(thorns.Amount * 2, ValueProp.Move), dealer, null);
        await PowerCmd.Apply<ThornsPower>(choiceContext, Owner, -1, Owner, null);
    }

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
}