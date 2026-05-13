using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Extensions;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Common;
[Pool(typeof(HeartwoodRangerCardPool))]
public class WitheringCleave : HeartWoodRangerCard
{
    public override string PortraitPath => "Cilef Base.png".CardImagePath();
    public WitheringCleave() : base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
    {
        WithDamage(4);
        WithKeyword(HeartwoodRangerKeywords.Grim);
        WithPower<WeakPower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (CombatState is not null)
        {
            foreach (var combatStateEnemy in CombatState.Enemies)
            {
                await CommonActions.Apply<WeakPower>(combatStateEnemy, this);
            }
        }
    }

    protected override void OnUpgrade()
    {

    }
}