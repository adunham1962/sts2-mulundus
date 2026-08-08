using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace STS2_Mulundus.STS2_MulundusCode.Cards;

public abstract class MulundusCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    ConstructedCardModel(cost, type, rarity, target)
{

    protected async Task Absorb<TPower>(PlayerChoiceContext context, IReadOnlyList<Creature> targets) where TPower : PowerModel
    {
        var amount = targets.Sum(creature => creature.GetPowerAmount<TPower>());
        foreach (var creature in targets)
        {
            await PowerCmd.Remove<TPower>(creature);
        }
        await CommonActions.ApplySelf<TPower>(context, this, amount);
    }
    
    protected async Task Absorb<TPower>(PlayerChoiceContext context, Creature target) where TPower : PowerModel
    {
        var amount = target.GetPowerAmount<TPower>();

        await PowerCmd.Remove<TPower>(target);
        await CommonActions.ApplySelf<TPower>(context, this, amount);
    }

}