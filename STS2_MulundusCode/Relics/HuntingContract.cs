using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.CardRewardAlternatives;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.Rewards;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rewards;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class HuntingContract : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/hunting_contract.png";
    
    public override bool TryModifyCardRewardAlternatives(
        Player player,
        CardReward cardReward,
        List<CardRewardAlternative> alternatives)
    {
        if (Owner != player)
            return false;
        alternatives.Add(new CardRewardAlternative("DELIVER", OnDeliver, PostAlternateCardRewardAction.EndSelectionAndCompleteReward));
        return true;
    }
    
    public async Task OnDeliver()
    {
        Flash();
        await TaskHelper.RunSafely(DoActivateVisuals());
        await PlayerCmd.GainGold(50, Owner);
    }
    
    private async Task DoActivateVisuals()
    {
        IsActivating = true;
        await Cmd.Wait(1f);
        IsActivating = false;
    }
    
    private bool _isActivating;
    
    private bool IsActivating
    {
        get => _isActivating;
        set
        {
            AssertMutable();
            _isActivating = value;
            InvokeDisplayAmountChanged();
        }
    }
}