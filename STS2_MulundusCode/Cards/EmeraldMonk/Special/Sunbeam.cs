using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
[Pool(typeof(TokenCardPool))]
public class Sunbeam : EmeraldMonkCard
{

    public Sunbeam() : base(1, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy)
    {
        WithDamage(16);
        WithPower<VulnerablePower>(3);
        WithKeyword(CardKeyword.Retain);
        WithKeyword(EmeraldMonkKeywords.Stance);
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (play.Target is not null)
            await CommonActions.Apply<VulnerablePower>(choiceContext, play.Target, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4);
    }
    
    public static IEnumerable<Sunbeam> Create(Player owner, decimal amount, ICombatState combatState)
    {
        var sunbeam = new List<Sunbeam>();
        for (var index = 0; index < amount; ++index)
            sunbeam.Add(combatState.CreateCard<Sunbeam>(owner));
        return sunbeam;
    }
}