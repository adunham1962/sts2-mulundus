using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class Revivify : HeartWoodRangerCard
{
    public Revivify() : base(3, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1);
        if (PileType.Exhaust.GetPile(Owner).IsEmpty)
        {
            return;
        }
        var card = (await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Exhaust.GetPile(Owner).Cards.ToList(), Owner, prefs)).FirstOrDefault();
        if (card == null)
            return;
        if (card.TargetType == TargetType.AnyEnemy && CombatState != null)
        {
            var target = GetRandomTarget(card);
            await CardCmd.AutoPlay(choiceContext, card, target);
            target = GetRandomTarget(card);
            await CardCmd.AutoPlay(choiceContext, card, target);
            target = GetRandomTarget(card);
            await CardCmd.AutoPlay(choiceContext, card, target);
        }
        else if (card.TargetType == TargetType.Self)
        {
            await CardCmd.AutoPlay(choiceContext, card, null);
            await CardCmd.AutoPlay(choiceContext, card, null);
            await CardCmd.AutoPlay(choiceContext, card, null);
        }
        
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}