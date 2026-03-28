using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items
{
	public class EldritchSoulArtifact : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eldritch Soul Artifact");
			/* Tooltip.SetDefault("Knowledge\n" +
                "Boosts melee speed by 10%, shoot speed by 25%, rogue damage by 15%, max minions by 2, and reduces mana cost by 15%"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.value = Item.buyPrice(1, 50, 0, 0);
			Item.accessory = true;
			Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.eArtifact = true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Cinderplate", 5);
            recipe.AddIngredient(null, "EssenceofChaos", 10);
            recipe.AddIngredient(null, "Phantoplasm", 10);
            recipe.AddIngredient(null, "ExodiumClusterOre", 15);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
        }
	}
}