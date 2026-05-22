using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class CuttingEdge : HeartWoodRangerCard
{
    public CuttingEdge() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<CuttingEdgePower>(3);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<CuttingEdgePower>(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["CuttingEdgePower"].UpgradeValueBy(2);
    }
}