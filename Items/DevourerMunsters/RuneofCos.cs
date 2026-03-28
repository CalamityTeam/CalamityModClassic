using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.DevourerMunsters
{
	public class RuneofCos : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rune of Kos");
			/* Tooltip.SetDefault("A relic of the profaned flame\n" +
                "Contains the power hunted relentlessly by the sentinels of the cosmic devourer\n" +
                "When used in certain areas of the world it will unleash them\n" +
                "Not consumable"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
		
		public override bool CanUseItem(Player player)
		{
			return (player.ZoneSkyHeight || player.ZoneUnderworldHeight || player.ZoneDungeon) &&
				(!NPC.AnyNPCs(Mod.Find<ModNPC>("StormWeaverHead").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("StormWeaverHeadNaked").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("CeaselessVoid").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("CosmicWraith").Type));
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.ZoneDungeon)
			{
				for (int num662 = 0; num662 < 2; num662++)
				{
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("DarkEnergySpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
				}
				NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("CeaselessVoid").Type);
			}
			else if (player.ZoneUnderworldHeight)
			{
				NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("CosmicWraith").Type);
			}
			else if (player.ZoneSkyHeight)
			{
				NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("StormWeaverHead").Type);
			}
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "UnholyEssence", 40);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}