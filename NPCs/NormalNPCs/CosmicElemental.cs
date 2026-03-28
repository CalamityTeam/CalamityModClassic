using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items;
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
	public class CosmicElemental : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Elemental");
			Main.npcFrameCount[NPC.type] = 22;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
				new FlavorTextBestiaryInfoElement("A strange creature that wanders the caves of the world, even stranger are the trinkets it sometimes holds!")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.5f;
			NPC.aiStyle = 91;
			NPC.damage = 20;
			NPC.width = 20; //324
			NPC.height = 30; //216
			NPC.defense = 10;
			NPC.lifeMax = 25;
			NPC.knockBackResist = 0.5f;
			AnimationType = 483;
			NPC.value = Item.buyPrice(0, 0, 3, 0);
			NPC.HitSound = SoundID.NPCHit7;
			NPC.DeathSound = SoundID.NPCDeath6;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("CosmicElementalBanner").Type;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyss || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
			{
				return 0f;
			}
			return SpawnCondition.Cavern.Chance * 0.01f;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180, true);
			}
			target.AddBuff(BuffID.Confused, 180, true);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 70, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 70, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(ItemID.BoneSword, 10));
			npcLoot.Add(new CommonDrop(ItemID.Starfury, 50));
			npcLoot.Add(new CommonDrop(ItemID.EnchantedSword, 100));
			npcLoot.Add(new CommonDrop(ItemID.Arkhalis, 1000));
			npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Starfury, 20));
			npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.EnchantedSword, 29));
			npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Arkhalis, 20));
		}
	}
}