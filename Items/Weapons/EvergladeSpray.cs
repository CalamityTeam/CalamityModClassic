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
	public class EvergladeSpray : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Everglade Spray");
			//Tooltip.SetDefault("Fires a stream of burning green ichor");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 28;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 8;
	        Item.width = 30;
	        Item.height = 30;
	        Item.useTime = 6;
	        Item.useAnimation = 18;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 4.5f;
	        Item.value = 550000;
	        Item.rare = ItemRarityID.LightPurple;
	        Item.UseSound = SoundID.Item13;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("EvergladeSprayProjectile").Type;
	        Item.shootSpeed = 10f;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Ichor, 20);
			recipe.AddIngredient(null, "DraedonBar", 3);
	        recipe.AddTile(TileID.Bookcases);
	        recipe.Register();
		}
	}
}