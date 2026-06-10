using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace STS2_Mulundus.STS2_MulundusCode;

public static class EmeraldMonkKeywords
{
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword EnterStance;
    
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Stance;

    public static bool HasEnterStance(this CardModel card)
    {
        return card.Keywords.Contains(EnterStance);
    }
    
    public static bool IsStance(this CardModel card)
    {
        return card.Keywords.Contains(Stance);
    }
}