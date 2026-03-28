using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class ApoctosisArray : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Apoctosis Array");
			// Tooltip.SetDefault("Fires ion blasts that speed up and then explode\nThe higher your mana the more damage they will do");
		}

		public override void SetDefaults()
		{
			Item.width = 98;
			Item.damage = 58;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 12;
			Item.useAnimation = 7;
			Item.useTime = 7;
			Item.useStyle = 5;
			Item.knockBack = 6.75f;
			Item.UseSound = SoundID.Item91;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.height = 34;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("IonBlast").Type;
			Item.shootSpeed = 8f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-25, 0);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        float manaAmount = ((float)player.statMana * 0.01f);
	        float damageMult = manaAmount;
	        int projectile = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage * damageMult), knockback, player.whoAmI, 0.0f, 0.0f);
	        Main.projectile[projectile].scale = (manaAmount * 0.375f);
	    	return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "IonBlaster");
			recipe.AddIngredient(ItemID.LunarBar, 10);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	}
}
