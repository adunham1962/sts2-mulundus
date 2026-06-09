using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rooms;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;


[Pool(typeof(EventRelicPool))]
public class ReflectedDream() : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/reflected_dream.png";

    private int _powerCount = 0;
    
    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        if (card.Type != CardType.Power || card.Owner != Owner || _powerCount > 0) return playCount;
        _powerCount++;
        return playCount + 1;
    }

    public override Task AfterCombatEnd(CombatRoom room)
    {
        _powerCount = 0;
        return Task.CompletedTask;
    }
}