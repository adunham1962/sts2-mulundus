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
public class BlindedByHatred : ConstructedCardModel
{
    public BlindedByHatred() : base(1, CardType.Status, CardRarity.Status, TargetType.None)
    {
        WithPower<VulnerablePower>(1);
        WithKeyword(CardKeyword.Exhaust);
    }

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card == this)
        {
            await CommonActions.ApplySelf<VulnerablePower>(choiceContext, this);
        }
    }

    public static IEnumerable<BlindedByHatred> Create(Player owner, decimal amount, CombatState combatState)
    {
        var list = new List<BlindedByHatred>();
        for (var index = 0; index < amount; ++index)
            list.Add(combatState.CreateCard<BlindedByHatred>(owner));
        return list;
    }
    
    public override int MaxUpgradeLevel => 0;
}