using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class SandTortoise : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sand Tortoise");
			Main.npcFrameCount[NPC.type] = 8;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
				new FlavorTextBestiaryInfoElement("Like their jungle and tundra cousins, they prove quite the serious threat with their high speed lunges.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 2f;
			NPC.damage = 110;
			NPC.aiStyle = 39;
			NPC.width = 46; 
			NPC.height = 32;
			NPC.defense = 34;
			NPC.scale = 1.5f;
			NPC.lifeMax = 580;
			NPC.knockBackResist = 0.2f;
			AnimationType = 153;
			NPC.value = Item.buyPrice(0, 0, 15, 0);
			NPC.HitSound = SoundID.NPCHit24;
			NPC.DeathSound = SoundID.NPCDeath27;
			NPC.noGravity = false;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("SandTortoiseBanner").Type;
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
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !Main.hardMode || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
			{
				return 0f;
			}
			return SpawnCondition.DesertCave.Chance * 0.05f;
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(ItemID.TurtleShell, 10));
		}
	}
}