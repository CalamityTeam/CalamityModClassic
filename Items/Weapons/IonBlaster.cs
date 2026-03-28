using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class IonBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ion Blaster");
			// Tooltip.SetDefault("Fires ion blasts that speed up and then explode\nThe higher your mana the more damage they will do");
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.damage = 20;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 8;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.useStyle = 5;
			Item.knockBack = 5.5f;
			Item.UseSound = SoundID.Item91;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.height = 28;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.shoot = Mod.Find<ModProjectile>("IonBlast").Type;
			Item.shootSpeed = 3f;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        float manaAmount = ((float)player.statMana * 0.01f);
	        float damageMult = manaAmount * 0.75f;
	        int projectile = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage * damageMult), knockback, player.whoAmI, 0.0f, 0.0f);
	    	Main.projectile[projectile].scale = manaAmount;
	    	return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofFright, 10);
			recipe.AddIngredient(ItemID.AdamantiteBar, 7);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofFright, 10);
			recipe.AddIngredient(ItemID.TitaniumBar, 7);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
