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
	public class Mycoroot : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mycoroot");
			// Tooltip.SetDefault("Fires a stream of short-range fungal roots");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 32;
			Item.damage = 10;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useTime = 5;
			Item.useAnimation = 5;
			Item.useStyle = 1;
			Item.knockBack = 1.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 32;
			Item.rare = 2;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.shoot = Mod.Find<ModProjectile>("Mycoroot").Type;
			Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
		    Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}
	}
}
