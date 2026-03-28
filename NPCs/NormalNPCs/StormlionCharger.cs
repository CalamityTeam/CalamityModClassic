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
	public class StormlionCharger : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stormlion");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
				new FlavorTextBestiaryInfoElement("This variant of antlion is very unusual with its feeding; As it takes to the surface and reach upwards with their mandibles to act as lightning rods, feeding off the storms' lightning.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 20;
			NPC.aiStyle = 3;
			NPC.width = 33; //324
			NPC.height = 31; //216
			NPC.defense = 9;
			NPC.lifeMax = 80;
			NPC.knockBackResist = 0.2f;
			AnimationType = 508;
			NPC.value = Item.buyPrice(0, 0, 2, 0);
			NPC.HitSound = SoundID.NPCHit31;
			NPC.DeathSound = SoundID.NPCDeath34;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("StormlionBanner").Type;
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
			if (spawnInfo.PlayerSafe || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
			{
				return 0f;
			}
			return SpawnCondition.DesertCave.Chance * 0.2f;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Electrified, 90, true);
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("StormlionMandible").Type, 2));
		}
	}
}