using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Enchantments;

public class AstralReinforcement : CustomEnchantmentModel
{
    protected override string CustomIconPath => "res://STS2_Mulundus/images/enchantments/astral_reinforcement.png";
    
    public override bool CanEnchant(CardModel card)
    {
        return base.CanEnchant(card) && card.DynamicVars.ContainsKey("Block") && card.DynamicVars.Block.BaseValue > 0 || card.DynamicVars.ContainsKey("CalculatedBlock");
    }

    public override decimal EnchantBlockMultiplicative(decimal originalBlock)
    {
        return 2;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card == Card)
            await CommonActions.ApplySelf<DexterityPower>(choiceContext, cardPlay.Card, -1m);
    }
}