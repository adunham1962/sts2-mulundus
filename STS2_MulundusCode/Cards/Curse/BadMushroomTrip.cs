using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Curse;
[Pool(typeof(CurseCardPool))]
public class BadMushroomTrip : ConstructedCardModel
{
    public override int MaxUpgradeLevel => 0;
    public override bool CanBeGeneratedByModifiers => false;
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/bad_mushroom_trip.png";
    
    public BadMushroomTrip() : base(-1, CardType.Curse, CardRarity.Curse, TargetType.None)
    {
        WithKeyword(CardKeyword.Unplayable);
        WithKeyword(CardKeyword.Eternal);
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        var hand = PileType.Hand.GetPile(Owner).Cards;
        if (player == Owner && hand.Contains(this))
        {
            await CardCmd.Discard(choiceContext, hand.ToList()[0]);
        }
    }
}