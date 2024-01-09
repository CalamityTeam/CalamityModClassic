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
	public class StormSurge : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Storm Surge");
			//Tooltip.SetDefault("Fear the storm\nDoes not consume ammo");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 25;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 58;
			Item.height = 22;
			Item.useTime = 22;
			Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5f;
			Item.UseSound = SoundID.Item122;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.autoReuse = false;
			Item.shoot = Mod.Find<ModProjectile>("StormSurge").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 10f;
		}
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "StormlionMandible");
			recipe.AddIngredient(null, "VictideBar", 2);
			recipe.AddIngredient(null, "AerialiteBar", 4);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}