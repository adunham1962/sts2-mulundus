using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Cards.Token;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Potions.HeartwoodRanger;
[Pool(typeof(HeartwoodRangerPotionPool))]
public class BerryPreserves : STS2_MulundusPotion
{
    public override PotionRarity Rarity => PotionRarity.Uncommon;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.AnyPlayer;

    public override string CustomPackedImagePath => "res://STS2_Mulundus/images/potions/berry_preserves.png";
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (target?.Player is null || target.CombatState is null) return;
        var berries = Goodberry.Create(target.Player, 3, target.CombatState);
        var cardModels = berries.ToList();
        foreach (var goodberry in cardModels)
        {
           CardCmd.Upgrade(goodberry);
        }

        await CardPileCmd.AddGeneratedCardsToCombat(cardModels, PileType.Hand, Owner);
    }
}