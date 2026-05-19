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
public class LostInDespair : ConstructedCardModel
{
    public LostInDespair() : base(1, CardType.Status, CardRarity.Status, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
        WithPower<WeakPower>(1);
    }
    
    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card == this)
        {
            await CommonActions.ApplySelf<WeakPower>(this);
        }
    }
    
    public static IEnumerable<LostInDespair> Create(Player owner, decimal amount, CombatState combatState)
    {
        var list = new List<LostInDespair>();
        for (var index = 0; index < amount; ++index)
            list.Add(combatState.CreateCard<LostInDespair>(owner));
        return list;
    }
    
    public override int MaxUpgradeLevel => 0;
}