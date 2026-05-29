using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class Revivify : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/revivify.png";
    public Revivify() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
        WithCards(3);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var exhaustPile = CardPile.Get(PileType.Exhaust, Owner);
        if (exhaustPile is null) return;
        var maxPossible = exhaustPile.Cards.ToList().Count;
        if (maxPossible > 3) maxPossible = 3; 
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, maxPossible);
        var cards = await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Exhaust.GetPile(Owner).Cards.ToList(), Owner, prefs);
        foreach (var cardModel in cards.ToList())
        {
            await CardPileCmd.Add(cardModel, PileType.Draw, CardPilePosition.Top);
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}