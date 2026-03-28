using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassicPreTrailer.NPCs.Leviathan
{
	public class AquaticAberration : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aquatic Aberration");
			Main.npcFrameCount[NPC.type] = 9;
		} 
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
				new FlavorTextBestiaryInfoElement("This creature devours whole schools of fish entirely by itself.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 60;
			NPC.width = 70; //324
			NPC.height = 40; //216
			NPC.defense = 18;
			NPC.lifeMax = CalamityWorldPreTrailer.death ? 2200 : 1100;
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 100000;
            }
            NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			AIType = -1;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("AquaticAberrationBanner").Type;
		}
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void AI()
		{
			bool revenge = CalamityWorldPreTrailer.revenge;
			NPC.TargetClosest(false);
			NPC.rotation = NPC.velocity.ToRotation();
			if (Math.Sign(NPC.velocity.X) != 0) 
			{
				NPC.spriteDirection = -Math.Sign(NPC.velocity.X);
			}
			if (NPC.rotation < -1.57079637f) 
			{
				NPC.rotation += 3.14159274f;
			}
			if (NPC.rotation > 1.57079637f) 
			{
				NPC.rotation -= 3.14159274f;
			}
			NPC.spriteDirection = Math.Sign(NPC.velocity.X);
			float num998 = 8f;
			float scaleFactor3 = 300f;
			float num999 = 800f;
			float num1000 = 60f;
			float num1001 = 5f;
			float scaleFactor4 = 0.8f;
			int num1002 = 0;
			float scaleFactor5 = 10f;
			float num1003 = 30f;
			float num1004 = 150f;
			float num1005 = 60f;
			float num1006 = 0.333333343f;
			float num1007 = 8f;
			num1006 *= num1005;
			int num1009 = (NPC.ai[0] == 2f) ? 2 : 1;
			int num1010 = (NPC.ai[0] == 2f) ? 30 : 20;
			for (int num1011 = 0; num1011 < 2; num1011++) 
			{
				if (Main.rand.Next(3) < num1009) 
				{
					int num1012 = Dust.NewDust(NPC.Center - new Vector2((float)num1010), num1010 * 2, num1010 * 2, 33, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[num1012].noGravity = true;
					Main.dust[num1012].velocity *= 0.2f;
					Main.dust[num1012].fadeIn = 1f;
				}
			}
			if (NPC.ai[0] == 0f) 
			{
				float scaleFactor6 = num998;
				Vector2 center4 = NPC.Center;
				Vector2 center5 = Main.player[NPC.target].Center;
				Vector2 vector126 = center5 - center4;
				Vector2 vector127 = vector126 - Vector2.UnitY * scaleFactor3;
				float num1013 = vector126.Length();
				vector126 = Vector2.Normalize(vector126) * scaleFactor6;
				vector127 = Vector2.Normalize(vector127) * scaleFactor6;
				bool flag64 = Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1);
				if (NPC.ai[3] >= 120f) 
				{
					flag64 = true;
				}
				float num1014 = 8f;
				flag64 = (flag64 && vector126.ToRotation() > 3.14159274f / num1014 && vector126.ToRotation() < 3.14159274f - 3.14159274f / num1014);
				if (num1013 > num999 || !flag64) 
				{
					NPC.velocity.X = (NPC.velocity.X * (num1000 - 1f) + vector127.X) / num1000;
					NPC.velocity.Y = (NPC.velocity.Y * (num1000 - 1f) + vector127.Y) / num1000;
					if (!flag64) 
					{
						NPC.ai[3] += 1f;
						if (NPC.ai[3] == 120f) 
						{
							NPC.netUpdate = true;
						}
					} 
					else
					{
						NPC.ai[3] = 0f;
					}
				} 
				else 
				{
					NPC.ai[0] = 1f;
					NPC.ai[2] = vector126.X;
					NPC.ai[3] = vector126.Y;
					NPC.netUpdate = true;
				}
			} 
			else if (NPC.ai[0] == 1f) 
			{
				NPC.velocity *= scaleFactor4;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= num1001) 
				{
					NPC.ai[0] = 2f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
					Vector2 velocity = new Vector2(NPC.ai[2], NPC.ai[3]) + new Vector2((float)Main.rand.Next(-num1002, num1002 + 1), (float)Main.rand.Next(-num1002, num1002 + 1)) * 0.04f;
					velocity.Normalize();
					velocity *= scaleFactor5;
					NPC.velocity = velocity;
				}
			} 
			else if (NPC.ai[0] == 2f) 
			{
				float num1016 = num1003;
				NPC.ai[1] += 1f;
				bool flag65 = Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) > num1004 && NPC.Center.Y > Main.player[NPC.target].Center.Y;
				if ((NPC.ai[1] >= num1016 && flag65) || NPC.velocity.Length() < num1007) 
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.velocity /= 2f;
					NPC.netUpdate = true;
					NPC.ai[1] = 45f;
					NPC.ai[0] = 4f;
				} 
				else 
				{
					Vector2 center6 = NPC.Center;
					Vector2 center7 = Main.player[NPC.target].Center;
					Vector2 vec2 = center7 - center6;
					vec2.Normalize();
					if (vec2.HasNaNs()) 
					{
						vec2 = new Vector2((float)NPC.direction, 0f);
					}
					NPC.velocity = (NPC.velocity * (num1005 - 1f) + vec2 * (NPC.velocity.Length() + num1006)) / num1005;
				}
			} 
			else if (NPC.ai[0] == 4f) 
			{
				NPC.ai[1] -= 3f;
				if (NPC.ai[1] <= 0f) 
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
				}
				NPC.velocity *= 0.95f;
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Wet, 120, true);
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 120);
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}