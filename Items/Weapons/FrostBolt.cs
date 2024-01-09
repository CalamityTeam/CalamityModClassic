using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class FrostBolt : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Frost Bolt");
			//Tooltip.SetDefault("Casts a slow-moving ball of frost");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 12;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 6;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 16;
	        Item.useAnimation = 16;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3.5f;
	        Item.value = 30000;
	        Item.rare = ItemRarityID.Green;
	        Item.UseSound = SoundID.Item8;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("FrostBoltProjectile").Type;
	        Item.shootSpeed = 6f;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IceBlock, 20);
			recipe.AddIngredient(ItemID.Shiverthorn, 2);
			recipe.AddIngredient(ItemID.SnowBlock, 10);
			recipe.AddIngredient(ItemID.WaterBucket);
	        recipe.AddTile(TileID.Bookcases);
	        recipe.Register();
		}
	}
}