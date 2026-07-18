using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Ancients;


public class TheEverlasting : CustomAncientModel
{
    protected override OptionPools MakeOptionPools => new(
        [AncientOption<DreadnoughtHide>()], 
        [AncientOption<ArdensSimulacrum>()], 
        [AncientOption<HuntingContract>()]);
    
    public override bool IsValidForAct(ActModel act) => false;
    
    public override bool ShouldForceSpawn(ActModel act, AncientEventModel? rngChosenAncient) => rngChosenAncient is Darv;
    
    public override string? CustomScenePath => "res://STS2_Mulundus/scenes/events/background_scenes/sts2_mulundus-the_everlasting.tscn";

    public override string? CustomMapIconPath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_everlasting.png";

    public override string? CustomMapIconOutlinePath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_everlasting.png";
    
    public override string? CustomRunHistoryIconPath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_everlasting.png";

    public override string? CustomRunHistoryIconOutlinePath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_everlasting.png";
    
}