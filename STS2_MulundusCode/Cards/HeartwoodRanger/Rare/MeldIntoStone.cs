using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class MeldIntoStone : HeartWoodRangerCard
{
    
    public MeldIntoStone() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<PlatingPower>(20);
        WithPower<MeldedInStonePower>(5);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<PlatingPower>(choiceContext, this);
        await CommonActions.ApplySelf<MeldedInStonePower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["MeldedInStonePower"].UpgradeValueBy(-2);
    }
}