using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.TheDevourerofGods
{
	public class CosmicWorm : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cosmic Worm");
			//Tooltip.SetDefault("Summons the devourer of the cosmos");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 0);
	            }
	        }
	    }
		
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(Mod.Find<ModNPC>("DevourerofGodsHead").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DevourerofGodsHead").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(null, "RuinousSoul", 3);
			recipe.AddIngredient(null, "ArmoredShell", 3);
			recipe.AddIngredient(null, "TwistingNether");
			recipe.AddIngredient(null, "DarkPlasma");
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}