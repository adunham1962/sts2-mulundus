using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class LivingBastionPower() : CustomPowerModel()
{
    
    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        
        if (side != CombatSide.Player)
        {
            return;
        }
        
        await CreatureCmd.Heal(Owner, 2 * Amount);
    }

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}