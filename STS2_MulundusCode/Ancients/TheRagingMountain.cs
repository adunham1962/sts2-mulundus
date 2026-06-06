using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Ancients;

public class TheRagingMountain : CustomAncientModel
{
    protected override OptionPools MakeOptionPools => new([
        AncientOption<TheFirstFlame>()
    ], [
        AncientOption<DragonTitansScale>(),
        AncientOption<HoardkeepersFang>(),
    ],[
        AncientOption<PressurizedMagma>()
    ]);
    
    public override bool IsValidForAct(ActModel act) => false;
    
    public override bool ShouldForceSpawn(ActModel act, AncientEventModel? rngChosenAncient) => rngChosenAncient is Orobas or Pael;
    
    public override string? CustomScenePath => "res://STS2_Mulundus/scenes/events/background_scenes/sts2_mulundus-the_raging_mountain.tscn";

    public override string? CustomMapIconPath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_raging_mountain.png";

    public override string? CustomMapIconOutlinePath => "res://STS2_Mulundus/images/packed/map/ancients/ancient_node_sts2_mulundus-the_raging_mountain_outline.png";
    
    public override string? CustomRunHistoryIconPath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_raging_mountain.png";

    public override string? CustomRunHistoryIconOutlinePath => "res://STS2_Mulundus/images/packed/map/ancients/ancient_node_sts2_mulundus-the_raging_mountain_outline.png";
}