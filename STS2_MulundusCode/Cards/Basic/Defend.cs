using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Basic;
[Pool(typeof(HeartwoodRangerCardPool))]
public class HeartwoodRangerDefend : HeartWoodRangerCard
{
    public HeartwoodRangerDefend() : base(1, CardType.Skill, CardRarity.Basic, TargetType.None)
    {
        WithBlock(5);
        WithTags(CardTag.Defend);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
    }
}