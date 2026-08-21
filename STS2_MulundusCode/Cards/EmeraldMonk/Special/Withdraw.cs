using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
[Pool(typeof(TokenCardPool))]
public class Withdraw : EmeraldMonkCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/withdraw.png";
    public Withdraw() : base(1, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithBlock(10);
        WithKeyword(EmeraldMonkKeywords.Stance);
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
    
    public static IEnumerable<Withdraw> Create(Player owner, decimal amount, ICombatState combatState)
    {
        var withdraws = new List<Withdraw>();
        for (var index = 0; index < amount; ++index)
            withdraws.Add(combatState.CreateCard<Withdraw>(owner));
        return withdraws;
    }
}