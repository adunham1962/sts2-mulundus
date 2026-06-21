using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class VolatileFrenzy : HeartWoodRangerCard
{
    public VolatileFrenzy() : base(6, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
    {
        WithDamage(3);
        WithEnergy(1);
    }

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (power is PoisonPower && applier == Owner.Creature)
        {
            EnergyCost.AddThisCombat(-1);
        }
        return Task.CompletedTask;
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var poisonPower = Owner.Creature.Powers.ToList().Find(p => p is PoisonPower);
        if (poisonPower is null) return;
        var amount = poisonPower.Amount;
        await CommonActions.CardAttack(this, play, amount).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1);
    }
}