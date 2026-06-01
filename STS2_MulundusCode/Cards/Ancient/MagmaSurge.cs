using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Ancient;
[Pool(typeof(EventCardPool))]
public class MagmaSurge: ConstructedCardModel
{
    
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/magma_surge.png";
    
    public MagmaSurge() : base(0, CardType.Attack, CardRarity.Ancient, TargetType.RandomEnemy)
    {
        WithDamage(12);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }
}