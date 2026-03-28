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
	public class ExsanguinationLance : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Exsanguination Lance");
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.damage = 77;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 22;
			Item.useStyle = 5;
			Item.useTime = 22;
			Item.knockBack = 6.75f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 44;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
			Item.shoot = Mod.Find<ModProjectile>("ExsanguinationLanceProjectile").Type;
			Item.shootSpeed = 8f;
		}
		
		public override bool CanUseItem(Player player)
	    {
	        for (int i = 0; i < 1000; ++i)
	        {
	            if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
	            {
	                return false;
	            }
	        }
	        return true;
	    }
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "CruptixBar", 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
