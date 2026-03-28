using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee.Yoyos
{
    public class QuagmireProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Quagmire");
		}
    	
        public override void SetDefaults()
        {
        	Projectile.CloneDefaults(ProjectileID.HelFire);
            Projectile.width = 16;
            Projectile.scale = 1.25f;
            Projectile.height = 16;
            Projectile.penetrate = 8;
            Projectile.DamageType = DamageClass.Melee;
            AIType = 553;
        }
        
        public override void AI()
        {
        	if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 44, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        	if (Projectile.owner == Main.myPlayer)
        	{
	        	if (Main.rand.Next(10) == 0)
	        	{
	            	int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.35f, Projectile.velocity.Y * 0.35f, 569, (int)((double)Projectile.damage * 0.65), Projectile.knockBack, Projectile.owner, 0f, 0f);
					Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
				}
	        	if (Main.rand.Next(30) == 0)
	        	{
	            	int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 570, (int)((double)Projectile.damage * 0.75), Projectile.knockBack, Projectile.owner, 0f, 0f);
					Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
				}
	        	if (Main.rand.Next(50) == 0)
	        	{
	            	int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.15f, Projectile.velocity.Y * 0.15f, 571, (int)((double)Projectile.damage * 0.85), Projectile.knockBack, Projectile.owner, 0f, 0f);
					Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
				}
        	}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.Venom, 200);
        }

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
			Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}