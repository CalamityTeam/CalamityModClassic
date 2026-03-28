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
	public class ProfanedSoulArtifact : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Profaned Soul Artifact");
			/* Tooltip.SetDefault("Purity\n" +
                "Summons a healer guardian which heals for a certain amount of health every few seconds\n" +
				"Summons a defensive guardian if you have at least 8 minion slots, which boosts your movement speed and your damage resistance\n" +
				"Summons an offensive guardian if you are wearing the tarragon summon set (or stronger), which boosts your summon damage and your minion slots\n" +
				"If you get hit their effects will disappear for 5 seconds"); */
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 6));
			ItemID.Sets.AnimatesAsSoul[Type] = true;
		}
		
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 40;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.pArtifact = true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Cinderplate", 5);
            recipe.AddIngredient(null, "CoreofCinder");
            recipe.AddIngredient(null, "DivineGeode", 5);
            recipe.AddIngredient(null, "ExodiumClusterOre", 15);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
        }
	}
}