using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Bumblebirb
{
	public class RougeSlash : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rouge Slash");
			// Tooltip.SetDefault("Fires a wave of 3 rouge air slashes");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 90;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 30;
			Item.width = 28;
			Item.height = 32;
			Item.useTime = 19;
			Item.useAnimation = 19;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 7.5f;
			Item.UseSound = SoundID.Item91;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("RougeSlashLarge").Type;
			Item.shootSpeed = 24f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("RougeSlashLarge").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X * 0.8f, velocity.Y * 0.8f, Mod.Find<ModProjectile>("RougeSlashMedium").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X * 0.6f, velocity.Y * 0.6f, Mod.Find<ModProjectile>("RougeSlashSmall").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}