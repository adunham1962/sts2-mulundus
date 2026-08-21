using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;

[Pool(typeof(EmeraldMonkCardPool))]
public class DivineProtection : EmeraldMonkCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/divine_protection.png";
    protected override bool HasBalanceEffect => true;
    protected override bool ShouldGlowGoldInternal => ShouldGlowGoldFromBalance;

    public DivineProtection() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(6);
        WithHeal(2);
        WithTip(EmeraldMonkKeywords.Balanced);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        if (TreatAsBalancedWhilePlaying)
        {
            await CreatureCmd.Heal(Owner.Creature, DynamicVars.Heal.BaseValue);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2);
        DynamicVars.Heal.UpgradeValueBy(1);
    }
}