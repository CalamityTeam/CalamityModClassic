using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class MoltenBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Molten Blast");
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;
            Main.projFrames[Projectile.type] = 4;
        }

        public override void AI()
        {
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 3)
			{
			   Projectile.frame = 0;
			}
        	if (Projectile.ai[1] == 0f)
        	{
        		for (int num621 = 0; num621 < 20; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
        		Projectile.ai[1] = 1f;
        		SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
        	}
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
        	int num251 = Main.rand.Next(5, 10);
			for (int num252 = 0; num252 < num251; num252++)
			{
				Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				while (value15.X == 0f && value15.Y == 0f)
				{
					value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				}
				value15.Normalize();
				value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.oldPosition.X + (float)(Projectile.width / 2), Projectile.oldPosition.Y + (float)(Projectile.height / 2), value15.X, value15.Y, Mod.Find<ModProjectile>("MoltenBlob").Type, (int)((double)Projectile.damage * 0.8), 0f, Projectile.owner, 0f, 0f);
			}
        	SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            for (int k = 0; k < 25; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 60);
        }
    }
}