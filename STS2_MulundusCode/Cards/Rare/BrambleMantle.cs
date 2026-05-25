using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class BrambleMantle : HeartWoodRangerCard
{
    public BrambleMantle() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<BrambleMantlePower>(3);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.ApplySelf<BrambleMantlePower>(this);
    }
}