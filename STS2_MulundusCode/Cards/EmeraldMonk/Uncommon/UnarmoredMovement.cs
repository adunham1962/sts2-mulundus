using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;
[Pool(typeof(EmeraldMonkCardPool))]
public class UnarmoredMovement : EmeraldMonkCard
{
    public UnarmoredMovement() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<UnarmoredMovementPower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<UnarmoredMovementPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["UnarmoredMovementPower"].UpgradeValueBy(1);
    }
}