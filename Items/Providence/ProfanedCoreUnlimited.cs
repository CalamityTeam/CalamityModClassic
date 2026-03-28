using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityModClassicPreTrailer.Items.Providence
{
	public class ProfanedCoreUnlimited : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Profaned Core");
			/* Tooltip.SetDefault("The core of the unholy flame\n" +
                "Summons Providence\n" +
                "Can only be used during daytime\n" +
                "Not consumable"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = false;
			Item.rare = 9;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(Mod.Find<ModNPC>("Providence").Type) && Main.dayTime && (player.ZoneHallow || player.ZoneUnderworldHeight) && CalamityWorldPreTrailer.downedBossAny;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Providence").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ProfanedCore");
            recipe.AddIngredient(null, "UnholyEssence", 50);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}