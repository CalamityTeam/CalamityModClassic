using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class AngryChicken : ModProjectile
    {
    	public int flareTimer = 600;
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Son of Yharon");
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 4f;
            Projectile.timeLeft = 18000;
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
        }

        public override void AI()
        {
        	if (Projectile.localAI[0] == 0f)
        	{
        		int num501 = 100;
				for (int num502 = 0; num502 < num501; num502++) 
				{
					int num503 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 16f), Projectile.width, Projectile.height - 16, 244, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num503].velocity *= 2f;
					Main.dust[num503].scale *= 1.15f;
				}
				Projectile.localAI[0] += 1f;
        	}
        	if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
			{
				Projectile.spriteDirection = -Projectile.direction;
        	}
        	flareTimer--;
        	if (flareTimer == 0)
        	{
				float spread = 90f * 0.0174f;
				double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y)- spread/2;
				double deltaAngle = spread/8f;
				double offsetAngle;
				int i;
				for (i = 0; i < 2; i++ )
				{
				   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("GigaFlareFriendly").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("GigaFlareFriendly").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				}
				flareTimer = 600;
        	}
        	float num633 = 0f;
			float num634 = 0f;
			float num635 = 0f;
			float num636 = 0f;
			if (Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type)
			{
				num633 = 700f;
				num634 = 800f;
				num635 = 1200f;
				num636 = 150f;
			}
			float num = (float)Main.rand.Next(90, 111) * 0.01f;
        	num *= Main.essScale;
			Lighting.AddLight(Projectile.Center, 1.2f * num, 0.8f * num, 0f * num);
			bool flag64 = Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.aChicken = false;
				}
				if (modPlayer.aChicken)
				{
					Projectile.timeLeft = 2;
				}
			}
			float num637 = 0.05f;
			for (int num638 = 0; num638 < 1000; num638++)
			{
				bool flag23 = (Main.projectile[num638].type == Mod.Find<ModProjectile>("AngryChicken").Type);
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
			if (Projectile.ai[0] == 2f && Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type)
			{
				Projectile.ai[1] += 1f;
				Projectile.extraUpdates = 1;
				Projectile.frameCounter++;
				if (Projectile.frameCounter > 1)
				{
					Projectile.frame++;
					Projectile.frameCounter = 0;
				}
				if (Projectile.frame > 2)
				{
					Projectile.frame = 0;
				}
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
			if (Projectile.ai[0] != 1f && (Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type))
			{
				Projectile.tileCollide = false;
			}
			if (Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16)))
			{
				Projectile.tileCollide = false;
			}
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
			float num647 = num634;
			if (flag25)
			{
				num647 = num635;
			}
			if (Vector2.Distance(player.Center, Projectile.Center) > num647)
			{
				if (Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type)
				{
					Projectile.ai[0] = 1f;
				}
				Projectile.tileCollide = false;
				Projectile.netUpdate = true;
			}
			if (Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type && flag25 && Projectile.ai[0] == 0f)
			{
				Vector2 vector47 = vector46 - Projectile.Center;
				float num648 = vector47.Length();
				vector47.Normalize();
				if (num648 > 200f)
				{
					float scaleFactor2 = 6f;
					if (Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type)
					{
						scaleFactor2 = 8f;
					}
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
					flag26 = (Projectile.ai[0] == 1f && (Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type));
				}
				float num650 = 6f;
				if (flag26)
				{
					num650 = 15f;
				}
				Vector2 center2 = Projectile.Center;
				Vector2 vector48 = player.Center - center2 + new Vector2(0f, -60f);
				float num651 = vector48.Length();
				if (num651 > 200f && num650 < 8f)
				{
					num650 = 8f;
				}
				if (num651 < num636 && flag26 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					if (Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type)
					{
						Projectile.ai[0] = 0f;
					}
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
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
			}
			if (Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type)
			{
				Projectile.frameCounter++;
				if (Projectile.frameCounter > 4)
				{
					Projectile.frame++;
					Projectile.frameCounter = 0;
				}
				if (Projectile.frame > 3)
				{
					Projectile.frame = 0;
				}
			}
			if (Projectile.ai[1] > 0f && (Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type))
			{
				Projectile.ai[1] += (float)Main.rand.Next(1, 4);
			}
			if (Projectile.ai[1] > 40f && Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[0] == 0f && (Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type))
			{
				if (Projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type && Projectile.ai[1] == 0f && flag25 && num633 < 500f)
				{
					Projectile.ai[1] += 1f;
					if (Main.myPlayer == Projectile.owner)
					{
						Projectile.ai[0] = 2f;
						Vector2 value20 = vector46 - Projectile.Center;
						value20.Normalize();
						Projectile.velocity = value20 * 8f;
						Projectile.netUpdate = true;
						return;
					}
				}
			}
        }
    }
}