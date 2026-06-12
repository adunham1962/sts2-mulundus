using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
[Pool(typeof(TokenCardPool))]
public class DragonsTorrent :EmeraldMonkCard
{

    public DragonsTorrent() : base(3, CardType.Attack, CardRarity.Token, TargetType.AllEnemies)
    {
        WithKeyword(CardKeyword.Retain);
        WithKeyword(EmeraldMonkKeywords.Stance);
        WithDamage(16);
        WithPower<WeakPower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (CombatState?.HittableEnemies != null)
            await CommonActions.Apply<WeakPower>(choiceContext, CombatState.HittableEnemies, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(5);
        DynamicVars["WeakPower"].UpgradeValueBy(1);
    }
    
    public static IEnumerable<DragonsTorrent> Create(Player owner, decimal amount, CombatState combatState)
    {
        var dragonsTorrentList = new List<DragonsTorrent>();
        for (var index = 0; index < amount; ++index)
            dragonsTorrentList.Add(combatState.CreateCard<DragonsTorrent>(owner));
        return dragonsTorrentList;
    }
}