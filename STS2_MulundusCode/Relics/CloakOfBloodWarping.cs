using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using STS2_Mulundus.STS2_MulundusCode.Cards.Ancient;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(HeartwoodRangerRelicPool))]
public class CloakOfBloodWarping : STS2_MulundusRelic
{
    public override async Task AfterObtained()
    {
        var cardPileAddResultList = new List<CardPileAddResult>(1);
        var card = ModelDb.Card<BloodWarp>();
        var cardPileAddResult = await CardPileCmd.Add(Owner.RunState.CreateCard(card, Owner), PileType.Deck);
        cardPileAddResultList.Add(cardPileAddResult);
        CardCmd.PreviewCardPileAdd(cardPileAddResultList, 2f);
    }

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (room is not CombatRoom) return;
        await PowerCmd.Apply<DexterityPower>(new ThrowingPlayerChoiceContext(), Owner.Creature, 1, Owner.Creature, null);
    }

    public override RelicRarity Rarity => RelicRarity.Rare;
}