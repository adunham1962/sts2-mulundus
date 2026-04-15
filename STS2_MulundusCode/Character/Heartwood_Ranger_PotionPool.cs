using BaseLib.Abstracts;
using STS2_Mulundus.STS2_MulundusCode.Extensions;
using Godot;

namespace STS2_Mulundus.STS2_MulundusCode.Character;

public class Heartwood_Ranger_PotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => HeartwoodRanger.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}