using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;

[Pool(typeof(EmeraldMonkCardPool))]
public class SymbioticPenance : EmeraldMonkCard
{

    public SymbioticPenance() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<VulnerablePower>(3);
        WithPower<WisdomPower>(3);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<WisdomPower>(choiceContext, this);
        await CommonActions.ApplySelf<VulnerablePower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["WisdomPower"].UpgradeValueBy(2);
    }
}