using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Projectiles
{
	public class VeriumBullet : ModProjectile
	{
		public override void SetDefaults()
		{
			//DisplayName.SetDefault("Verium Bullet");
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.light = 0.5f;
			Projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			AIType = ProjectileID.Bullet;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
			{
				Projectile.Kill();
			}
			else
			{
				if (Projectile.velocity.X != oldVelocity.X)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			}
			return false;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		
		public override bool PreAI()
        {
            for (int index1 = 0; index1 < 5; ++index1)
            {
                float x = Projectile.position.X - Projectile.velocity.X / 10f * (float)index1;
                float y = Projectile.position.Y - Projectile.velocity.Y / 10f * (float)index1;
                int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 182, 0.0f, 0.0f, 0, new Color(), 1f);
                Main.dust[index2].color = new Color(255, 0, 200);
                Main.dust[index2].alpha = Projectile.alpha;
                Main.dust[index2].position.X = x;
                Main.dust[index2].position.Y = y;
                Main.dust[index2].velocity *= 0.0f;
                Main.dust[index2].noGravity = true;
            }
            float direction = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.X);
            float ai = Projectile.localAI[0];
            if (ai == 0.0F)
            {
                Projectile.localAI[0] = direction;
                ai = direction;
            }

            float X = Projectile.position.X;
            float Y = Projectile.position.Y;
            float num5 = 300F; // This value determines how much the bullet is allowed to turn from its original rotation to the                                                   // rotation it needs to chase its target.
            bool flag2 = false;
            int targetID = 0;
            if (Projectile.ai[1] == 0.0F)
            {
                for (int i = 0; i < 200; ++i)
                {
                    // Checks if the indexed npc can be chased and if the projectile doesn't have a target (projectile.ai[1] == 0.0, since                   // that is the cache for the targeted npc index.
                    if ( Main.npc[i].CanBeChasedBy((object)this, false) && (Projectile.ai[1] == 0.0 || Projectile.ai[1] == (i + 1)) )
                    {
                        float targetPosX = Main.npc[i].position.X + (Main.npc[i].width / 2);
                        float targetPosY = Main.npc[i].position.Y + (Main.npc[i].height / 2);
                        float newDir = Math.Abs(Projectile.position.X + (Projectile.width / 2) - targetPosX) +
                        Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - targetPosY);

                        if (newDir < num5 && Collision.CanHit(new Vector2(Projectile.position.X + (Projectile.width / 2),
                        Projectile.position.Y + (Projectile.height / 2)), 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                        {
                            num5 = newDir;
                            X = targetPosX;
                            Y = targetPosY;
                            flag2 = true;
                            targetID = i;
                        }
                    }
                }
                if (flag2) // This is basically true when the projectile has found a valid target.
                Projectile.ai[1] = (float)(targetID + 1); // So we set the target.
                flag2 = false; // And reset flag2.
            }
            if (Projectile.ai[1] > 0.0) // If the projectile has a target, since the projectile.ai[1] holds the index of the targeted NPC.
            {
                int index = (int)(Projectile.ai[1] - 1.0);
                if (Main.npc[index].active && Main.npc[index].CanBeChasedBy((object)this, true) && !Main.npc[index].dontTakeDamage)
                {
                    // This is a range check, so it checks if the distance between this projectile and its target is less than 
                    // 1000 pixels.
                    if ((Math.Abs(Projectile.position.X + (Projectile.width / 2) - (Main.npc[index].position.X + (Main.npc[index].width / 2)))                  + Math.Abs(Projectile.position.Y + (Projectile.height / 2) - (Main.npc[index].position.Y 
                    + (float)(Main.npc[index].height / 2)))) < 1000.0)
                    {                       
                        // If the distance is OK, set flag2 to true again, since we use that boolean once more and also set the
                        // target X and Y positions correctly so that we know which way we want the projectile to move.                      
                        flag2 = true;
                        X = Main.npc[index].position.X + (float)(Main.npc[index].width / 2);
                        Y = Main.npc[index].position.Y + (float)(Main.npc[index].height / 2);
                    }
                }
                else
                Projectile.ai[1] = 0.0f; // Else if the distance is not OK, leave flag2 as it is (false) and reset the target.
            }
            if (!Projectile.friendly) // Yeah, if the projectile is not friendly, it means that it can only target players...
            flag2 = false; // So flag 2 should be false, since this bullet only loops through the NPC list, not the player one.

            if (flag2) // So, if this flag is true (with the distance check and all).
            {
                // What follows are calculations to get the direction towards the targeted NPC and turning the velocity towards that                 // NPC. Not going into too much detail here, since this is just basic math :p
                float newAI = ai;
                Vector2 vector2 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num8 = X - vector2.X;
                float num9 = Y - vector2.Y;
                float num10 = (float)Math.Sqrt((double)num8 * (double)num8 + (double)num9 * (double)num9);
                float num11 = newAI / num10;
                float num12 = num8 * num11;
                float num13 = num9 * num11;
                int num14 = 8;
                Projectile.velocity.X = (Projectile.velocity.X * (float)(num14 - 1) + num12) / (float)num14;
                Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num14 - 1) + num13) / (float)num14;
            }
            return false; // Return false to disable vanilla AI!
        }
	}
}