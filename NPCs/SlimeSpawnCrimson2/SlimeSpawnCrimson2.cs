using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.SlimeSpawnCrimson2
{
	public class SlimeSpawnCrimson2 : ModNPC
	{
		public float spikeTimer = 60f;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Spiked Crimson Slime Spawn");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 1;
			NPC.damage = 50;
			NPC.width = 40; //324
			NPC.height = 30; //216
			NPC.defense = 10;
			NPC.lifeMax = 140;
			NPC.knockBackResist = 0f;
			AnimationType = 81;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.alpha = 60;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.buffImmune[24] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
                new FlavorTextBestiaryInfoElement("Seriously, don't touch it.")

            });
        }

        public override void AI()
		{
			if (spikeTimer > 0f)
			{
				spikeTimer -= 1f;
			}
			if (!NPC.wet && !Main.player[NPC.target].npcTypeNoAggro[NPC.type])
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
					if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer == 0f)
					{
						for (int n = 0; n < 5; n++)
						{
							Vector2 vector4 = new Vector2((float)(n - 2), -4f);
							vector4.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
							vector4.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
							vector4.Normalize();
							vector4 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector3.X, vector3.Y, vector4.X, vector4.Y, Mod.Find<ModProjectile>("CrimsonSpike").Type, 15, 0f, Main.myPlayer, 0f, 0f);
							spikeTimer = 30f;
						}
					}
				}
				else if (num16 < 200f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
				{
					NPC.ai[0] = -40f;
					if (NPC.velocity.Y == 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
					}
					if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer == 0f)
					{
						num15 = Main.player[NPC.target].position.Y - vector3.Y - (float)Main.rand.Next(0, 200);
						num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
						num16 = 4.5f / num16;
						num14 *= num16;
						num15 *= num16;
						spikeTimer = 50f;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), vector3.X, vector3.Y, num14, num15, Mod.Find<ModProjectile>("CrimsonSpike").Type, 11, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemID.Nazar, 100, 50));
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Cursed, 60, true);
			}
		}
	}
}