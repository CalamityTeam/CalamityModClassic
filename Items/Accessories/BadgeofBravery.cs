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
    public class BadgeofBravery : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Badge of Bravery");
            // Tooltip.SetDefault("15% increased melee speed");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 21, 0, 0);
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.badgeOfBravery = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "UeliaceBar", 2);
            recipe.AddIngredient(ItemID.FeralClaws);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

		public override void OnCraft(Recipe recipe)
		{
			if (Main.rand.Next(40) == 0)
				recipe.createItem.type = Mod.Find<ModItem>("SamuraiBadge").Type;
			else
				recipe.createItem.type = Item.type;
		}
	}
}