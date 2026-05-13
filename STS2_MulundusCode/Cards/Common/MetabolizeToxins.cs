using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Extensions;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Common;
[Pool(typeof(HeartwoodRangerCardPool))]
public class MetabolizeToxins : HeartWoodRangerCard
{
    public override string PortraitPath => "Cilef Base.png".CardImagePath();
    public MetabolizeToxins() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithHeal(1);
        WithPower<StrengthPower>(1);
    }
    protected override bool HasEnergyCostX => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var xValue = ResolveEnergyXValue();
        var currentPoison = Owner.Creature.GetPower<PoisonPower>();
        if (currentPoison is not { Amount: > 0 })
        {
            return;
        }

        var delta = 0;
        if (xValue > currentPoison.Amount)
        {
            delta = currentPoison.Amount;
        } else if (currentPoison.Amount > xValue)
        {
            delta = xValue;
        }
        else
        {
            delta = xValue;
        }
        
        await CreatureCmd.Heal(Owner.Creature, delta);
        await CommonActions.ApplySelf<StrengthPower>(this, DynamicVars["StrengthPower"].BaseValue * delta);
        await CommonActions.ApplySelf<PoisonPower>(this, delta * -1);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Heal.UpgradeValueBy(1);
        DynamicVars["StrengthPower"].UpgradeValueBy(1);
    }
}