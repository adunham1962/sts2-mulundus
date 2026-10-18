using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;

[Pool(typeof(EmeraldMonkCardPool))]
public class CubicSurprise : EmeraldMonkCard
{

    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    public CubicSurprise() : base(3, CardType.Skill, CardRarity.Rare, TargetType.AllAllies)
    {
        WithPower<IntangiblePower>(1);
        WithPower<ShadowStepPower>(1);
        WithKeyword(CardKeyword.Exhaust);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState == null) return;
        await CommonActions.Apply<IntangiblePower>(choiceContext, CombatState.Allies, this);
        await CommonActions.Apply<ShadowStepPower>(choiceContext, CombatState.Allies, this);
        
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}