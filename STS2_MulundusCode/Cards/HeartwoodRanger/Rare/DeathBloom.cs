using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class DeathBloom : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/death_bloom.png";
    public DeathBloom() : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithEnergy(1);
        WithPower<ThornsPower>(1);
        WithPower<StrengthPower>(1);
        WithPower<PoisonPower>(3);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        await CommonActions.ApplySelf<ThornsPower>(choiceContext, this);
        await CommonActions.ApplySelf<StrengthPower>(choiceContext, this);
        await CommonActions.ApplySelf<PoisonPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Energy.UpgradeValueBy(1);
        DynamicVars["StrengthPower"].UpgradeValueBy(1);
    }
}