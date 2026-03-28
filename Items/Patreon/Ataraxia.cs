using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Patreon
{
	public class Ataraxia : ModItem
	{
		public static int BaseDamage = 3120;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ataraxia");
			// Tooltip.SetDefault("Equanimity");
		}

		public override void SetDefaults()
		{
			Item.width = 94;
			Item.height = 92;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.damage = BaseDamage;
			Item.knockBack = 2.5f;
			Item.useAnimation = 18;
			Item.useTime = 18;
			Item.autoReuse = true;
			Item.useTurn = true;

			Item.useStyle = 1;
			Item.UseSound = SoundID.Item1;

			Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
			Item.value = Item.buyPrice(2, 50, 0, 0);

			Item.shoot = Mod.Find<ModProjectile>("AtaraxiaMain").Type;
			Item.shootSpeed = 9f;
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
		{
			// thanks ataraxia very cool
			// i'll never forget your 13276749 damage lol
			float damageBoost = (float)BaseDamage * player.GetModPlayer<CalamityPlayerPreTrailer>().ataraxiaDamageBoost;
			float damageAdd = damageBoost + (float)BaseDamage;
			damage.Base = (damageAdd * player.GetDamage(DamageClass.Melee).Base);
		}

		// Fires one large and two small projectiles which stay together in formation.
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Play the Terra Blade sound upon firing
			SoundEngine.PlaySound(SoundID.Item60, position);

			// Center projectile
			int centerID = Mod.Find<ModProjectile>("AtaraxiaMain").Type;
			int centerDamage = damage;
			Vector2 centerVec = new Vector2(velocity.X, velocity.Y);
			int center = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, centerID, centerDamage, knockback, player.whoAmI, 0.0f, 0.0f);

			// Side projectiles (these deal 75% damage)
			int sideID = Mod.Find<ModProjectile>("AtaraxiaSide").Type;
			int sideDamage = (int)(0.75f * centerDamage);
			Vector2 speed = new Vector2(velocity.X, velocity.Y);
			speed.Normalize();
			speed *= 22f;
			Vector2 rrp = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 leftOffset = speed.RotatedBy((double)(MathHelper.PiOver4), default(Vector2));
			Vector2 rightOffset = speed.RotatedBy((double)(-MathHelper.PiOver4), default(Vector2));
			leftOffset -= 1.4f * speed;
			rightOffset -= 1.4f * speed;
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), rrp.X + leftOffset.X, rrp.Y + leftOffset.Y, velocity.X, velocity.Y, sideID, sideDamage, knockback, player.whoAmI, 0.0f, 0.0f);
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), rrp.X + rightOffset.X, rrp.Y + rightOffset.Y, velocity.X, velocity.Y, sideID, sideDamage, knockback, player.whoAmI, 0.0f, 0.0f);
			return false;
		}

		// On-hit, tosses out five homing projectiles. This is not like Holy Collider.
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.ShadowFlame, 480);
			target.AddBuff(BuffID.Ichor, 480);

			// Does not summon extra projectiles versus dummies.
			if (target.type == NPCID.TargetDummy)
				return;

			// Individual true melee homing missiles deal 10% of the weapon's base damage.
			int numSplits = 5;
			int trueMeleeID = Mod.Find<ModProjectile>("AtaraxiaHoming").Type;
			int trueMeleeDamage = (int)(0.1f * Item.damage);
			float angleVariance = MathHelper.TwoPi / (float)numSplits;
			float spinOffsetAngle = MathHelper.Pi / (2f * numSplits);
			Vector2 posVec = new Vector2(8f, 0f).RotatedByRandom(MathHelper.TwoPi);

			for (int i = 0; i < numSplits; ++i)
			{
				posVec = posVec.RotatedBy(angleVariance);
				Vector2 velocity = new Vector2(posVec.X, posVec.Y).RotatedBy(spinOffsetAngle);
				velocity.Normalize();
				velocity *= 8f;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null),target.Center + posVec, velocity, trueMeleeID, trueMeleeDamage, Item.knockBack, player.whoAmI, 0.0f, 0.0f);
			}
		}

		// Spawn some fancy dust while swinging
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dustCount = Main.rand.Next(3, 6);
			Vector2 corner = new Vector2(hitbox.X + hitbox.Width / 4, hitbox.Y + hitbox.Height / 4);
			for (int i = 0; i < dustCount; ++i)
			{
				// Pick a random dust to spawn
				int dustID = -1;
				switch (Main.rand.Next(5))
				{
					case 0:
					case 1: dustID = 70; break;
					case 2: dustID = 71; break;
					default: dustID = 86; break;
				}
				int idx = Dust.NewDust(corner, hitbox.Width / 2, hitbox.Height / 2, dustID);
				Main.dust[idx].noGravity = true;
			}
		}

		public override void AddRecipes()
		{
			Recipe r = CreateRecipe();
			r.AddIngredient(ItemID.BrokenHeroSword);
			r.AddIngredient(null, "CosmiliteBar", 25);
			r.AddIngredient(null, "Phantoplasm", 35);
			r.AddIngredient(null, "NightmareFuel", 90);
			r.AddIngredient(null, "EndothermicEnergy", 90);
			r.AddIngredient(null, "DarksunFragment", 65);
			r.AddIngredient(null, "BarofLife", 15);
			r.AddIngredient(null, "CoreofCalamity", 5);
			r.AddIngredient(null, "HellcasterFragment", 10);
			r.AddTile(null, "DraedonsForge");
			r.Register();
		}
	}
}
