using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
    public class AquaticStar : ModProjectile
    {
    	public float dust = 0f;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aquatic Star");
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 66;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 388;
        }
        
        public override void AI()
        {
        	if (dust == 0f)
        	{
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base;
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
				int num226 = 36;
				for (int num227 = 0; num227 < num226; num227++)
				{
					Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
					vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Projectile.Center;
					Vector2 vector7 = vector6 - Projectile.Center;
					int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 33, vector7.X * 1.1f, vector7.Y * 1.1f, 100, default(Color), 1.4f);
					Main.dust[num228].noGravity = true;
					Main.dust[num228].noLight = true;
					Main.dust[num228].velocity = vector7;
				}
				dust += 1f;
        	}
			if (Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base != Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue)
			{
				int damage2 = (int)(((float)Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue /
					Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue) *
					Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base);
				Projectile.damage = damage2;
			}
			Projectile.rotation += Projectile.velocity.X * 0.04f;
        	bool flag64 = Projectile.type == Mod.Find<ModProjectile>("AquaticStar").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			player.AddBuff(Mod.Find<ModBuff>("AquaticStar").Type, 3600);
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.aStar = false;
				}
				if (modPlayer.aStar)
				{
					Projectile.timeLeft = 2;
				}
			}
		}
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 5;
        }
    }
}