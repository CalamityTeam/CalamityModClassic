using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class UrchinSpikeFugu : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Urchin Spike");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 90;
            Projectile.noEnchantments = true;
        }
        
        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.ai[0] == 0f)
            {
                float num695 = 100f;
                int num696 = -1;
                int num3;
                for (int num697 = 0; num697 < 200; num697 = num3 + 1)
                {
                    NPC nPC5 = Main.npc[num697];
                    if (nPC5.CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC5.position, nPC5.width, nPC5.height))
                    {
                        float num698 = (nPC5.Center - Projectile.Center).Length();
                        if (num698 < num695)
                        {
                            num696 = num697;
                            num695 = num698;
                        }
                    }
                    num3 = num697;
                }
                Projectile.ai[0] = (float)(num696 + 1);
                if (Projectile.ai[0] == 0f)
                {
                    Projectile.ai[0] = -15f;
                }
                if (Projectile.ai[0] > 0f)
                {
                    float scaleFactor5 = (float)Main.rand.Next(35, 75) / 30f;
                    Projectile.velocity = (Projectile.velocity * 20f + Vector2.Normalize(Main.npc[(int)Projectile.ai[0] - 1].Center - Projectile.Center + new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101))) * scaleFactor5) / 21f;
                    Projectile.netUpdate = true;
                }
            }
            else if (Projectile.ai[0] > 0f)
            {
                Vector2 value16 = Vector2.Normalize(Main.npc[(int)Projectile.ai[0] - 1].Center - Projectile.Center);
                Projectile.velocity = (Projectile.velocity * 40f + value16 * 12f) / 41f;
            }
            else
            {
                float[] var_2_1E1A4_cp_0 = Projectile.ai;
                int var_2_1E1A4_cp_1 = 0;
                float num73 = var_2_1E1A4_cp_0[var_2_1E1A4_cp_1];
                var_2_1E1A4_cp_0[var_2_1E1A4_cp_1] = num73 + 1f;
                Projectile.alpha -= 25;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
                Projectile.velocity.Y = Projectile.velocity.Y + 0.015f;
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.Venom, 180);
        }
    }
}