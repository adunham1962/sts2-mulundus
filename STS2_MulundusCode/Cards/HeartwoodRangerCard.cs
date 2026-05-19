using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using STS2_Mulundus.STS2_MulundusCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using static MegaCrit.Sts2.Core.Entities.Cards.TargetType;

namespace STS2_Mulundus.STS2_MulundusCode.Cards;

public abstract class HeartWoodRangerCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    ConstructedCardModel(cost, type, rarity, target)
{
    
    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        var rangerCard = card as HeartWoodRangerCard;
        if (rangerCard == this && card.IsGrim())
        {
            await PlayAgainstRandom(card, choiceContext);
            await CardPileCmd.Add(rangerCard, PileType.Exhaust);
        }
    }

    protected async Task PlayAgainstRandom(CardModel card, PlayerChoiceContext choiceContext)
    {
        var target = card.TargetType switch
        {
            AnyEnemy => GetRandomTarget(card),
            Self => Owner.Creature,
            _ => null
        };
        await CardCmd.AutoPlay(choiceContext, card, target);
    }
    
    protected Creature GetRandomTarget(CardModel card)
    {
        var target = Owner.Creature;
        if (card.TargetType != AnyEnemy || CombatState == null) return target;
        var random = Random.Shared.Next(CombatState.HittableEnemies.Count);
        target = CombatState.HittableEnemies[random];

        return target;
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