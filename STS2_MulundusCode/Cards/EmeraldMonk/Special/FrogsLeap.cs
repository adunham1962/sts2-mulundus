using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
[Pool(typeof(TokenCardPool))]
public class FrogsLeap : EmeraldMonkCard
{
    public FrogsLeap() : base(2, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithKeyword(EmeraldMonkKeywords.Stance);
        WithKeyword(CardKeyword.Retain);
        WithBlock(12);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(4);
    }
    
    public static IEnumerable<FrogsLeap> Create(Player owner, decimal amount, ICombatState combatState)
    {
        var frogsLeaps = new List<FrogsLeap>();
        for (var index = 0; index < amount; ++index)
            frogsLeaps.Add(combatState.CreateCard<FrogsLeap>(owner));
        return frogsLeaps;
    }
}