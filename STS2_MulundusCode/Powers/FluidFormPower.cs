using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class FluidFormPower : CustomPowerModel
{
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        var card = cardPlay.Card;
        if (card.Owner == Owner.Player && card.EnergyCost.HasLocalModifiers &&
            card.EnergyCost.Canonical > card.EnergyCost.GetResolved())
        {
            await CardPileCmd.Draw(context, Amount, Owner.Player);
        }
    }

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}