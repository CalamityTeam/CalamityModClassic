using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.CalamityBiomeNPCs
{
	public class CultistAssassin : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cultist Assassin");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
				new FlavorTextBestiaryInfoElement("A devotee brought to madness by the power of the brimstone flame.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 3;
			NPC.damage = 50;
			NPC.width = 18; //324
			NPC.height = 40; //216
			NPC.defense = 25;
			NPC.lifeMax = 80;
			NPC.knockBackResist = 0.5f;
			AnimationType = 331;
			AIType = NPCID.ChaosElemental;
			NPC.value = Item.buyPrice(0, 0, 2, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath50;
			if (CalamityWorldPreTrailer.downedProvidence)
			{
				NPC.damage = 250;
				NPC.defense = 130;
				NPC.lifeMax = 5000;
				NPC.value = Item.buyPrice(0, 0, 50, 0);
			}
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("CultistAssassinBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<Crag>().Type };
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
			return (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneCalamity || spawnInfo.Player.ZoneDungeon) && Main.hardMode ? 0.04f : 0f;
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
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180, true);
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), Mod.Find<ModItem>("Bloodstone").Type, 2));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("EssenceofChaos").Type, 3));
		}
	}
}