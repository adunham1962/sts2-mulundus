using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
namespace STS2_Mulundus.STS2_MulundusCode;

public static class HeartwoodRangerKeywords
{
    [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Grim;

    public static bool IsGrim(this CardModel card)
    {
        return card.Keywords.Contains(Grim);
    }
}