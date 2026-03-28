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
	public class HeavenfallenStardisk : CalamityDamageItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Heavenfallen Stardisk");
			/* Tooltip.SetDefault("Throws a stardisk upwards which then launches itself towards your mouse cursor,\n" +
							   "explodes into several astral energy bolts if the thrower is moving vertically when throwing it and during its impact"); */
		}

		public override void SafeSetDefaults()
		{
			Item.width = 38;
			Item.height = 34;
			Item.damage = 150;
			Item.crit += 20;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 23;
			Item.useStyle = 1;
			Item.useTime = 23;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.value = Item.buyPrice(0, 60, 0, 0);
			Item.rare = 7;
			Item.shoot = Mod.Find<ModProjectile>("HeavenfallenStardisk").Type;
			Item.shootSpeed = 10f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
			Projectile.NewProjectile(Entity.GetSource_FromThis(null),position.X, position.Y, 0f, -10f, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			return false;
		}
	}
}
