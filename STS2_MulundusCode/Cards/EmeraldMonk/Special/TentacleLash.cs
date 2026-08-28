using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;

[Pool(typeof(TokenCardPool))]
public class TentacleLash : EmeraldMonkCard
{

    public TentacleLash() : base(1, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy)
    {
        WithDamage(4);
        WithBlock(4);
        WithKeyword(EmeraldMonkKeywords.Stance);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await CommonActions.CardBlock(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1);
        DynamicVars.Block.UpgradeValueBy(1);
    }
    
    public static IEnumerable<TentacleLash> Create(Player owner, decimal amount, ICombatState combatState)
    {
        var tentacleLashes = new List<TentacleLash>();
        for (var index = 0; index < amount; ++index)
            tentacleLashes.Add(combatState.CreateCard<TentacleLash>(owner));
        return tentacleLashes;
    }
}