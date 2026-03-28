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
	public class CalamityEye : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Calamity Eye");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				new FlavorTextBestiaryInfoElement("john calamity")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 2;
			NPC.damage = 40;
			NPC.width = 30; //324
			NPC.height = 32; //216
			NPC.defense = 24;
			NPC.lifeMax = 140;
			NPC.knockBackResist = 0f;
			AnimationType = 2;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("CalamityEyeBanner").Type;
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
			if (spawnInfo.PlayerSafe || !NPC.downedPlantBoss || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSulphur)
			{
				return 0f;
			}
			return SpawnCondition.OverworldNightMonster.Chance * 0.045f;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Weak, 120, true);
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 120, true);
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("BlightedLens").Type, 2));
			npcLoot.Add(new CommonDrop(ItemID.Lens, 2));
		}
	}
}