using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Random;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;
[Pool(typeof(EmeraldMonkCardPool))]
public class Command : EmeraldMonkCard
{
    public Command() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithPower<VulnerablePower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (play.Target != null)
        {
            await CommonActions.Apply<VulnerablePower>(choiceContext, play.Target, this);
            if (play.Target.IsMonster && CombatState is not null)
            {
                play.Target.Monster?.MoveStateMachine?.OnMovePerformed(new MoveState());
                play.Target.PrepareForNextTurn(CombatState.PlayerCreatures);
            }
        }
        
    }

    protected override void OnUpgrade()
    {
       EnergyCost.UpgradeBy(-1);
    }
}