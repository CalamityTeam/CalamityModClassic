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
	public class CryoSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cryo Slime");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
				new FlavorTextBestiaryInfoElement("This slime has been infused with the power of Cryonic Ore, now resembling it heavily.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 1;
			NPC.damage = 30;
			NPC.width = 40; //324
			NPC.height = 30; //216
			NPC.defense = 12;
			NPC.lifeMax = 120;
			NPC.knockBackResist = 0f;
			AnimationType = 81;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			NPC.alpha = 50;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("CryoSlimeBanner").Type;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !CalamityWorldPreTrailer.downedCryogen || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyss ||
				spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
			{
				return 0f;
			}
			return SpawnCondition.Cavern.Chance * 0.08f;
		}
		
		/*
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("MSparkle").Type, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("MSparkle").Type, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		*/
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("CryonicOre").Type, 1, 10, 27));
		}
	}
}