using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Saves.Runs;
using STS2_Mulundus.STS2_MulundusCode.Cards.Ancient;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class PressurizedMagma : STS2_MulundusRelic
{
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/pressurized_magma.png";
    public override RelicRarity Rarity => RelicRarity.Ancient;

    private int _cardsPlayedThisTurn;
    
    public override bool ShowCounter => true;
    
    public override int DisplayAmount => _cardsPlayedThisTurn;
    
    private bool _isActivating;
    
    [SavedProperty]
    private int CardsPlayedThisTurn
    {
        get => _cardsPlayedThisTurn;
        set {
            AssertMutable();
            _cardsPlayedThisTurn = value;
            UpdateDisplay();
        }
    }
    
    private bool IsActivating
       {
          get => _isActivating;
          set
          { 
              AssertMutable();
              _isActivating = value;
              UpdateDisplay();
          }
       }
    
    private void UpdateDisplay()
    {
        if (IsActivating)
            Status = RelicStatus.Normal;
        else
            Status = CardsPlayedThisTurn == 3 ? RelicStatus.Active : RelicStatus.Normal;
        InvokeDisplayAmountChanged();
    }

    private void NotifyCardPlayed()
    {
       ++CardsPlayedThisTurn;
       if (CardsPlayedThisTurn != 4) return;
       TaskHelper.RunSafely(DoActivateVisuals());
    }
    
    private async Task DoActivateVisuals()
    {
       IsActivating = true;
       Flash();
       await Cmd.Wait(1f);
       IsActivating = false;
    }
    
    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (!DoesCardCountForRelic(cardPlay.Card)) return Task.CompletedTask;
        NotifyCardPlayed();
        return Task.CompletedTask;
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<MagmaSurge>()
    ];
    
    private bool DoesCardCountForRelic(CardModel card) => card.Owner == Owner; 

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (!DoesCardCountForRelic(cardPlay.Card)) return;
        CardsPlayedThisTurn++;
        if (CardsPlayedThisTurn != 4) return;
        var magmaSurge = ModelDb.Card<MagmaSurge>();
        var cardPileAddResult = await CardPileCmd.AddGeneratedCardToCombat(Owner.Creature.CombatState?.CreateCard(magmaSurge, Owner)!, PileType.Discard, Owner);
        CardCmd.PreviewCardPileAdd([cardPileAddResult], 2f);
    }

    public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != Owner.Creature.Side) return Task.CompletedTask;
        CardsPlayedThisTurn = 0;
        return Task.CompletedTask;
    }
}