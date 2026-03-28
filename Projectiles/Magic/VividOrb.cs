using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class VividOrb : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Orb");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 6;
        }

        public override void AI()
        {
        	Projectile.velocity.X *= 1.01f;
        	Projectile.velocity.Y *= 1.01f;
            if (Projectile.ai[0] == 0f)
			{
				Projectile.ai[0] = Projectile.velocity.X;
				Projectile.ai[1] = Projectile.velocity.Y;
			}
			if (Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y)) > 2.0)
			{
				Projectile.velocity *= 0.98f;
			}
			int[] array = new int[20];
			int num428 = 0;
			float num429 = 300f;
			bool flag14 = false;
			for (int num430 = 0; num430 < 200; num430++)
			{
				if (Main.npc[num430].CanBeChasedBy(Projectile, false))
				{
					float num431 = Main.npc[num430].position.X + (float)(Main.npc[num430].width / 2);
					float num432 = Main.npc[num430].position.Y + (float)(Main.npc[num430].height / 2);
					float num433 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num431) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num432);
					if (num433 < num429 && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num430].Center, 1, 1))
					{
						if (num428 < 20)
						{
							array[num428] = num430;
							num428++;
						}
						flag14 = true;
					}
				}
			}
			if (flag14)
			{
				int num434 = Main.rand.Next(num428);
				num434 = array[num434];
				float num435 = Main.npc[num434].position.X + (float)(Main.npc[num434].width / 2);
				float num436 = Main.npc[num434].position.Y + (float)(Main.npc[num434].height / 2);
				Projectile.localAI[0] += 1f;
				if (Projectile.localAI[0] > 24f)
				{
					Projectile.localAI[0] = 0f;
					float num437 = 6f;
					Vector2 value10 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					value10 += Projectile.velocity * 4f;
					float num438 = num435 - value10.X;
					float num439 = num436 - value10.Y;
					float num440 = (float)Math.Sqrt((double)(num438 * num438 + num439 * num439));
					num440 = num437 / num440;
					num438 *= num440;
					num439 *= num440;
					if (Projectile.owner == Main.myPlayer)
					{
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), value10.X, value10.Y, num438, num439, Mod.Find<ModProjectile>("VividBolt").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
					}
					return;
				}
			}
        }
    }
}