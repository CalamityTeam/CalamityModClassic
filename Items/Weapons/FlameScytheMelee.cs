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
	public class FlameScytheMelee : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flame Scythe");
		}
		
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.damage = 145;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
			Item.useAnimation = 19;
			Item.useStyle = 1;
			Item.useTime = 19;
			Item.knockBack = 8.5f;
			Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.height = 48;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
			Item.shoot = Mod.Find<ModProjectile>("FlameScytheProjectileMelee").Type;
			Item.shootSpeed = 16f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "CruptixBar", 9);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
