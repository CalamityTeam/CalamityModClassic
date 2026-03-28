using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.SlimeGod
{
	public class SlimeSpawnCrimson2 : ModNPC
	{
		public float spikeTimer = 60f;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spiked Crimson Slime Spawn");
			Main.npcFrameCount[NPC.type] = 2;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 1;
			NPC.damage = 30;
			NPC.width = 40; //324
			NPC.height = 30; //216
			NPC.defense = 10;
			NPC.lifeMax = 130;
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 120000;
            }
            NPC.knockBackResist = 0f;
			AnimationType = 81;
			NPC.alpha = 60;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.buffImmune[24] = true;
		}
		
		public override void AI()
		{
			if (spikeTimer > 0f)
			{
				spikeTimer -= 1f;
			}
			if (!NPC.wet)
			{
				Vector2 vector3 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num14 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector3.X;
				float num15 = Main.player[NPC.target].position.Y - vector3.Y;
				float num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
				if (Main.expertMode && num16 < 120f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
				{
					NPC.ai[0] = -40f;
					if (NPC.velocity.Y == 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
					}
					if (Main.netMode != 1 && spikeTimer == 0f)
					{
						for (int n = 0; n < 5; n++)
						{
							Vector2 vector4 = new Vector2((float)(n - 2), -4f);
							vector4.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
							vector4.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
							vector4.Normalize();
							vector4 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector3.X, vector3.Y, vector4.X, vector4.Y, Mod.Find<ModProjectile>("CrimsonSpike").Type, 13, 0f, Main.myPlayer, 0f, 0f);
							spikeTimer = 30f;
						}
					}
				}
				else if (num16 < 360f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
				{
					NPC.ai[0] = -40f;
					if (NPC.velocity.Y == 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
					}
					if (Main.netMode != 1 && spikeTimer == 0f)
					{
						num15 = Main.player[NPC.target].position.Y - vector3.Y - (float)Main.rand.Next(0, 200);
						num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
						num16 = 6.5f / num16;
						num14 *= num16;
						num15 *= num16;
						spikeTimer = 50f;
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector3.X, vector3.Y, num14, num15, Mod.Find<ModProjectile>("CrimsonSpike").Type, 10, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.NormalvsExpert(ItemID.Nazar, 100, 50));
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Cursed, 60, true);
		}
	}
}