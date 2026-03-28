using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class FlakKraken : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Kraken");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 0.002f;
            Projectile.timeLeft = 36000;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 4;
        }

        public override void AI()
        {
            if (Projectile.type != Mod.Find<ModProjectile>("FlakKraken").Type || 
                !Main.projectile[(int)Projectile.ai[1]].active || 
                Main.projectile[(int)Projectile.ai[1]].type != Mod.Find<ModProjectile>("FlakKrakenGun").Type)
            {
                Projectile.Kill();
                return;
            }
            Projectile.rotation += 0.2f;
            if (Projectile.localAI[0] < 1f)
            {
                Projectile.localAI[0] += 0.002f;
                Projectile.scale += 0.002f;
                Projectile.width = (int)(50f * Projectile.scale);
                Projectile.height = (int)(50f * Projectile.scale);
            }
            else
            {
                Projectile.width = 50;
                Projectile.height = 50;
            }
            Player player = Main.player[Projectile.owner];
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
            }
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = (float)player.direction;
                num79 = 0f;
            }
            vector2 += new Vector2(num78, num79);
            /*if (projectile.localAI[0] >= 1f)
            {
                vector2.X -= 25f;
                vector2.Y -= 25f;
            }*/
            float speed = 30f;
            float speedScale = 3f;
            Vector2 vectorPos = Projectile.Center;
            if (Vector2.Distance(vector2, vectorPos) < 90f)
            {
                speed = 10f;
                speedScale = 1f;
            }
            if (Vector2.Distance(vector2, vectorPos) < 30f)
            {
                speed = 3f;
                speedScale = 0.3f;
            }
            if (Vector2.Distance(vector2, vectorPos) < 10f)
            {
                speed = 1f;
                speedScale = 0.1f;
            }
            float num678 = vector2.X - vectorPos.X;
            float num679 = vector2.Y - vectorPos.Y;
            float num680 = (float)Math.Sqrt((double)(num678 * num678 + num679 * num679));
            num680 = speed / num680;
            num678 *= num680;
            num679 *= num680;
            if (Projectile.velocity.X < num678)
            {
                Projectile.velocity.X = Projectile.velocity.X + speedScale;
                if (Projectile.velocity.X < 0f && num678 > 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X + speedScale;
                }
            }
            else if (Projectile.velocity.X > num678)
            {
                Projectile.velocity.X = Projectile.velocity.X - speedScale;
                if (Projectile.velocity.X > 0f && num678 < 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X - speedScale;
                }
            }
            if (Projectile.velocity.Y < num679)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + speedScale;
                if (Projectile.velocity.Y < 0f && num679 > 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + speedScale;
                }
            }
            else if (Projectile.velocity.Y > num679)
            {
                Projectile.velocity.Y = Projectile.velocity.Y - speedScale;
                if (Projectile.velocity.Y > 0f && num679 < 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - speedScale;
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            target.damage = (int)((double)target.damage * (double)Projectile.localAI[0]);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 200, 50, Projectile.alpha);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}