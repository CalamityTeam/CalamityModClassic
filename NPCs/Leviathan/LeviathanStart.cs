using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.Leviathan
{
	public class LeviathanStart : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Leviathan Lure");
			//Tooltip.SetDefault("???");
			NPC.npcSlots = 10f;
			NPC.damage = 0;
			NPC.width = 50; //324
			NPC.height = 100; //216
			NPC.defense = 0;
			NPC.lifeMax = 3000;
			NPC.knockBackResist = 0f;
			Main.npcFrameCount[NPC.type] = 3;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.noGravity = true;
			NPC.chaseable = false;
			NPC.HitSound = SoundID.NPCHit1;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("What could her intentions be?")

            });
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
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 0.5f, 0.3f);
			bool flag100 = false;
			for (int num569 = 0; num569 < 200; num569++)
			{
				if (Main.npc[num569].active && Main.npc[num569].type == (NPCID.DukeFishron))
				{
					flag100 = true;
				}
			}
			if (flag100)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneBeach && spawnInfo.Player.wet && !NPC.AnyNPCs(NPCID.DukeFishron) && !NPC.AnyNPCs(Mod.Find<ModNPC>("LeviathanStart").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Siren").Type) && !Main.snowMoon && !Main.pumpkinMoon ? 0.35f : 0f;
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = 3000;
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
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("Siren").Type);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("Leviathan").Type);
			}
		}
	}
}