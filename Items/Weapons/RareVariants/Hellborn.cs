using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.RareVariants
{
	public class Hellborn : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hellborn");
			/* Tooltip.SetDefault("Converts all bullets to explosive rounds\n" +
				"Enemies that touch the gun while it's being fired take massive damage"); */
		}

	    public override void SetDefaults()
	    {
			Item.damage = 21;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 50;
			Item.height = 24;
			Item.useTime = 9;
			Item.useAnimation = 9;
			Item.useStyle = 5;
			Item.knockBack = 1f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = 10;
			Item.shootSpeed = 14f;
			Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{    
		    for (int index = 0; index < 3; ++index)
		    {
		        float num7 = velocity.X;
		        float num8 = velocity.Y;
		        float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
		        float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
		        Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, ProjectileID.ExplosiveBullet, damage, knockback, player.whoAmI, 0f, 0f);
		    }
		    return false;
		}

		public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.SourceDamage.Base *= 15;
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.OnFire, 400);
		}
	}
}