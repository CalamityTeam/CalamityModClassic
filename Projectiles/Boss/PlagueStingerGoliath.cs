using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class PlagueStingerGoliath : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plague Homing Stinger");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            AIType = 270;
        }
        
        public override void AI()
        {
            Projectile.velocity.X *= 1.01f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 180);
        }

        public override void PostDraw(Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 center = new Vector2(Projectile.Center.X, Projectile.Center.Y);
            Vector2 vector11 = new Vector2((float)(TextureAssets.Projectile[Projectile.type].Value.Width / 2), (float)(TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type] / 2));
            Vector2 vector = center - Main.screenPosition;
            vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Projectiles/Boss/PlagueStingerGoliathGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Projectiles/Boss/PlagueStingerGoliathGlow").Value.Height / Main.projFrames[Projectile.type])) * 1f / 2f;
            vector += vector11 * 1f + new Vector2(0f, 0f + 4f + Projectile.gfxOffY);
            Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - Projectile.alpha, 127 - Projectile.alpha, 127 - Projectile.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.Red);
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Projectiles/Boss/PlagueStingerGoliathGlow").Value, vector,
                null, color, Projectile.rotation, vector11, 1f, spriteEffects, 0f);
        }
    }
}