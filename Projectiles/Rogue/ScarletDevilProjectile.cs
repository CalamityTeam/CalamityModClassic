using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityModClassicPreTrailer.Projectiles.Rogue
{
    public class ScarletDevilProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spear the Gungnir");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 95;
			Projectile.height = 95;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 420;
			Projectile.extraUpdates = 1;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}

        public override void AI()
        {
			Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.55f) / 255f, ((100 - Projectile.alpha) * 0.25f) / 255f, ((100 - Projectile.alpha) * 0.01f) / 255f);
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
			Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 130, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 0, new Color(255, 255, 255), 0.85f);
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 5f)
			{
				int numProj = 2;
				float rotation = MathHelper.ToRadians(15);
				if (Projectile.owner == Main.myPlayer)
				{
					for (int i = 0; i < numProj; i++)
					{
						Vector2 perturbedSpeed = new Vector2(-Projectile.velocity.X / 3, -Projectile.velocity.Y / 3).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProj - 1)));
						for(int j = 0; j < 2; j++)
						{
							Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("ScarletDevilBullet").Type, (int)((double)Projectile.damage * 0.03), 0f, Projectile.owner, 0f, 0f);
							perturbedSpeed *= 1.05f;
						}
					}
				}
				Projectile.ai[0] = 0f;
			}
        }
		
		public override Color? GetAlpha(Color lightColor)
        {
            return new Color(250, 250, 250);
        }

        public override void OnKill(int timeLeft)
        {
			SoundEngine.PlaySound(SoundID.Item122, Projectile.position);
        }
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Projectile.position = Projectile.Center;
            Projectile.width = (Projectile.height = 150);
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ScarletBlast").Type, (int)((double)Projectile.damage * 0.0075), 0f, Projectile.owner, 0f, 0f);
			CalamityPlayerPreTrailer modPlayer = Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>();
        	if (target.type == NPCID.TargetDummy || modPlayer.rogueStealth <= 0f)
			{
				return;
			}
        	Main.player[Main.myPlayer].statLife += 12;
			Main.player[Main.myPlayer].HealEffect(12);
        }
		
		public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                Texture2D trail = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Projectiles/Rogue/ScarletDevilProjectile").Value;
                lightColor = new Color(100, 100, 100);
                Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                Main.spriteBatch.Draw(trail, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}