using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;

namespace CalamityModClassicPreTrailer.Items
{
	public class AridArtifact : ModItem
	{
		public override void SetStaticDefaults()
	 	{
	 		// DisplayName.SetDefault("Arid Artifact");
	 		/* Tooltip.SetDefault("Summons a sandstorm\n" +
	 		                   "The sandstorm will happen shortly after the item is used"); */
	 	}
	
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.rare = 5;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item66;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Sandstorm.Happening;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			Sandstorm.StartSandstorm();
			// reflection not needed anymore
			// typeof(Sandstorm).GetMethod("StartSandstorm", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, null);
			return true;
		}
		
		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
			recipe.AddIngredient(ItemID.AdamantiteBar, 5);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
			recipe.AddIngredient(ItemID.TitaniumBar, 5);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	}
}