using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Common;

[Pool(typeof(HeartwoodRangerCardPool))]
public class SpikeGrowth : HeartWoodRangerCard
{
    public SpikeGrowth() : base(1, CardType.Power, CardRarity.Common, MegaCrit.Sts2.Core.Entities.Cards.TargetType.Self)
    {
        WithPower<ThornsPower>(3);
        
    }

    private int _thornsAmount = 3;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<ThornsPower>(this, _thornsAmount);
    }

    protected override void OnUpgrade()
    {
        _thornsAmount++;
    }
}