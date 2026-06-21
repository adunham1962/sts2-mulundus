using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Token;
[Pool(typeof(TokenCardPool))]
public class Goodberry : ConstructedCardModel
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/goodberry.png";
    public Goodberry() : base(0, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithHeal(1);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.Heal(Owner.Creature, DynamicVars.Heal.BaseValue);
    }

    public static IEnumerable<Goodberry> Create(Player owner, decimal amount, ICombatState combatState)
    {
        var goodBerryList = new List<Goodberry>();
        for (var index = 0; index < amount; ++index)
            goodBerryList.Add(combatState.CreateCard<Goodberry>(owner));
        return goodBerryList;
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars.Heal.UpgradeValueBy(1);
    }
}