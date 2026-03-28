using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer;

public static class CalamityHelper
{
    public static NPCShop AddWithCustomValue(this NPCShop shop, int itemType, int customValue, params Condition[] conditions)
    {
        var item = new Item(itemType)
        {
            shopCustomPrice = customValue
        };
        return shop.Add(item, conditions);
    }
    public static Condition Create(string key, Func<bool> predicate)
    {
        return new Condition(
            Language.GetText($"Mods.CalamityModClassicPreTrailer.Condition.{key}"),
            predicate
        );
    }
}