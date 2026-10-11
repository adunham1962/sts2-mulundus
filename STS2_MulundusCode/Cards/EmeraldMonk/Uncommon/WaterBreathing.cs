using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;
[Pool(typeof(EmeraldMonkCardPool))]
public class WaterBreathing : EmeraldMonkCard
{
    public WaterBreathing() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<WaterBreathingPower>(1);
        WithKeyword(EmeraldMonkKeywords.Ebb);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<WaterBreathingPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["WaterBreathingPower"].UpgradeValueBy(1);
    }
}