using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;
[Pool(typeof(EmeraldMonkCardPool))]
public class Guidance : EmeraldMonkCard
{

    public Guidance() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithPower<GuidancePower>(4);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<GuidancePower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["GuidancePower"].UpgradeValueBy(2);
    }
}