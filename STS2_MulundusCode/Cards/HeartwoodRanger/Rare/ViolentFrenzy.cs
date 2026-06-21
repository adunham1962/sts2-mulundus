using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class ViolentFrenzy : HeartWoodRangerCard
{
    public ViolentFrenzy() : base(9, CardType.Attack, CardRarity.Rare, TargetType.RandomEnemy)
    {
        WithDamage(6);
        WithVar("hits", 7);
        WithEnergy(1);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play, DynamicVars["hits"].IntValue).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1);
        DynamicVars["hits"].UpgradeValueBy(1);
    }
    

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (power.Type == PowerType.Debuff && amount > 0)
        {
            EnergyCost.AddThisTurn(-1 * Convert.ToInt32(amount));
        }
        return Task.CompletedTask;
    }
}