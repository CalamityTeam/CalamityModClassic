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
	public class FrigidflashBolt : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Frigidflash Bolt");
			//Tooltip.SetDefault("Casts a slow-moving ball of flash-freezing magma");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 46;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 13;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 15;
	        Item.useAnimation = 15;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 5.5f;
	        Item.value = 500000;
	        Item.rare = ItemRarityID.LightPurple;
	        Item.UseSound = SoundID.Item21;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("FrigidflashBoltProjectile").Type;
	        Item.shootSpeed = 6.5f;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "FrostBolt");
			recipe.AddIngredient(null, "FlareBolt");
			recipe.AddIngredient(null, "EssenceofEleum", 2);
			recipe.AddIngredient(null, "EssenceofChaos", 2);
	        recipe.AddTile(TileID.Bookcases);
	        recipe.Register();
		}
	}
}