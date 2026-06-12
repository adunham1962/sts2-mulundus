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
    
    [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Ebb;

    [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Flow;
    
    public static bool HasEbb (this CardModel card) => card.Keywords.Contains(Ebb);
    
    public static bool HasFlow (this CardModel card) => card.Keywords.Contains(Flow);
    
    public static bool HasEnterStance(this CardModel card)
    {
        return card.Keywords.Contains(EnterStance);
    }
    
    public static bool IsStance(this CardModel card)
    {
        return card.Keywords.Contains(Stance);
    }
}