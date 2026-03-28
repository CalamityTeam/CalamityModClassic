using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class StormSurge : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Storm Surge");
			// Tooltip.SetDefault("Fear the storm\nDoes not consume ammo");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 22;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 58;
			Item.height = 22;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 5f;
			Item.UseSound = SoundID.Item122;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
			Item.autoReuse = false;
			Item.shoot = Mod.Find<ModProjectile>("StormSurge").Type;
			Item.shootSpeed = 10f;
		}
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "StormlionMandible");
			recipe.AddIngredient(null, "VictideBar", 2);
			recipe.AddIngredient(null, "AerialiteBar", 2);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}