using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
    public class RedDevil : ModProjectile
    {
    	public int dust = 3;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Red Devil");
			Main.projFrames[Projectile.type] = 5;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
        }

        public override void AI()
        {
        	bool flag64 = Projectile.type == Mod.Find<ModProjectile>("RedDevil").Type;
        	Player player = Main.player[Projectile.owner];
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
        	if (!modPlayer.redDevil)
        	{
        		Projectile.active = false;
        		return;
        	}
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.rDevil = false;
				}
				if (modPlayer.rDevil)
				{
					Projectile.timeLeft = 2;
				}
			}
        	dust--;
        	if (dust >= 0)
        	{
        		int num501 = 50;
				for (int num502 = 0; num502 < num501; num502++) 
				{
					int num503 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 16f), Projectile.width, Projectile.height - 16, 235, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num503].velocity *= 2f;
					Main.dust[num503].scale *= 1.15f;
				}
        	}
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 8)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 4)
			{
			   Projectile.frame = 0;
			}
			float num = (float)Main.rand.Next(90, 111) * 0.01f;
        	num *= Main.essScale;
			Lighting.AddLight(Projectile.Center, 1f * num, 0f * num, 0.15f * num);
			Projectile.rotation = Projectile.velocity.X * 0.04f;
			if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
			{
				Projectile.spriteDirection = -Projectile.direction;
        	}
			float num633 = 700f;
			float num634 = 2000f;
			float num635 = 3000f;
			float num636 = 150f;
			float num637 = 0.05f;
			for (int num638 = 0; num638 < 1000; num638++)
			{
				bool flag23 = (Main.projectile[num638].type == Mod.Find<ModProjectile>("RedDevil").Type);
				if (num638 != Projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == Projectile.owner && flag23 && Math.Abs(Projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num638].position.Y) < (float)Projectile.width)
				{
					if (Projectile.position.X < Main.projectile[num638].position.X)
					{
						Projectile.velocity.X = Projectile.velocity.X - num637;
					}
					else
					{
						Projectile.velocity.X = Projectile.velocity.X + num637;
					}
					if (Projectile.position.Y < Main.projectile[num638].position.Y)
					{
						Projectile.velocity.Y = Projectile.velocity.Y - num637;
					}
					else
					{
						Projectile.velocity.Y = Projectile.velocity.Y + num637;
					}
				}
			}
			bool flag24 = false;
			if (Projectile.ai[0] == 2f)
			{
				Projectile.ai[1] += 1f;
				Projectile.extraUpdates = 1;
				if (Projectile.ai[1] > 40f)
				{
					Projectile.ai[1] = 1f;
					Projectile.ai[0] = 0f;
					Projectile.extraUpdates = 0;
					Projectile.numUpdates = 0;
					Projectile.netUpdate = true;
				}
				else
				{
					flag24 = true;
				}
			}
			if (flag24)
			{
				return;
			}
			Vector2 vector46 = Projectile.position;
			bool flag25 = false;
			if (Projectile.ai[0] != 1f)
			{
				Projectile.tileCollide = false;
			}
			if (Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16)))
			{
				Projectile.tileCollide = false;
			}
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (npc.CanBeChasedBy(Projectile, false))
				{
					float num646 = Vector2.Distance(npc.Center, Projectile.Center);
					if ((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !flag25)
					{
						num633 = num646;
						vector46 = npc.Center;
						flag25 = true;
					}
				}
			}
			else
			{
				for (int num645 = 0; num645 < 200; num645++)
				{
					NPC nPC2 = Main.npc[num645];
					if (nPC2.CanBeChasedBy(Projectile, false))
					{
						float num646 = Vector2.Distance(nPC2.Center, Projectile.Center);
						if (((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
						{
							num633 = num646;
							vector46 = nPC2.Center;
							flag25 = true;
						}
					}
				}
			}
			float num647 = num634;
			if (flag25)
			{
				num647 = num635;
			}
			if (Vector2.Distance(player.Center, Projectile.Center) > num647)
			{
				Projectile.ai[0] = 1f;
				Projectile.tileCollide = false;
				Projectile.netUpdate = true;
			}
			if (flag25 && Projectile.ai[0] == 0f)
			{
				Vector2 vector47 = vector46 - Projectile.Center;
				float num648 = vector47.Length();
				vector47.Normalize();
				if (num648 > 200f)
				{
					float scaleFactor2 = 16f; //8
					vector47 *= scaleFactor2;
					Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
				}
				else
				{
					float num649 = 4f;
					vector47 *= -num649;
					Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
				}
			}
			else
			{
				bool flag26 = false;
				if (!flag26)
				{
					flag26 = (Projectile.ai[0] == 1f);
				}
				float num650 = 5f; //6
				if (flag26)
				{
					num650 = 12f; //15
				}
				Vector2 center2 = Projectile.Center;
				Vector2 vector48 = player.Center - center2 + new Vector2(0f, -30f); //-60
				float num651 = vector48.Length();
				if (num651 > 200f && num650 < 6.5f) //200 and 8
				{
					num650 = 6.5f; //8
				}
				if (num651 < num636 && flag26 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
					Projectile.netUpdate = true;
				}
				if (num651 > 2000f)
				{
					Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
					Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2);
					Projectile.netUpdate = true;
				}
				if (num651 > 70f)
				{
					vector48.Normalize();
					vector48 *= num650;
					Projectile.velocity = (Projectile.velocity * 40f + vector48) / 41f;
				}
				else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
				{
					Projectile.velocity.X = -0.2f;
					Projectile.velocity.Y = -0.1f;
				}
			}
			if (Projectile.ai[1] > 0f)
			{
				Projectile.ai[1] += (float)Main.rand.Next(1, 4);
			}
			if (Projectile.ai[1] > 80f)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[0] == 0f)
			{
				float scaleFactor3 = 24f;
				int num658 = 114;
				if (flag25 && Projectile.ai[1] == 0f)
				{
					Projectile.ai[1] += 1f;
					if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, vector46, 0, 0))
					{
						Vector2 value19 = vector46 - Projectile.Center;
						value19.Normalize();
						value19 *= scaleFactor3;
						int num659 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value19.X, value19.Y, num658, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num659].timeLeft = 300;
						Projectile.netUpdate = true;
					}
				}
			}
        }

		public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
		{
			return false;
		}
	}
}