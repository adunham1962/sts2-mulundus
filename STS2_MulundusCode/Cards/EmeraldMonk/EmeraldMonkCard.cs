using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using STS2_Mulundus.STS2_MulundusCode.Extensions;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk;

public abstract class EmeraldMonkCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    ConstructedCardModel(cost, type, rarity, target)
{

    public override Task AfterFlush(PlayerChoiceContext choiceContext, Player player, IReadOnlyCollection<CardModel> flushedCards,
        IReadOnlyCollection<CardModel> retainedCards)
    {
        if (retainedCards.Any(card => card.IsStance() && card == this))
        {
            EnergyCost.SetUntilPlayed(EnergyCost.GetResolved() - 1);
        }
        
        return Task.CompletedTask;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        var card = cardPlay.Card;
        if (card.IsStance() && card == this)
        {
            CardPileCmd.RemoveFromCombat(card);
        }

        if (card.HasEnterStance() && card == this)
        {
            var cards = PileType.Hand.GetPile(Owner).Cards.ToList().Where(c => c.IsStance());
            foreach (var cardModel in cards)
            {
                CardPileCmd.RemoveFromCombat(cardModel);
            }
        }

        if (this.HasEbb() && card != this)
        {
            if (card.Type == CardType.Attack)
            {
                EnergyCost.SetUntilPlayed(EnergyCost.Canonical - 1);
            }
            else
            {
                EnergyCost.SetUntilPlayed(EnergyCost.Canonical);
            }
        }
        
        if (this.HasFlow() && card != this)
        {
            if (card.Type == CardType.Skill)
            {
                EnergyCost.SetUntilPlayed(EnergyCost.Canonical - 1);
            }
            else
            {
                EnergyCost.SetUntilPlayed(EnergyCost.Canonical);
            }
        }

        return Task.CompletedTask;
    }


    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    //public override string CustomPortraitPath => "Cilef Base Upscale.png".BigCardImagePath();

    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    //public override string PortraitPath => "Cilef Base.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}