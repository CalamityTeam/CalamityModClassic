using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.HeatSpirit
{
	public class HeatSpirit : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Heat Spirit");
			//Tooltip.SetDefault("Heat Spirit");
			NPC.npcSlots = 0.5f;
			NPC.aiStyle = 86;
			NPC.damage = 66;
			NPC.width = 40; //324
			NPC.height = 24; //216
			NPC.defense = 20;
			NPC.lifeMax = 110;
			NPC.knockBackResist = 0f;
			Main.npcFrameCount[NPC.type] = 4;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			NPC.HitSound = SoundID.NPCHit52;
			NPC.DeathSound = SoundID.NPCDeath55;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("Some say that this spirit's mask is made of refined Hellstone.")

            });
        }

        public override void FindFrame(int frameHeight)
        {
			if (NPC.velocity.X < 0f)
			{
				NPC.direction = -1;
			}
			else
			{
				NPC.direction = 1;
			}
			if (NPC.direction == 1)
			{
				NPC.spriteDirection = 1;
			}
			if (NPC.direction == -1)
			{
				NPC.spriteDirection = -1;
			}
			NPC.rotation = (float)Math.Atan2((double)(NPC.velocity.Y * (float)NPC.direction), (double)(NPC.velocity.X * (float)NPC.direction));
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.5f, 0f, 0.05f);
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = spawnInfo.SpawnTileX;
            int y = spawnInfo.SpawnTileY;
            int tile = (int)Main.tile[x, y].TileType;
            bool oUnderworld = (y >= (Main.maxTilesY * 0.8f));
            return oUnderworld && Main.hardMode ? 0.05f : 0f;
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.OnFire, 300, true);
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100, true);
			}
			else
			{
				target.AddBuff(BuffID.OnFire, 160, true);
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 50, true);
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			npcLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofChaos>(), 2));
        }
	}
}