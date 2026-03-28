using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Amidias
{
    public class BigCoral : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Big Coral");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;
        }
        
        public override void AI()
        {
			Projectile.velocity.X *= 0.999f;
			Projectile.velocity.Y = Projectile.velocity.Y + 0.025f;
        }
		
		public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 18;
			Projectile.height = 34;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num621 = 0; num621 < 10; num621++)
			{
				int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 51, Projectile.oldVelocity.X / 4, Projectile.oldVelocity.Y / 4, 0, new Color(234, 183, 100), 1.5f);
                Main.dust[num195].noGravity = true;
				Main.dust[num195].velocity *= 3f;
			}
        }
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (target.rarity != 2 && !target.boss)
			{
				target.AddBuff(Mod.Find<ModBuff>("SilvaStun").Type, 15);
			}
        }
    }
}