using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class MoltenAmputator : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Molten Amputator");
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 3;
            Projectile.timeLeft = 300;
            AIType = 52;
        }
        
        public override void AI()
        {
			int num250 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, (float)(Projectile.direction * 2), 0f, 150, default(Color), 1.3f);
			Main.dust[num250].noGravity = true;
			Main.dust[num250].velocity *= 0f;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
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
				int blob = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.oldPosition.X + (float)(Projectile.width / 2), Projectile.oldPosition.Y + (float)(Projectile.height / 2), value15.X, value15.Y, Mod.Find<ModProjectile>("MoltenBlob").Type, (int)((double)Projectile.damage * 0.5f), 0f, Projectile.owner, 0f, 0f);
				Main.projectile[blob].hostile = false;
				Main.projectile[blob].friendly = true;
				Main.projectile[blob].penetrate = 1;
			}
        	SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            for (int k = 0; k < 25; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}