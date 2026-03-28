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
    public class AzathothProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Azathoth");
		}
    	
        public override void SetDefaults()
        {
        	Projectile.CloneDefaults(ProjectileID.Kraken);
            Projectile.width = 16;
            Projectile.scale = 1.2f;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            AIType = 554;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 1;
        }
        
        public override void AI()
        {
            if (Main.rand.Next(6) == 0)
        	{
            	if (Projectile.owner == Main.myPlayer)
            	{
            		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.35f, Projectile.velocity.Y * 0.35f, Mod.Find<ModProjectile>("CosmicOrb").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            	}
        	}
        }

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
			Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}