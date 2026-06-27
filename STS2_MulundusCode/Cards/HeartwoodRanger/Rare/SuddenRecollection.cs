using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class SuddenRecollection : HeartWoodRangerCard
{

    public SuddenRecollection() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
    } 
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var possibleCards = PileType.Exhaust.GetPile(Owner).Cards.ToList().FindAll(c => c != this);
        var block = 0;
        for (var i = 0; i < possibleCards.Count && i < 5; i++)
        {
            var random = Random.Shared.Next(possibleCards.Count);
            var randomCard = possibleCards[random];
            await CardPileCmd.Add(randomCard, PileType.Discard);
            block += randomCard.EnergyCost.GetResolved();
            possibleCards = PileType.Exhaust.GetPile(Owner).Cards.ToList().FindAll(c => c != this);
        }

        await CommonActions.CardBlock(this, new BlockVar(block, ValueProp.Move), play);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}