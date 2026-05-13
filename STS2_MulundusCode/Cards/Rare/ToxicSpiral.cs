using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Extensions;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class ToxicSpiral : HeartWoodRangerCard
{
    public override string PortraitPath => "Cilef Base.png".CardImagePath();
    private int _exhaustedThisTurn = 0;
    
    public ToxicSpiral() : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithPower<PoisonPower>(1);
        WithPower<StrengthPower>(1);
        WithKeyword(CardKeyword.Exhaust);
    }

    public override Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        _exhaustedThisTurn++;
        return Task.CompletedTask;
    }

    public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        _exhaustedThisTurn = 0;
        return Task.CompletedTask;
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState != null)
        {
            await PowerCmd.Apply<StrengthPower>(Owner.Creature, DynamicVars["StrengthPower"].BaseValue * _exhaustedThisTurn, Owner.Creature, this);
            await PowerCmd.Apply<PoisonPower>(Owner.Creature, DynamicVars["PoisonPower"].BaseValue * _exhaustedThisTurn, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}