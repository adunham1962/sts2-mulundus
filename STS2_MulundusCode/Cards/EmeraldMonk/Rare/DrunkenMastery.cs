using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;

[Pool(typeof(EmeraldMonkCardPool))]
public class DrunkenMastery() : EmeraldMonkCard(-1, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override bool HasEnergyCostX => true;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var x = ResolveEnergyXValue();
        await CommonActions.ApplySelf<StrengthPower>(choiceContext, this, IsUpgraded ? x + 1 : x);
        await CommonActions.ApplySelf<DrunkenMasteryPower>(choiceContext, this, 1);
    }
}