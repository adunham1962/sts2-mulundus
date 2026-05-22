using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class CursorsLogPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        var player = Owner.Player;
        if (player is null || card.Owner != Owner.Player ) return;

        var exhaustCard = PileType.Exhaust.GetPile(Owner.Player).Cards.ToList().FindAll(c => c.EnergyCost == card.EnergyCost).FirstOrDefault();
        if (exhaustCard is null || exhaustCard == card) return;

        await CardPileCmd.Add(exhaustCard, PileType.Draw, CardPilePosition.Top, this);
    }
}