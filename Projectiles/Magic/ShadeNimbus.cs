using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class ShadeNimbus : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nimbus");
			Main.projFrames[Projectile.type] = 6;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 28;
            Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 8)
			{
				Projectile.frameCounter = 0;
				Projectile.frame++;
				if (Projectile.frame > 5)
				{
					Projectile.frame = 0;
				}
			}
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] >= 7200f)
			{
				Projectile.alpha += 5;
				if (Projectile.alpha > 255)
				{
					Projectile.alpha = 255;
					Projectile.Kill();
				}
			}
			else
			{
				Projectile.ai[0] += 1f;
				if (Projectile.ai[0] > 8f)
				{
					Projectile.ai[0] = 0f;
					if (Projectile.owner == Main.myPlayer)
					{
						int num414 = (int)(Projectile.position.X + 14f + (float)Main.rand.Next(Projectile.width - 28));
						int num415 = (int)(Projectile.position.Y + (float)Projectile.height + 4f);
						Projectile.NewProjectile(Projectile.GetSource_FromThis(null), (float)num414, (float)num415, 0f, 5f, Mod.Find<ModProjectile>("Shaderain").Type, Projectile.damage, 0f, Projectile.owner, 0f, 0f);
					}
				}
			}
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] >= 10f)
			{
				Projectile.localAI[0] = 0f;
				int num416 = 0;
				int num417 = 0;
				float num418 = 0f;
				int num419 = Projectile.type;
				for (int num420 = 0; num420 < 1000; num420++)
				{
					if (Main.projectile[num420].active && Main.projectile[num420].owner == Projectile.owner && Main.projectile[num420].type == num419 && Main.projectile[num420].ai[1] < 3600f)
					{
						num416++;
						if (Main.projectile[num420].ai[1] > num418)
						{
							num417 = num420;
							num418 = Main.projectile[num420].ai[1];
						}
					}
				}
				if (num416 > 2)
				{
					Main.projectile[num417].netUpdate = true;
					Main.projectile[num417].ai[1] = 36000f;
					return;
				}
			}
        }
    }
}