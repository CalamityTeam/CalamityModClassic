using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons.SunkenSea
{
	public class ClamorRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Clamor Rifle");
			// Tooltip.SetDefault("Shoots homing energy bolts");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 32;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 64;
			Item.height = 30;
			Item.useTime = 13;
			Item.useAnimation = 13;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 2.5f;
			Item.value = Item.buyPrice(0, 36, 0, 0);
			Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/PlasmaBolt");
			Item.autoReuse = true;
			Item.rare = 5;
			Item.shoot = Mod.Find<ModProjectile>("ClamorRifleProj").Type;
			Item.shootSpeed = 15f;
			Item.useAmmo = 97;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("ClamorRifleProj").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
		}
	}
}