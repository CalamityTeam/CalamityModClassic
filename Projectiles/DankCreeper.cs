using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class DankCreeper : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Dank Creeper");
            Projectile.width = 74;
            Projectile.height = 74;
            Projectile.scale = 0.75f;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minionSlots = 1;
            Projectile.aiStyle = 54;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 317;
            Projectile.tileCollide = false;
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
					int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 14, vector7.X * 1.5f, vector7.Y * 1.5f, 100, default(Color), 1.4f);
					Main.dust[num228].noGravity = true;
					Main.dust[num228].noLight = true;
					Main.dust[num228].velocity = vector7;
				}
				Projectile.localAI[0] += 1f;
        	}
        	bool flag64 = Projectile.type == Mod.Find<ModProjectile>("DankCreeper").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
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
    }
}