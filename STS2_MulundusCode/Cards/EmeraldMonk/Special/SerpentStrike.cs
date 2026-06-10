using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;

[Pool(typeof(TokenCardPool))]
public class SerpentStrike : EmeraldMonkCard 
{

    public SerpentStrike() : base(1, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy)
    {
        WithDamage(6);
        WithPower<PoisonPower>(5);
        WithKeyword(CardKeyword.Retain);
        WithKeyword(EmeraldMonkKeywords.Stance);
        WithTags(CardTag.Strike);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (play.Target != null) await CommonActions.Apply<PoisonPower>(choiceContext, play.Target, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
    
    public static IEnumerable<SerpentStrike> Create(Player owner, decimal amount, CombatState combatState)
    {
        var serpentStrikeList = new List<SerpentStrike>();
        for (var index = 0; index < amount; ++index)
            serpentStrikeList.Add(combatState.CreateCard<SerpentStrike>(owner));
        return serpentStrikeList;
    }
}