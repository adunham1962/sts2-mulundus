using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;

[Pool(typeof(EmeraldMonkCardPool))]
public class Atonement : EmeraldMonkCard
{

    protected override bool HasBalanceEffect => true;
    
    protected override bool ShouldGlowGoldInternal => ShouldGlowGoldFromBalance;
    
    public Atonement() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithEnergy(2);
        WithPower<EnergyNextTurnPower>(2);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<EnergyNextTurnPower>(choiceContext, this);
        if (ShouldGlowGoldFromBalance)
        {
            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["EnergyNextTurnPower"].UpgradeValueBy(1);
    }
}