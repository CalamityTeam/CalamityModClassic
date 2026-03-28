using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions;
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
	public class CrimulanBlightSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crimulan Blight Slime");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
				new FlavorTextBestiaryInfoElement("A slime blessed by a higher being, and one of the few of its kind not to consume for its own.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 1;
			NPC.damage = 30;
			NPC.width = 60; //324
			NPC.height = 42; //216
			NPC.defense = 12;
			NPC.lifeMax = 130;
			NPC.knockBackResist = 0.3f;
			AnimationType = 244;
			NPC.value = Item.buyPrice(0, 0, 2, 0);
			NPC.alpha = 105;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.buffImmune[24] = true;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("CrimulanBlightSlimeBanner").Type;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyss)
			{
				return 0f;
			}
			return SpawnCondition.Crimson.Chance * 0.15f;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 40; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.ManaSickness, 120, true);
			target.AddBuff(BuffID.Confused, 120, true);
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("EbonianGel").Type, 1, 15, 17));
			npcLoot.Add(new CommonDrop(ItemID.Gel, 1, 10, 15));
			npcLoot.Add(ItemDropRule.ByCondition(new DownedSkeletron(), Mod.Find<ModItem>("Carnage").Type, 100));
		}
	}
}