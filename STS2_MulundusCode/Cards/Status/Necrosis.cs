using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Status;
[Pool(typeof(StatusCardPool))]
public class Necrosis : ConstructedCardModel
{
    public Necrosis() : base(1, CardType.Status, CardRarity.Status, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
        WithPower<FrailPower>(1);
    }

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card == this)
        {
            await CommonActions.ApplySelf<FrailPower>(this);
        }
    }
    
    public static IEnumerable<Necrosis> Create(Player owner, decimal amount, CombatState combatState)
    {
        var list = new List<Necrosis>();
        for (var index = 0; index < amount; ++index)
            list.Add(combatState.CreateCard<Necrosis>(owner));
        return list;
    }
    
    public override int MaxUpgradeLevel => 0;
}