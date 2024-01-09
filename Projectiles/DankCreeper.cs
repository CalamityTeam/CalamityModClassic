using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class DankCreeper : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Dank Creeper");
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 74;
            Projectile.height = 74;
            Projectile.scale = 0.75f;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minionSlots = 1;
            Projectile.aiStyle = 54;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 317;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 7;
        }

        public override void AI()
        {
        	if (Projectile.localAI[0] == 0f)
        	{
        		int num226 = 36;
				for (int num227 = 0; num227 < num226; num227++)
				{
					Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
					vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Projectile.Center;
					Vector2 vector7 = vector6 - Projectile.Center;
					int num228 = Dust.NewDust(vector6 + vector7, 0, 0, DustID.Demonite, vector7.X * 1.5f, vector7.Y * 1.5f, 100, default(Color), 1.4f);
					Main.dust[num228].noGravity = true;
					Main.dust[num228].noLight = true;
					Main.dust[num228].velocity = vector7;
				}
				Projectile.localAI[0] += 1f;
        	}
        	bool flag64 = Projectile.type == Mod.Find<ModProjectile>("DankCreeper").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			player.AddBuff(Mod.Find<ModBuff>("DankCreeper").Type, 3600);
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.dCreeper = false;
				}
				if (modPlayer.dCreeper)
				{
					Projectile.timeLeft = 2;
				}
			}
		}
        
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}