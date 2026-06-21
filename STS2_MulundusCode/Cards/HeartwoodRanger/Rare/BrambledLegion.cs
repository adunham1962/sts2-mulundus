using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class BrambledLegion : HeartWoodRangerCard
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    
    public BrambledLegion() : base(2, CardType.Skill, CardRarity.Rare, TargetType.AllAllies)
    {
        WithPower<ThornsPower>(2);
        WithBlock(5);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState is null) return;
        foreach (var creature in CombatState.GetTeammatesOf(Owner.Creature).Where(c => c is { IsAlive: true, IsPlayer: true }))
        {
            await PowerCmd.Apply<ThornsPower>(choiceContext, creature, DynamicVars["ThornsPower"].BaseValue, Owner.Creature, this);
            await CreatureCmd.GainBlock(creature, DynamicVars.Block, play);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ThornsPower"].UpgradeValueBy(1);
        DynamicVars.Block.UpgradeValueBy(3);
    }
}