using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
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

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card == this)
        {
            // TODO: Make your hand "confused" a la Snecko Oil when you draw this rather than discarding a card
            var hand = CardPile.GetCards(Owner, PileType.Hand).ToList();
            var index = new Random().Next(hand.Count);
            var discard = hand[index];
            await CardCmd.Discard(choiceContext, discard);
        }
    }
}