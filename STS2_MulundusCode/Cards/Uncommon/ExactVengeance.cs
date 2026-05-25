using BaseLib.Utils;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class ExactVengeance : HeartWoodRangerCard
{
    private Creature[] _validTargets = [];
    
    public ExactVengeance() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithCalculatedDamage(12,
            (card, creature) => card.Owner.Creature.Powers.Any(p => p.Type == PowerType.Debuff) ? 12 : 0);
        WithKeyword(CardKeyword.Exhaust);
    }

   //protected new IEnumerable<DynamicVar> CanonicalVars =
   // [
    //    new CalculationBaseVar(12),
    //    new ExtraDamageVar(12),
    //    new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, creature) )
    //];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (play.Target is null) return;
        await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromCard(this).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (target != Owner.Creature || dealer is null) return Task.CompletedTask;
        _validTargets.AddItem(dealer);
        return Task.CompletedTask;
    }

    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != Owner.Creature.Side) return Task.CompletedTask;
        _validTargets = [];
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }

    public override bool ShouldAllowHitting(Creature creature)
    {
        return creature.IsEnemy && _validTargets.Contains(creature);
    }
}