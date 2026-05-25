using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class MeldedInStonePower() : CustomPowerModel()
{
    
    public override bool ShouldPlay(CardModel card, AutoPlayType _)
    {
        return card.Owner.Creature != Owner || card.Type != CardType.Attack || Amount <= 0;
    }

    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player) return Task.CompletedTask;
        SetAmount(Amount - 1);
        return Task.CompletedTask;
    }

    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
}