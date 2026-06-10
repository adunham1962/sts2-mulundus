using BaseLib.Abstracts;
using Godot;

namespace STS2_Mulundus.STS2_MulundusCode.Character;

public class EmeraldMonkCardPool : CustomCardPoolModel
{
    public override Color DeckEntryCardColor => new("ffffff");
    public override bool IsColorless => false;
    
    public override string Title => EmeraldMonk.CharacterId; //This is not a display name.

    public override string BigEnergyIconPath => "res://STS2_Mulundus/images/charui/emerald_monk_big_energy.png";
    public override string TextEnergyIconPath => "res://STS2_Mulundus/images/charui/emerald_monk_text_energy.png";
    
    /* These HSV values will determine the color of your card back.
        They are applied as a shader onto an already colored image,
        so it may take some experimentation to find a color you like.
        Generally they should be values between 0 and 1. */
    
    public override float H => 0.49f; //Hue; changes the color.   // 177/
    public override float S => 0.54f; //Saturation                // 54 /
    public override float V => 0.82f; //Brightness                // 82 /
}