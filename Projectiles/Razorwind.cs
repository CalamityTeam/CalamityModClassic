using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class Razorwind : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Razorwind");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 2;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10 -
				(NPC.downedFishron ? 2 : 0) -
        		(NPC.downedMoonlord ? 3 : 0) -
        		(CalamityWorld.downedDoG ? 2 : 0) -
        		(CalamityWorld.downedYharon ? 1 : 0);
        }
        
        public override void AI()
        {
        	bool kingSlime = NPC.downedSlimeKing;
			bool eyeOfCthulhu = NPC.downedBoss1;
			bool eaterOrBrain = NPC.downedBoss2;
			bool queenBee = NPC.downedQueenBee;
			bool skeletron = NPC.downedBoss3;
			bool wallOfFlesh = Main.hardMode;
			bool anyMechBoss = NPC.downedMechBossAny;
			bool plantera = NPC.downedPlantBoss;
			bool golem = NPC.downedGolemBoss;
			bool dukeFish = NPC.downedFishron;
			bool cultist = NPC.downedAncientCultist;
			bool moonLord = NPC.downedMoonlord;
			bool providence = CalamityWorld.downedProvidence;
			bool devourerOfGods = CalamityWorld.downedDoG;
			bool yharon = CalamityWorld.downedYharon;
        	float speedMult = 1f + //1
				(kingSlime ? 0.1f : 0f) + //1.1
				(eyeOfCthulhu ? 0.1f : 0f) + //1.2
				(eaterOrBrain ? 0.1f : 0f) + //1.3
				(queenBee ? 0.15f : 0f) + //1.45
				(skeletron ? 0.15f : 0f) + //1.6
				(wallOfFlesh ? 0.15f : 0f) + //1.75
				(anyMechBoss ? 0.1f : 0f) + //1.85
				(plantera ? 0.15f : 0f) + //2
				(golem ? 0.1f : 0f) + //2.1
				(dukeFish ? 0.15f : 0f) + //2.25
				(cultist ? 0.15f : 0f) + //2.4
				(moonLord ? 0.35f : 0f) + //2.75
				(providence ? 0.15f : 0f) + //2.9
				(devourerOfGods ? 0.15f : 0f) + //3.05
				(yharon ? 0.2f : 0f); //3.25
        	Projectile.rotation += Projectile.velocity.X * 0.1f;
			Projectile.alpha -= 5;
			if (Projectile.alpha < 50) 
			{
				Projectile.alpha = 50;
			}
        	Projectile.localAI[1] += 1f;
			if (Projectile.localAI[1] > 4f)
			{
				for (int num468 = 0; num468 < 3; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Flare_Blue, 0f, 0f, 100, new Color(53, Main.DiscoG, 255), 2f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
			}
			float num472 = Projectile.Center.X;
			float num473 = Projectile.Center.Y;
			float num474 = 150f * speedMult;
			bool flag17 = false;
			for (int num475 = 0; num475 < 200; num475++)
			{
				if (Main.npc[num475].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num475].Center, 1, 1))
				{
					float num476 = Main.npc[num475].position.X + (float)(Main.npc[num475].width / 2);
					float num477 = Main.npc[num475].position.Y + (float)(Main.npc[num475].height / 2);
					float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num476) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num477);
					if (num478 < num474)
					{
						num474 = num478;
						num472 = num476;
						num473 = num477;
						flag17 = true;
					}
				}
			}
			if (flag17)
			{
				float num483 = 5f * speedMult;
				Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num484 = num472 - vector35.X;
				float num485 = num473 - vector35.Y;
				float num486 = (float)Math.Sqrt((double)(num484 * num484 + num485 * num485));
				num486 = num483 / num486;
				num484 *= num486;
				num485 *= num486;
				Projectile.velocity.X = (Projectile.velocity.X * 20f + num484) / 21f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num485) / 21f;
				return;
			}
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 10; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Flare_Blue, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 100, new Color(53, Main.DiscoG, 255));
            }
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(53, Main.DiscoG, 255, Projectile.alpha);
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}