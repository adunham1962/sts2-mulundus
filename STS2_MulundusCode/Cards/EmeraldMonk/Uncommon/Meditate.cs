using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;

[Pool(typeof(EmeraldMonkCardPool))]
public class Meditate : EmeraldMonkCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/meditate.png";
    protected override bool HasEnergyCostX => true;

    public Meditate() : base(-1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithTip(EmeraldMonkKeywords.Balanced);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var xValue = ResolveEnergyXValue();
        if (IsUpgraded)
            xValue += 1;

        await CommonActions.ApplySelf<WisdomPower>(choiceContext, this, xValue);
    }
}