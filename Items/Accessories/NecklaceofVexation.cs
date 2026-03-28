using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class NecklaceofVexation : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Necklace of Vexation");
            /* Tooltip.SetDefault("Revenge\n" +
            "15% increased damage when under 50% life"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.vexation = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DraedonBar", 2);
            recipe.AddIngredient(ItemID.AvengerEmblem);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}