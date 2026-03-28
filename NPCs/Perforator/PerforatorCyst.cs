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
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.Perforator
{
	public class PerforatorCyst : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Perforator Cyst");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
				new FlavorTextBestiaryInfoElement("The culmination of the crimson's horrific flesh, avoid it at all costs.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.1f;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 0;
			NPC.width = 30; //324
			NPC.height = 30; //216
			NPC.defense = 0;
			NPC.lifeMax = 1000;
			NPC.knockBackResist = 0f;
			NPC.chaseable = false;
			NPC.HitSound = SoundID.NPCHit13;
			NPC.rarity = 2;
		}
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || NPC.AnyNPCs(Mod.Find<ModNPC>("PerforatorCyst").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("PerforatorHive").Type))
			{
				return 0f;
			}
			return SpawnCondition.Crimson.Chance * (Main.hardMode ? 0.05f : 0.5f);
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = 2000;
			NPC.damage = 0;
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
				if (Main.netMode != 1 && NPC.CountNPCS(Mod.Find<ModNPC>("PerforatorHive").Type) < 1)
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					NPC.NewNPC(NPC.GetSource_FromThis(null), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("PerforatorHive").Type);
				}
			}
		}
	}
}