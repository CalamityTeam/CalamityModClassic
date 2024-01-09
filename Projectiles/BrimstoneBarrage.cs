using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class BrimstoneBarrage : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Brimstone");
			Main.projFrames[Projectile.type] = 5;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 600;
            CooldownSlot = 1;
        }

        public override void AI()
        {
        	bool revenge = CalamityWorld.revenge;
        	Projectile.velocity.X *= 1.005f;
        	Projectile.velocity.Y *= 1.005f;
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 5)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 4)
			{
			   Projectile.frame = 0;
			}
			if (Projectile.ai[0] == 0f)
			{
				Projectile.ai[0] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.75f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 240);
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
        	Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
			int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
			int y6 = num214 * Projectile.frame;
			Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), Projectile.scale, SpriteEffects.None, 0f);
			return false;
        }
    }
}