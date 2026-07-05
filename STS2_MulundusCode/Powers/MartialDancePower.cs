using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class MartialDancePower : CustomPowerModel
{
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card is EmeraldMonkCard && cardPlay.Card.Keywords.Contains(EmeraldMonkKeywords.Stance) &&
            cardPlay.Card.Owner == Owner.Player)
        {
            await PlayerCmd.GainEnergy(Amount, Owner.Player);
        }
    }

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}