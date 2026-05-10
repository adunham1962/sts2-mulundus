using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Cards.Token;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class GoodberryCache() : HeartWoodRangerCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var berries = Goodberry.Create(Owner, 10, CombatState!).ToList();
        if (IsUpgraded)
        {
            berries.ForEach(b => CardCmd.Upgrade(b));
        }
        var card = this;
        var pendingResults = berries.Select(b => CardPileCmd.AddGeneratedCardToCombat(b, PileType.Discard, true)).ToList();
        var results = await Task.WhenAll(pendingResults);
        CardCmd.PreviewCardPileAdd(results);
    }

    protected override void OnUpgrade()
    {
        
    }
}