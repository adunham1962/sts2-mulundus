using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class BloomingGrove : HeartWoodRangerCard
{
    
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    
    public BloomingGrove() : base(1, CardType.Skill, CardRarity.Rare, TargetType.AllAllies)
    {
        WithHeal(6);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState is null) return;
        foreach (var creature in CombatState.GetTeammatesOf(Owner.Creature).Where(c => c is { IsAlive: true, IsPlayer: true }))
        {
            await CreatureCmd.Heal(creature, DynamicVars.Heal.BaseValue);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Heal.UpgradeValueBy(3);
    }
}