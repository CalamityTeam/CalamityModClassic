using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class BallOFugu : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ball O Fugu");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Vector2 vector62 = Main.player[Projectile.owner].Center - Projectile.Center;
            Projectile.rotation = vector62.ToRotation() - 1.57f;
            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
                return;
            }
            Main.player[Projectile.owner].itemAnimation = 10;
            Main.player[Projectile.owner].itemTime = 10;
            float arg_1DC8F_0 = vector62.X;
            if (vector62.X < 0f)
            {
                Main.player[Projectile.owner].ChangeDir(1);
                Projectile.direction = 1;
            }
            else
            {
                Main.player[Projectile.owner].ChangeDir(-1);
                Projectile.direction = -1;
            }
            Main.player[Projectile.owner].itemRotation = (vector62 * -1f * (float)Projectile.direction).ToRotation();
            Projectile.spriteDirection = ((vector62.X > 0f) ? -1 : 1);
            if (Projectile.ai[0] == 0f && vector62.Length() > 400f)
            {
                Projectile.ai[0] = 1f;
            }
            if (Projectile.ai[0] == 1f || Projectile.ai[0] == 2f)
            {
                float num693 = vector62.Length();
                if (num693 > 1500f)
                {
                    Projectile.Kill();
                    return;
                }
                if (num693 > 600f)
                {
                    Projectile.ai[0] = 2f;
                }
                Projectile.tileCollide = false;
                float num694 = 20f;
                if (Projectile.ai[0] == 2f)
                {
                    num694 = 40f;
                }
                Projectile.velocity = Vector2.Normalize(vector62) * num694;
                if (vector62.Length() < num694)
                {
                    Projectile.Kill();
                    return;
                }
            }
            float[] var_2_1DE21_cp_0 = Projectile.ai;
            int var_2_1DE21_cp_1 = 1;
            float num73 = var_2_1DE21_cp_0[var_2_1DE21_cp_1];
            var_2_1DE21_cp_0[var_2_1DE21_cp_1] = num73 + 1f;
            if (Projectile.ai[1] > 5f)
            {
                Projectile.alpha = 0;
            }
            if ((int)Projectile.ai[1] % 4 == 0 && Projectile.owner == Main.myPlayer)
            {
                Vector2 vector63 = vector62 * -1f;
                vector63.Normalize();
                vector63 *= (float)Main.rand.Next(45, 65) * 0.1f;
                vector63 = vector63.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866, default(Vector2));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector63.X, vector63.Y, Mod.Find<ModProjectile>("UrchinSpikeFugu").Type, (int)((double)Projectile.damage * 0.6), Projectile.knockBack * 0.2f, Projectile.owner, -10f, 0f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            Projectile.ai[0] = 1f;
            Projectile.netUpdate = true;
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Microsoft.Xna.Framework.Color transparent = Microsoft.Xna.Framework.Color.Transparent;
            Texture2D texture2D2 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Chains/BallOFuguChain").Value;
            Vector2 vector17 = Projectile.Center;
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = null;
            Vector2 origin = new Vector2((float)texture2D2.Width * 0.5f, (float)texture2D2.Height * 0.5f);
            float num91 = (float)texture2D2.Height;
            Vector2 vector18 = mountedCenter - vector17;
            float rotation15 = (float)Math.Atan2((double)vector18.Y, (double)vector18.X) - 1.57f;
            bool flag13 = true;
            if (float.IsNaN(vector17.X) && float.IsNaN(vector17.Y))
            {
                flag13 = false;
            }
            if (float.IsNaN(vector18.X) && float.IsNaN(vector18.Y))
            {
                flag13 = false;
            }
            while (flag13)
            {
                if (vector18.Length() < num91 + 1f)
                {
                    flag13 = false;
                }
                else
                {
                    Vector2 value2 = vector18;
                    value2.Normalize();
                    vector17 += value2 * num91;
                    vector18 = mountedCenter - vector17;
                    Microsoft.Xna.Framework.Color color17 = Lighting.GetColor((int)vector17.X / 16, (int)(vector17.Y / 16f));
                    Main.spriteBatch.Draw(texture2D2, vector17 - Main.screenPosition, sourceRectangle, color17, rotation15, origin, 1f, SpriteEffects.None, 0f);
                }
            }
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.Venom, 240);
            Projectile.ai[0] = 1f;
            Projectile.netUpdate = true;
        }
    }
}