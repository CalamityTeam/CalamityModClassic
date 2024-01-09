using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class Mushmash : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mush");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 10;
        }
        
        public override void OnKill(int timeLeft)
        {
			if (Projectile.owner == Main.myPlayer)
			{
				int num814 = 12;
				int num815 = (int)(Projectile.position.X / 16f - (float)num814);
				int num816 = (int)(Projectile.position.X / 16f + (float)num814);
				int num817 = (int)(Projectile.position.Y / 16f - (float)num814);
				int num818 = (int)(Projectile.position.Y / 16f + (float)num814);
				if (num815 < 0)
				{
					num815 = 0;
				}
				if (num816 > Main.maxTilesX)
				{
					num816 = Main.maxTilesX;
				}
				if (num817 < 0)
				{
					num817 = 0;
				}
				if (num818 > Main.maxTilesY)
				{
					num818 = Main.maxTilesY;
				}
				for (int num824 = num815; num824 <= num816; num824++)
				{
					for (int num825 = num817; num825 <= num818; num825++)
					{
						float num826 = Math.Abs((float)num824 - Projectile.position.X / 16f);
						float num827 = Math.Abs((float)num825 - Projectile.position.Y / 16f);
						double num828 = Math.Sqrt((double)(num826 * num826 + num827 * num827));
						if (num828 < (double)num814)
						{
							if (Main.tile[num824, num825] != null && Main.tile[num824, num825].HasTile && 
							    Main.tile[num824, num825].TileType != 59 && 
							    Main.tile[num824, num825].TileType != 1 && 
							    Main.tile[num824, num825].TileType != 0 &&
							    Main.tile[num824, num825].TileType != 70 &&
							    Main.tile[num824, num825].TileType != 71 &&
							    Main.tile[num824, num825].TileType != 72 &&
							    Main.tile[num824, num825].TileType != 190 &&
							    Main.tile[num824, num825].TileType != Mod.Find<ModTile>("AerialiteOre").Type &&
							    Main.tile[num824, num825].TileType != Mod.Find<ModTile>("PerennialOre").Type &&
							    Main.tile[num824, num825].TileType != Mod.Find<ModTile>("ChaoticOre").Type &&
							    Main.tile[num824, num825].TileType != Mod.Find<ModTile>("CryonicOre").Type &&
							    Main.tile[num824, num825].TileType != Mod.Find<ModTile>("UelibloomOre").Type)
							{
								WorldGen.KillTile(num824, num825, false, false, false);
								if (!Main.tile[num824, num825].HasTile && Main.netMode != NetmodeID.SinglePlayer)
								{
									NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, (float)num824, (float)num825, 0f, 0, 0, 0);
								}
							}
						}
					}
				}
				if (Main.netMode != NetmodeID.SinglePlayer)
				{
					NetMessage.SendData(MessageID.KillProjectile, -1, -1, null, Projectile.identity, (float)Projectile.owner, 0f, 0f, 0, 0, 0);
				}
			}
			Projectile.active = false;	
        }
    }
}