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
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class AeroSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aero Slime");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
				new FlavorTextBestiaryInfoElement("After feasting on Aerialite Ore, this slime has sprouted wings!")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 14;
			NPC.damage = 18;
			NPC.width = 40; //324
			NPC.height = 30; //216
			NPC.defense = 10;
			NPC.lifeMax = 50;
			NPC.knockBackResist = 0f;
			AnimationType = 121;
			NPC.value = Item.buyPrice(0, 0, 1, 0);
			NPC.alpha = 50;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("AeroSlimeBanner").Type;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || (!CalamityWorldPreTrailer.downedHiveMind && !CalamityWorldPreTrailer.downedPerforator) || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyss ||
				spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
			{
				return 0f;
			}
			return SpawnCondition.Cavern.Chance * 0.05f;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 59, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 59, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("AerialiteOre").Type, 1, 10, 27));
		}
	}
}