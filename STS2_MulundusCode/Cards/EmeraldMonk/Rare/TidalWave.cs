using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;

[Pool(typeof(EmeraldMonkCardPool))]
public class TidalWave : EmeraldMonkCard
{

    public TidalWave() : base(4, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
    {
        WithPower<WeakPower>(1);
        WithDamage(33);
        WithKeyword(EmeraldMonkKeywords.Flow);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (CombatState != null) await CommonActions.Apply<WeakPower>(choiceContext, CombatState.HittableEnemies, this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}