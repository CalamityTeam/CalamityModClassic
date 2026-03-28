using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class LunicEye : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lunic Eye");
			// Tooltip.SetDefault("Fires lunic beams that reduce enemy protection\nProjectile damage is multiplied by all of your damage bonuses");
		}

		public override void SetDefaults()
		{
			Item.width = 80;
			Item.damage = 7;
			Item.rare = 5;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = 5;
			Item.knockBack = 4.5f;
			Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/LaserCannon");
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.height = 50;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.shoot = Mod.Find<ModProjectile>("LunicBeam").Type;
			Item.shootSpeed = 13f;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 0);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float damageMult = player.GetDamage(DamageClass.Melee).Additive + player.GetDamage(DamageClass.Ranged).Additive + player.GetDamage(DamageClass.Magic).Additive + 
                CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage + player.GetDamage(DamageClass.Summon).Additive;
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage * damageMult), knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Stardust", 20);
			recipe.AddIngredient(null, "AerialiteBar", 15);
			recipe.AddIngredient(ItemID.SunplateBlock, 15);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
