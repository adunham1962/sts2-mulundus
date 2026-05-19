using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(HeartwoodRangerRelicPool))]
public class HeartwoodStaff() : STS2_MulundusRelic()
{
    
    public override RelicRarity Rarity => RelicRarity.Starter;
    
   // public override bool ShowCounter => true;

  //  public override int DisplayAmount => _attacksPlayed % 4;

   // private int _attacksPlayed = 0;

  //  private bool _isActivating;
    
 //   [SavedProperty]
  //  public int AttacksPlayed
  //  {
   //     get => _attacksPlayed;
    //    private set
  //      {
    //        AssertMutable();
 //           _attacksPlayed = value % 4;
  //          UpdateDisplay();
  //      }
  //  }
    
 //   private bool IsActivating
 //   {
  //      get => _isActivating;
  //      set
  //      {
  //          AssertMutable();
  //          _isActivating = value;
   //         UpdateDisplay();
    //    }
 //   }
    
  //  private void UpdateDisplay()
 //   {
  //      if (IsActivating)
  //          Status = RelicStatus.Normal;
   //     else
  //          Status = AttacksPlayed == 1 ? RelicStatus.Active : RelicStatus.Normal;
 //       InvokeDisplayAmountChanged();
//    }
    
  //  public void NotifyAttackPlayed()
  //  {
  //      ++AttacksPlayed;
  //      if (AttacksPlayed != 0)
   //         return;
   //     TaskHelper.RunSafely(DoActivateVisuals());
 //   }
    
  //  private async Task DoActivateVisuals()
   // {
    //    IsActivating = true;
    //    Flash();
   //     await Cmd.Wait(1f);
  //      IsActivating = false;
   // }
    
  //  public override Task BeforeCardPlayed(CardPlay cardPlay)
   // {
    //    if (!DoesCardCountForRelic(cardPlay.Card))
    //        return Task.CompletedTask;
   //     NotifyAttackPlayed();
   //     return Task.CompletedTask;
  //  }
    
  //  public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
 //   {
     //   if (DoesCardCountForRelic(cardPlay.Card))
     //   { 
     //       if (AttacksPlayed % 4 == 0)
//{
     //           await CommonActions.ApplySelf<StrengthPower>(cardPlay.Card, 1);
    //        } 
    //    }
        
    //}

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (room.RoomType is not (RoomType.Boss or RoomType.Elite or RoomType.Monster))
            return;
        Flash();
        var amount = CardPile.GetCards(Owner, PileType.Deck).Count() / 10;
        await PowerCmd.Apply<StrengthPower>(Owner.Creature, amount, Owner.Creature, null);
    }

   // private bool DoesCardCountForRelic(CardModel card)
   // {
   //     return card.Type == CardType.Attack && card.Owner == Owner;
   // } 
}