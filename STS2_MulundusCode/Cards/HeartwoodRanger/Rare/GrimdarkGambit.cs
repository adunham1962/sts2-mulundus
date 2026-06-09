using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class GrimdarkGambit : HeartWoodRangerCard
{
    public GrimdarkGambit() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithVar(new DynamicVar("CardsToPlay", 3));
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var possibleCards = PileType.Exhaust.GetPile(Owner).Cards.ToList().FindAll(c => c != this);
        for (var i = 0; i < possibleCards.Count && i < DynamicVars["CardsToPlay"].BaseValue; i++)
        {
            var random = Random.Shared.Next(possibleCards.Count);
            var randomCard = possibleCards[random];
            await PlayAgainstRandom(randomCard, choiceContext);
            possibleCards = PileType.Exhaust.GetPile(Owner).Cards.ToList().FindAll(c => c != this);
        }

    }

    protected override void OnUpgrade()
    {
        DynamicVars["CardsToPlay"].UpgradeValueBy(2);
    }
}