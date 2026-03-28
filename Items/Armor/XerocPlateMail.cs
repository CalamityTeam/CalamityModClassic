using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class XerocPlateMail : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Xeroc Plate Mail");
            /* Tooltip.SetDefault("+20 max life\n" +
                "6% increased movement speed\n" +
                "7% increased rogue damage and critical strike chance\n" +
                "Armor of the cosmos"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 32, 0, 0);
			Item.rare = 9;
            Item.defense = 27;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 20;
            player.moveSpeed += 0.06f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 7;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.07f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "MeldiateBar", 22);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}