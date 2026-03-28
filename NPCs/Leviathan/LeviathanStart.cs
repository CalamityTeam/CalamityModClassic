using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.Armor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.Leviathan
{
	public class LeviathanStart : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("???");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
				new FlavorTextBestiaryInfoElement("An alluring being... perhaps hiding something?")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 0;
			NPC.width = 70; //324
			NPC.height = 70; //216
			NPC.defense = 0;
			NPC.lifeMax = 3000;
			NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.chaseable = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.rarity = 2;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/SirenLure");
		}
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.1f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void AI()
		{
			NPC.TargetClosest(true);
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.55f, 0.25f, 0f);
			for (int num569 = 0; num569 < 200; num569++)
			{
				if (Main.npc[num569].active && Main.npc[num569].boss)
				{
					NPC.active = false;
				}
			}
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || 
                NPC.AnyNPCs(NPCID.DukeFishron) || 
                NPC.AnyNPCs(Mod.Find<ModNPC>("LeviathanStart").Type) || 
                NPC.AnyNPCs(Mod.Find<ModNPC>("Siren").Type) || 
                NPC.AnyNPCs(Mod.Find<ModNPC>("Leviathan").Type) || 
                spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSulphur)
			{
				return 0f;
			}
            if (!Main.hardMode)
            {
                return SpawnCondition.OceanMonster.Chance * 0.025f;
            }
            if (!NPC.downedPlantBoss && !CalamityWorldPreTrailer.downedCalamitas)
			{
				return SpawnCondition.OceanMonster.Chance * 0.1f;
			}
			return SpawnCondition.OceanMonster.Chance * 0.4f;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = 3000;
			NPC.damage = 0;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule revActive = new LeadingConditionRule(new RevCondition());
			revActive.OnSuccess(ItemDropRule.OneFromOptions(4, new int[]
			{
				ModContent.ItemType<SirensHeart>(),
				ModContent.ItemType<SirensHeartAlt>(),
			}));
			npcLoot.Add(revActive);
		}

        public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life > 0)
			{
				for (int k = 0; k < 5; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
			else
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
				if (Main.netMode != 1)
				{
					NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.position.Y + NPC.height, Mod.Find<ModNPC>("Siren").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				}
			}
		}
	}
}