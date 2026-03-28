using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class YharimsInsignia : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yharim's Insignia");
            /* Tooltip.SetDefault("10% increased damage when under 50% life\n" +
                "10% increased melee speed\n" +
                "5% increased melee damage\n" +
                "Melee attacks and melee projectiles inflict holy fire\n" +
                "Increased invincibility after taking damage\n" +
                "Temporary immunity to lava\n" +
                "Increased melee knockback"); */
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 38;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.yInsignia = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WarriorEmblem);
            recipe.AddIngredient(null, "NecklaceofVexation");
            recipe.AddIngredient(null, "CoreofCinder", 5);
            recipe.AddIngredient(ItemID.CrossNecklace);
            recipe.AddIngredient(null, "BadgeofBravery");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}