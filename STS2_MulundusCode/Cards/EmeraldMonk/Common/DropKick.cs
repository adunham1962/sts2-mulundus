using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;
[Pool(typeof(EmeraldMonkCardPool))]
public class DropKick : EmeraldMonkCard
{
    
    public DropKick() : base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithCalculatedDamage(8, 3,
            (card, _) => CombatManager.Instance.History.Entries.OfType<CardPlayFinishedEntry>().Count((e) =>
                e.HappenedThisTurn(card.CombatState) && e.Actor == card.Owner.Creature));
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (play.Target != null)
            await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromCard(this).Targeting(play.Target)
                .WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(2);
        DynamicVars.ExtraDamage.UpgradeValueBy(1);
    }
}