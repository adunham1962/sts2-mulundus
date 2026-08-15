using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class DownpourPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side is CombatSide.Enemy && Owner.HasPower<DownpourPower>())
        {
            await CommonActions.Apply<DownpourPower>(choiceContext, Owner, null, -1);
        }
    }

    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource)
    {
        return target == Owner && cardSource is EmeraldMonkCard && (cardSource.HasEbb() || cardSource.HasFlow() || cardSource.IsStance()) ? Amount : 0;
    }
}