using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class JudgeAsWorthy : HeartWoodRangerCard
{
    
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    protected override bool HasEnergyCostX => true;

    public JudgeAsWorthy() : base(-1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly)
    {
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var count = ResolveEnergyXValue();
        if (IsUpgraded) ++count;
        var loss = count * 2;
        var heal = loss * 2;

        await CreatureCmd.Damage(choiceContext, Owner.Creature, new DamageVar(loss, ValueProp.Move), this);
        if (play.Target is null) return;
        await CreatureCmd.Heal(play.Target, heal);
    }
}