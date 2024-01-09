using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;

namespace CalamityModClassic1Point2.Items
{
	public class AridArtifact : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item66;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Sandstorm.Happening;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			Sandstorm.Happening = true;
			Sandstorm.TimeLeft = (int)(3600f * (8f + Main.rand.NextFloat() * 16f));
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
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