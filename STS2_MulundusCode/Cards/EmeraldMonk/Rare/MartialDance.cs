using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;

[Pool(typeof(EmeraldMonkCardPool))]
public class MartialDance : EmeraldMonkCard
{

    public MartialDance() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<MartialDancePower>(1);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<MartialDancePower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["MartialDancePower"].UpgradeValueBy(1);
    }
}