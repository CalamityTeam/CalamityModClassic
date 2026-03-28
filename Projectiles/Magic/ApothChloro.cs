using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
	public class ApothChloro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rays of Annihilation");
		}

		public override void SetDefaults()
		{
			Projectile.width = 10;
            Projectile.height = 10;
			Projectile.alpha = 70;
			Projectile.timeLeft = 120;
			Projectile.penetrate = 1;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
		}

		public override void AI()
        {
			Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int i = 0; i < 5; i++)
            {
                Dust dust4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 242, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1f)];
                dust4.velocity = Vector2.Zero;
                dust4.position -= Projectile.velocity / 5f * (float)i;
                dust4.noGravity = true;
                dust4.scale = 0.8f;
                dust4.noLight = true;
            }
			float num1 = (float)(Projectile.position.X - Projectile.velocity.X / 10.0 * 9.0);
            float num2 = (float)(Projectile.position.Y - Projectile.velocity.Y / 10.0 * 9.0);
            float num3 = (float)Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y));
            float num4 = Projectile.localAI[0];
            if ((double)num4 == 0.0)
            {
                Projectile.localAI[0] = num3;
                num4 = num3;
            }
            if (Projectile.alpha > 0)
                Projectile.alpha = Projectile.alpha - 25;
            if (Projectile.alpha < 0)
                Projectile.alpha = 0;
            float num5 = (float)Projectile.position.X;
            float num6 = (float)Projectile.position.Y;
            float num7 = 300f;
            bool flag2 = false;
            int num8 = 0;
            float num9 = 0f;
            if ((double)Projectile.ai[1] == 0.0)
            {
                for (int index = 0; index < 200; ++index)
                {
                    if (Main.npc[index].CanBeChasedBy((object)this, false) && ((double)Projectile.ai[1] == 0.0 || (double)Projectile.ai[1] == (double)(index + 1)))
                    {
                        num1 = (float)Main.npc[index].position.X + (float)(Main.npc[index].width / 2);
                        num2 = (float)Main.npc[index].position.Y + (float)(Main.npc[index].height / 2);
                        num9 = Math.Abs((float)Projectile.position.X + (float)(Projectile.width / 2) - num1) + Math.Abs((float)Projectile.position.Y + (float)(Projectile.height / 2) - num2);
                        if ((double)num9 < (double)num7 && Collision.CanHit(new Vector2((float)Projectile.position.X + (float)(Projectile.width / 2), (float)Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.npc[index].position, Main.npc[index].width, Main.npc[index].height))
                        {
                            num7 = num9;
                            num5 = num1;
                            num6 = num2;
                            flag2 = true;
                            num8 = index;
                        }
                    }
                }
                if (flag2)
                    Projectile.ai[1] = (float)(num8 + 1);
                flag2 = false;
            }
            if ((double)Projectile.ai[1] > 0.0)
            {
                int index = (int)((double)Projectile.ai[1] - 1.0);
                if (Main.npc[index].active && Main.npc[index].CanBeChasedBy((object)this, true) && !Main.npc[index].dontTakeDamage)
                {
                    if ((double)Math.Abs((float)Projectile.position.X + (float)(Projectile.width / 2) - ((float)Main.npc[index].position.X + (float)(Main.npc[index].width / 2))) + (double)Math.Abs((float)Projectile.position.Y + (float)(Projectile.height / 2) - ((float)Main.npc[index].position.Y + (float)(Main.npc[index].height / 2))) < 3000.0)
                    {
                        flag2 = true;
                        num5 = (float)Main.npc[index].position.X + (float)(Main.npc[index].width / 2);
                        num6 = (float)Main.npc[index].position.Y + (float)(Main.npc[index].height / 2);
                    }
                }
                else
                    Projectile.ai[1] = 0.0f;
            }
            if (!Projectile.friendly)
                flag2 = false;
            if (flag2)
            {
                double num15 = (double) num4;
                Vector2 vector2 = new Vector2((float) (Projectile.position.X + (double) Projectile.width * 0.5), (float) (Projectile.position.Y + (double) Projectile.height * 0.5));
                num2 = num5 - (float) vector2.X;
                num9 = num6 - (float) vector2.Y;
                double num10 = Math.Sqrt((double) num2 * (double) num2 + (double) num9 * (double) num9);
                float num11 = (float) (num15 / num10);
                float num12 = num2 * num11;
                float num13 = num9 * num11;
                int num14 = 8;
                Projectile.velocity.X = (float) ((Projectile.velocity.X * (double) (num14 - 1) + (double) num12) / (double) num14);
                Projectile.velocity.Y = (float) ((Projectile.velocity.Y * (double) (num14 - 1) + (double) num13) / (double) num14);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 600, true);
			target.AddBuff(Mod.Find<ModBuff>("DemonFlames").Type, 600, true);
        }
	}
}
