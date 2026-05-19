using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2_Mulundus.STS2_MulundusCode.Cards.Token;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Extensions;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Common;
[Pool(typeof(HeartwoodRangerCardPool))]
public class Decompose : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/decompose.png";

    public Decompose() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithVar("BerryAdd", 1);
        WithKeyword(CardKeyword.Exhaust);
        WithTips(_ => [HoverTipFactory.FromCard<Goodberry>()]);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1);
        if (PileType.Discard.GetPile(Owner).IsEmpty)
        {
            return;
        }
        var card = (await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Discard.GetPile(Owner).Cards.ToList(), Owner, prefs)).FirstOrDefault();
        if (card == null)
            return;
        if (Owner == card.Owner && CombatState is not null)
        {
            await CardCmd.Exhaust(choiceContext, card);
            var berries = Goodberry.Create(Owner, DynamicVars["BerryAdd"].BaseValue, CombatState).ToList();
            foreach (var goodberry in berries)
            {
                await CardPileCmd.AddGeneratedCardToCombat(goodberry, PileType.Hand, true);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["BerryAdd"].UpgradeValueBy(1);
    }
}