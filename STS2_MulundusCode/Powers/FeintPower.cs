using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class FeintPower : CustomPowerModel
{

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Player)
        {
            await PowerCmd.Remove<DoubleDamagePower>(Owner);
            await PowerCmd.Remove(this);
        }
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner == Owner.Player && cardPlay.Card.Type == CardType.Attack)
        {
            await PowerCmd.Remove<DoubleDamagePower>(Owner);
            await PowerCmd.Remove(this);
        }
    }

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
}