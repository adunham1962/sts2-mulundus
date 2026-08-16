using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;

[Pool(typeof(EmeraldMonkCardPool))]
public class EmpoweredStrikes : EmeraldMonkCard
{
    public EmpoweredStrikes() : base(0, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<EmpoweredStrikesPower>(2);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<EmpoweredStrikesPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["EmpoweredStrikesPower"].UpgradeValueBy(2);
    }
}