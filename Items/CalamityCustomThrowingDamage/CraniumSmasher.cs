using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class CraniumSmasher : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cranium Smasher");
			// Tooltip.SetDefault("Throws disks that roll on the ground, occasionally launches an explosive disk");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 50;
			Item.height = 50;
			Item.damage = 140;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useStyle = 1;
			Item.useTime = 15;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
			Item.shoot = Mod.Find<ModProjectile>("CraniumSmasher").Type;
			Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.Next(0, 5) == 0)
			{
				damage = (int)(damage * 1.25f);
				type = Mod.Find<ModProjectile>("CraniumSmasherExplosive").Type;
			}
			else
			{
				type = Mod.Find<ModProjectile>("CraniumSmasher").Type;
			}
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			return false;
		}	
	}
}
