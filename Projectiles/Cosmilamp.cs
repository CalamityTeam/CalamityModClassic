using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class Cosmilamp : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cosmilamp");
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 30;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minionSlots = 1;
            Projectile.aiStyle = 54;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 317;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 3;
        }

        public override void AI()
        {
        	if (Projectile.localAI[0] == 0f)
        	{
        		int num226 = 36;
				for (int num227 = 0; num227 < num226; num227++)
				{
					Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
					vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Projectile.Center;
					Vector2 vector7 = vector6 - Projectile.Center;
					int num228 = Dust.NewDust(vector6 + vector7, 0, 0, DustID.TreasureSparkle, vector7.X * 1.5f, vector7.Y * 1.5f, 100, default(Color), 1.4f);
					Main.dust[num228].noGravity = true;
					Main.dust[num228].noLight = true;
					Main.dust[num228].velocity = vector7;
				}
				Projectile.localAI[0] += 1f;
        	}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.75f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
        	float num395 = (float)Main.mouseTextColor / 200f - 0.35f;
			num395 *= 0.2f;
			Projectile.scale = num395 + 0.95f;
        	int num1262 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TreasureSparkle, 0f, 0f, 0, default(Color), 1f);
			Main.dust[num1262].velocity *= 0.1f;
			Main.dust[num1262].scale = 1.3f;
			Main.dust[num1262].noGravity = true;
        	bool flag64 = Projectile.type == Mod.Find<ModProjectile>("Cosmilamp").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			player.AddBuff(Mod.Find<ModBuff>("Cosmilamp").Type, 3600);
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.cLamp = false;
				}
				if (modPlayer.cLamp)
				{
					Projectile.timeLeft = 2;
				}
			}
		}
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.immune[Projectile.owner] = 4;
        }
    }
}