using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Ancient;
[Pool(typeof(EventCardPool))]
public class EternitysResolve : ConstructedCardModel
{

    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/eternitys_resolve.png";
    
    public EternitysResolve() : base(1, CardType.Skill, CardRarity.Ancient, TargetType.Self)
    {
        WithKeyword(CardKeyword.Eternal);
        WithPower<PlatingPower>(6);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<PlatingPower>(choiceContext, this);
        await CreatureCmd.LoseMaxHp(choiceContext, Owner.Creature, 1, true);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}