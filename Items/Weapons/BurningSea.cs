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
	public class BurningSea : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Burning Sea");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 79;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 15;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 6.5f;
	        Item.value = 300000;
	        Item.rare = ItemRarityID.LightPurple;
	        Item.UseSound = SoundID.Item20;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("BrimstoneFireball").Type;
	        Item.shootSpeed = 15f;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyCore", 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}