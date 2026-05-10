using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Runs;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(HeartwoodRangerRelicPool))]
public class DarkContract() : STS2_MulundusRelic()
{
    
    public override RelicRarity Rarity => RelicRarity.Rare;

    public override async Task AfterObtained()
    {
        await CreatureCmd.LoseMaxHp(new ThrowingPlayerChoiceContext(), Owner.Creature, 10, false);
    }
    
    public override CardCreationOptions ModifyCardRewardCreationOptions(Player player, CardCreationOptions options)
    {
        if (Owner != player || options.Flags.HasFlag(CardCreationFlags.NoCardPoolModifications))
            return options;
        var list1 = options.GetPossibleCards(player).ToList();
        var list2 = ModelDb.CardPool<NecrobinderCardPool>().GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint).ToList();
        if (options.Flags.HasFlag(CardCreationFlags.NoRarityModification))
        {
            var allowedRarities = options.GetPossibleCards(player).Select(c => c.Rarity).ToHashSet();
            list2 = list2.Where(c => allowedRarities.Contains(c.Rarity)).ToList();
        }
        foreach (var cardModel in list2.Where(cardModel => !list1.Contains(cardModel)))
        {
            list1.Add(cardModel);
        }
        return options.WithCustomPool(list1);
    }
}