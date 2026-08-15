using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
[Pool(typeof(TokenCardPool))]
public class RainBarrage : EmeraldMonkCard
{
    public RainBarrage() : base(1, CardType.Attack, CardRarity.Token, TargetType.AllEnemies)
    {
        WithDamage(1);
        WithKeyword(EmeraldMonkKeywords.Stance);
        WithVar("Hits", 2);
        WithPower<DownpourPower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var hits = DynamicVars["Hits"].IntValue;
        await CommonActions.CardAttack(this, play, hits).Execute(choiceContext);
        if (CombatState != null)
            await CommonActions.Apply<DownpourPower>(choiceContext, CombatState.HittableEnemies, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Hits"].UpgradeValueBy(1);
    }
    
    public static IEnumerable<RainBarrage> Create(Player owner, decimal amount, ICombatState combatState)
    {
        var rainBarrages = new List<RainBarrage>();
        for (var index = 0; index < amount; ++index)
            rainBarrages.Add(combatState.CreateCard<RainBarrage>(owner));
        return rainBarrages;
    }
}