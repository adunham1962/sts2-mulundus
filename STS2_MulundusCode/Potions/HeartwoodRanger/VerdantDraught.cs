using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Potions.HeartwoodRanger;

[Pool(typeof(Heartwood_Ranger_PotionPool))]
public class VerdantDraught : STS2_MulundusPotion
{
    public override PotionRarity Rarity => PotionRarity.Common;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.AnyPlayer;
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (target is null) return;
        await PowerCmd.Apply<PoisonPower>(target, 3, Owner.Creature, null);
        await PowerCmd.Apply<StrengthPower>(target, 1, Owner.Creature, null);
        if (target.Player is null) return;
        await CardPileCmd.Draw(choiceContext, 3, target.Player);
    }
}