using BaseLib.Abstracts;
using Godot;
using STS2_Mulundus.STS2_MulundusCode.Extensions;

namespace STS2_Mulundus.STS2_MulundusCode.Character;

public class EmeraldMonkPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => EmeraldMonk.Color;
    
    public override string BigEnergyIconPath => "charui\\big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui\\text_energy.png".ImagePath();
}