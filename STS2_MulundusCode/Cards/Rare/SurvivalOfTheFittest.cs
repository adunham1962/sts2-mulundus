using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class SurvivalOfTheFittest : HeartWoodRangerCard
{

    public SurvivalOfTheFittest() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithDamage(7);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //var amount = DynamicVars["DexterityPower"].BaseValue + DynamicVars.Damage.BaseValue;
        //if (play.Target is null) return;
        //if (Owner.Creature.GetPowerAmount<StrengthPower>() > play.Target.GetPowerAmount<StrengthPower>())
        //{
        //    amount *= amount;
        //}

        //await DamageCmd.Attack(amount).Targeting(play.Target).FromCard(this).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }

    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource)
    {
        if (cardSource is not null && cardSource != this) return amount;
        return amount + DynamicVars["DexterityPower"].BaseValue;
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource)
    {
        if (cardSource is not null && cardSource != this || target is null) return amount;
        if (Owner.Creature.GetPowerAmount<StrengthPower>() > target.GetPowerAmount<StrengthPower>()) return amount * 2;
        return amount;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}