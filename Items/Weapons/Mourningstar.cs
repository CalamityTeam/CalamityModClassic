using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class Mourningstar : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mourningstar");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.damage = 650;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.autoReuse = true;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 13;
			Item.useTime = 13;
			Item.useStyle = 5;
			Item.knockBack = 2.5f;
			Item.UseSound = SoundID.Item116;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.shootSpeed = 24f;
			Item.shoot = 611;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "CosmiliteBar", 5);
			recipe.AddIngredient(null, "Phantoplasm", 5);
			recipe.AddIngredient(ItemID.SolarEruption);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f; //0.5
	    	float ai3X = (Main.rand.NextFloat() - 0.25f) * 0.7853982f; //0.5
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Mourningstar").Type, damage, knockback, player.whoAmI, 0.0f, ai3);
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Mourningstar").Type, damage, knockback, player.whoAmI, 0.0f, ai3X);
	    	return false;
		}
	}
}
