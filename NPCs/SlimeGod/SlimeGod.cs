using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point0.NPCs.SlimeGod
{
	public class SlimeGod : ModNPC
    {
        public override void SetStaticDefaults()
        {
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Scale = 0.5f,
				PortraitScale = 0.5f,
				PortraitPositionYOverride = 20
            };
            value.Position.Y += 15;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }
        public override void SetDefaults()
		{
			//NPC.name = "Corrupt Slime God");
			//DisplayName.SetDefault("The Slime God");
			NPC.aiStyle = 15;
			NPC.damage = 60;
			NPC.width = 324; //324
			NPC.height = 216; //216
			NPC.defense = 10;
			NPC.lifeMax = 8000;
			NPC.knockBackResist = 0f;
			NPC.boss = true;
			AnimationType = 50;
			Main.npcFrameCount[NPC.type] = 6;
			NPC.value = Item.buyPrice(0, 2, 0, 0);
			NPC.alpha = 60;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound= SoundID.NPCHit1;
			NPC.DeathSound= SoundID.NPCDeath1;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			Music = MusicID.Boss1;
		}

        /*public override void AI()
		{
			if (Main.netMode != 1)
			{
				npc.localAI[0] += (float)Main.rand.Next(4);
				if (npc.localAI[0] >= (float)Main.rand.Next(1400, 26000))
				{
					npc.localAI[0] = 0f;
					npc.TargetClosest(true);
					if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
					{
						float num941 = 8f;
						Vector2 vector104 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)(npc.height / 2));
						float num942 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
						float num943 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
						float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
						num944 = num941 / num944;
						num942 *= num944;
						num943 *= num944;
						num942 += (float)Main.rand.Next(-20, 21) * 0.05f;
						num943 += (float)Main.rand.Next(-20, 21) * 0.05f;
						int num945 = 22;
						if (Main.expertMode)
						{
							num945 = 18;
						}
						int num946 = ;
						vector104.X += num942 * 5f;
						vector104.Y += num943 * 5f;
						int num947 = Projectile.NewProjectile(vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num947].timeLeft = 300;
						npc.netUpdate = true;
					}
				}	
			}
		}*/
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
				new FlavorTextBestiaryInfoElement("The previous ruler of Slimedom, the Slime God has since formed a cult of infectious slimes after it had been overthrown by its heir.")
            });
        }

        public override bool PreKill()
		{
			return false;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < hit.Damage / NPC.lifeMax * 100.0; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= (NPC.lifeMax * 0.75f) && NPC.life >= (NPC.lifeMax * 0.65f))
			{
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_OnHit(NPC), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("SlimeSpawnCorrupt").Type, Main.rand.Next(1, 2));
			}
			if (NPC.life <= (NPC.lifeMax * 0.5f) && NPC.life >= (NPC.lifeMax * 0.4f))
			{
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_OnHit(NPC), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("SlimeSpawnCorrupt").Type, Main.rand.Next(1, 2));
			}
			if (NPC.life <= (NPC.lifeMax * 0.25f) && NPC.life >= (NPC.lifeMax * 0.15f))
			{
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_OnHit(NPC), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("SlimeSpawnCorrupt").Type, Main.rand.Next(2, 3));
			}
			if (Main.netMode != 1 && NPC.life <= 0)
			{
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_OnHit(NPC), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("SlimeGodRun").Type);
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode || Main.rand.Next(1) == 0)
			{
				target.AddBuff(BuffID.ManaSickness, 600, true);
			}
		}
	}
}