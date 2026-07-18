using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Ancient;

[Pool(typeof(EventCardPool))]
public class HiredHelp : ConstructedCardModel
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/hired_help.png";
    
    public HiredHelp() : base(0, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy)
    {
        WithDamage(15);
        WithBlock(15);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        {
            var goldLoss = Owner.Gold >= 10 ? 10 : Owner.Gold;
            await PlayerCmd.LoseGold(goldLoss, Owner);
            if (goldLoss < 10)
            {
                await CreatureCmd.LoseMaxHp(choiceContext, Owner.Creature, 1, true);
            }

            await CommonActions.CardAttack(this, play).Execute(choiceContext);
            await CommonActions.CardBlock(this, play);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(5);
        DynamicVars.Block.UpgradeValueBy(5);
    }
}