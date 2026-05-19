using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Extensions;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;
public class InvokeYourOathPower : CustomPowerModel
{
    
    //Loads from STS2_Mulundus/images/powers/your_power.png
    public override string CustomPackedIconPath => "res://STS2_Mulundus/images/powers/invoke_your_oath_power.png";

    public override string CustomBigIconPath => "res://STS2_Mulundus/images/powers/big/invoke_your_oath_power.png";
    
    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        if (card.Owner.Creature != this.Owner || card.Type != CardType.Attack)
        {
            return playCount;
        }

        return playCount + 1;
    }

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;
}