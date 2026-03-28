using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items.Patreon
{
	public class NormalityRelocator : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Normality Relocator");
			/* Tooltip.SetDefault("I'll be there in the blink of an eye\n" +
				"Press Z to teleport to the position of the mouse\n" +
				"Boosts movement and fall speed by 10%\n" +
				"Works while in the inventory"); */
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 7));
			ItemID.Sets.AnimatesAsSoul[Type] = true;
		}
		
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 38;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
		}

		public override void UpdateInventory(Player player)
		{
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.normalityRelocator = true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.RodofDiscord);
            recipe.AddIngredient(ItemID.FragmentStardust, 30);
            recipe.AddIngredient(null, "ExodiumClusterOre", 10);
			recipe.AddIngredient(null, "Cinderplate", 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
        }
	}
}