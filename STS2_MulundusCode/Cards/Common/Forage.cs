using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Cards.Token;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Common;

[Pool(typeof(HeartwoodRangerCardPool))]
public class Forage() : HeartWoodRangerCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var berries = Goodberry.Create(Owner, 3, CombatState!).ToList();
        var drawResult = await CardPileCmd.AddGeneratedCardToCombat(berries[0], PileType.Draw, true, CardPilePosition.Random);
        var discardResult = await CardPileCmd.AddGeneratedCardToCombat(berries[1], PileType.Discard, true);
        await CardPileCmd.AddGeneratedCardToCombat(berries[2], PileType.Hand, true);
        
        CardCmd.PreviewCardPileAdd([drawResult, discardResult]);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}