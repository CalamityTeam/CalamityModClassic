using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class PlagueStingerGoliathV2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Exploding Plague Stinger");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 0;
            AIType = 55;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 1200;
        }
        
        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
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
            vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Projectiles/Boss/PlagueStingerGoliathV2Glow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Projectiles/Boss/PlagueStingerGoliathV2Glow").Value.Height / Main.projFrames[Projectile.type])) * 1f / 2f;
            vector += vector11 * 1f + new Vector2(0f, 0f + 4f + Projectile.gfxOffY);
            Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - Projectile.alpha, 127 - Projectile.alpha, 127 - Projectile.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.Red);
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Projectiles/Boss/PlagueStingerGoliathV2Glow").Value, vector,
                null, color, Projectile.rotation, vector11, 1f, spriteEffects, 0f);
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        	if (Projectile.owner == Main.myPlayer)
        	{
        		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("PlagueExplosion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
        }
    }
}