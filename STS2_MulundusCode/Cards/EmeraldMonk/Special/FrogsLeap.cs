using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
[Pool(typeof(TokenCardPool))]
public class FrogsLeap : EmeraldMonkCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/frogs_leap.png";
    public FrogsLeap() : base(1, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithKeyword(EmeraldMonkKeywords.Stance);
        WithPower<FrogsLeapPower>(4);
        WithCards(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<FrogsLeapPower>(choiceContext, this);
        await CommonActions.Draw(this, choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["FrogsLeapPower"].UpgradeValueBy(2);
    }
    
    public static IEnumerable<FrogsLeap> Create(Player owner, decimal amount, ICombatState combatState)
    {
        var frogsLeaps = new List<FrogsLeap>();
        for (var index = 0; index < amount; ++index)
            frogsLeaps.Add(combatState.CreateCard<FrogsLeap>(owner));
        return frogsLeaps;
    }
}